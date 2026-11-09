// Conceptual C# translation of main_07_ambush_gunner.c.
// The C source calls this object ambush_gunner; TrapBlast is the popular reference name.
// This reference file is not included in the game's C build.
using System;

namespace Mmx4.Reference;

internal enum TrapBlastLifecycle
{
    Initialize,
    Active,
    Despawn
}

internal enum TrapBlastBehavior
{
    HitReaction,
    ResumePrevious,
    Emerge,
    Aim,
    Fire
}

// Lifecycle states are Initialize -> Active -> Despawn. Active either despawns after a fatal hit
// or when it leaves the 0x40-pixel screen margin.
// Behavior states: subtype/kind 2-7 starts buried in Emerge, waits for the player to cross the
// configured distance, then bursts out and moves to Aim. Aim waits 0x28 (40) updates, aims toward
// the player, and either enters Fire or retries after 0x3C (60) updates if its shot points the wrong
// way. Fire shoots on animation event 2 and returns to Aim on event 1. Kind 1 and unsupported kinds
// start directly in Aim. HitReaction and ResumePrevious are behavior-table entries, but this object's
// own transitions do not select them. Animation events and behavior-state changes are separate.
internal sealed class TrapBlast
{
    public TrapBlastLifecycle Lifecycle;
    public TrapBlastBehavior Behavior;
    public int SubState; // unk6
    public int Kind { get; set; } // placement subtype, stored in unk2

    public int Hp;
    public int ContactDamage;
    public int InvincibilityTimer;
    public int BackgroundOffset;
    public int Facing; // unk15; zero and nonzero are kept distinct as in the source.
    public int X { get; set; }
    public int Y { get; set; }
    public int PreviousX; // unk18
    public int PreviousY; // unk1C
    public int AnimationEvent { get; set; }
    public int Timer; // unk7C

    public int Unk7A;
    public int Unk80; // Main7Ext.unk80: selects which side of the emerge-distance threshold to use.
    public int Unk84; // Main7Ext.unk84: emerge-distance threshold in pixels.
    public int SavedXVelocity;
    public int SavedYVelocity;
    public int Unk90; // Enables hit processing in the active update.
    public TrapBlastBehavior SavedBehavior;

    // Speeds are 16.16 fixed point: 65536 = 1 pixel per update.
    public int XSpeed;
    public int YSpeed;

    private static readonly int[] TerrainBox = { -13, -12, 25, 24 };
    private static readonly int[] Debris = { 3, 4, 5, 6, 7, 0, 0, 0 };
    private static readonly int[] EmergeParticles = { 1, 2, 3, 4, 1, 2, 3, 4 };

    public void Update(ITrapBlastWorld world)
    {
        switch (Lifecycle)
        {
            case TrapBlastLifecycle.Initialize:
                Initialize(world);
                break;
            case TrapBlastLifecycle.Active:
                UpdateActive(world);
                break;
            case TrapBlastLifecycle.Despawn:
                Despawn(world);
                break;
        }
    }

    private void Initialize(ITrapBlastWorld world)
    {
        Hp = 6;
        ContactDamage = 3;
        InvincibilityTimer = 0;
        BackgroundOffset = world.PlayerBackgroundOffset;
        XSpeed = 0;
        YSpeed = 0;
        PreviousX = X;
        PreviousY = Y;
        world.ConfigureCollisionAndAnimation(this, Animations, TerrainBox, TerrainBox);

        if (Kind == 1)
        {
            world.SetAnimation(this, 1); // Animation 1: exposed/aiming pose.
            Behavior = TrapBlastBehavior.Aim;
            Facing = world.FacingRight;
            Unk90 = 1;
        }
        else if (Kind >= 2 && Kind <= 7)
        {
            world.SetAnimation(this, 0); // Animation 0: emerge from the ground.
            Behavior = TrapBlastBehavior.Emerge;
            Unk7A = 1;
            InvincibilityTimer = 1;
            Facing = (Kind & 1) != 0 ? world.FacingRight : 0;
            Unk80 = Kind >= 6 ? 1 : 0;
            Unk84 = Kind >= 6 ? 0x28 : Kind >= 4 ? 0x8C : 0x64;
            Unk90 = 0;
        }
        else
        {
            world.SetAnimation(this, 1); // Animation 1: exposed/aiming pose.
            Behavior = TrapBlastBehavior.Aim;
            Facing = 0;
            Unk90 = 1;
        }

        Lifecycle = TrapBlastLifecycle.Active;
        SubState = 0;
    }

    private void UpdateActive(ITrapBlastWorld world)
    {
        PreviousX = X;
        PreviousY = Y;
        RunBehavior(world);

        if (Unk90 != 0)
        {
            world.PrepareHitCheck(this);
            SavedBehavior = Behavior;
            if (world.CheckHit(this) < 0)
            {
                world.SpawnExplosion(this);
                world.SpawnDebris(this, Debris, count: 5);
                world.ApplyImpactEffect(this, 0x0E, X + ((Kind & 1) == 0 ? -0x0A : 0x0A), Y);
                Lifecycle = TrapBlastLifecycle.Despawn;
                return;
            }
        }

        if (!world.IsOutsideScreen(this, 0x40, 0x40))
        {
            if (Behavior != TrapBlastBehavior.Emerge || SubState != 0)
                world.UpdateOnScreen(this, 0x20, 0x20);
        }
        else
        {
            Lifecycle = TrapBlastLifecycle.Despawn;
        }
    }

    private void RunBehavior(ITrapBlastWorld world)
    {
        switch (Behavior)
        {
            case TrapBlastBehavior.HitReaction:
                world.RunEnemyHitReaction(this);
                break;
            case TrapBlastBehavior.ResumePrevious:
                Behavior = SavedBehavior;
                break;
            case TrapBlastBehavior.Emerge:
                UpdateEmerge(world);
                break;
            case TrapBlastBehavior.Aim:
                UpdateAim(world);
                break;
            case TrapBlastBehavior.Fire:
                UpdateFire(world);
                break;
        }
    }

    private void UpdateEmerge(ITrapBlastWorld world)
    {
        switch (SubState)
        {
            case 0:
                WaitForEmerge(world);
                break;
            case 1:
                EmergeBurst(world);
                break;
            case 2:
                FinishEmerge(world);
                break;
        }
    }

    private void WaitForEmerge(ITrapBlastWorld world)
    {
        bool playerCrossedTrap =
            (Facing == 0 && X > world.PlayerX) ||
            (Facing != 0 && X < world.PlayerX);
        if (!playerCrossedTrap)
            return;

        int distance = Math.Abs(X - world.PlayerX);
        if ((Unk80 == 0 && distance < Unk84) || (Unk80 != 0 && distance > Unk84))
        {
            Unk7A = 0;
            SubState = 1;
        }
    }

    private void EmergeBurst(ITrapBlastWorld world)
    {
        InvincibilityTimer = 0;
        Unk90 = 1;
        world.SpawnRubble(this, EmergeParticles, count: 8,
            horizontalSpeed: Facing == 0 ? -3 * 0x10000 : 3 * 0x10000);
        world.PlaySound(this, channel: 2, playStationSound: 0x15, saturnSound: 0x14);
        world.Animate(this);
        SubState = 2;
    }

    private void FinishEmerge(ITrapBlastWorld world)
    {
        if (AnimationEvent == 2)
            world.PlaySound(this, channel: 2, playStationSound: 0x16, saturnSound: 0x15);

        if (AnimationEvent == 1)
        {
            world.SetAnimation(this, 1); // Animation 1: exposed/aiming pose.
            Behavior = TrapBlastBehavior.Aim;
            SubState = 0;
        }

        world.Animate(this);
    }

    private void UpdateAim(ITrapBlastWorld world)
    {
        if (SubState == 0)
        {
            Timer = 0x28; // 40 updates before aiming.
            SubState = 1;
            return;
        }

        if (SubState != 1)
            return;

        if (Timer == 0)
        {
            world.SetVelocityFromAngle(this, world.AngleToPlayer(this) & 0xFF);
            bool shotPointsTowardFacing =
                (Facing == 0 && XSpeed < 0) ||
                (Facing != 0 && XSpeed > 0);

            if (shotPointsTowardFacing)
            {
                SavedXVelocity = XSpeed;
                SavedYVelocity = YSpeed;
                world.SetAnimation(this, 2); // Animation 2: firing.
                Behavior = TrapBlastBehavior.Fire;
                SubState = 2;
            }
            else
            {
                Timer = 0x3C; // Retry after 60 updates.
            }

            XSpeed = 0;
            YSpeed = 0;
        }
        else
        {
            Timer--;
        }
    }

    private void UpdateFire(ITrapBlastWorld world)
    {
        if (AnimationEvent == 2)
        {
            world.PlaySound(this, channel: 2, playStationSound: 0x17, saturnSound: 0x16);
            TrapBlastShot? shot = world.FindFreeShot();
            if (shot != null)
            {
                shot.Active = 0x41;
                shot.Id = 2;
                shot.SpriteTexture = SpriteTexture;
                shot.SpritePalette = SpritePalette;
                shot.AnimationTable = Animations;
                shot.SpriteFrames = SpriteFrames;
                shot.Kind = Kind & 1;
                shot.Facing = Facing;
                shot.BackgroundOffset = BackgroundOffset;
                shot.X = X;
                shot.Y = Y;

                world.SetVelocityFromAngle(this, world.AngleToPlayer(this) & 0xFF);
                if ((Facing == 0 && XSpeed < 0) || (Facing != 0 && XSpeed > 0))
                {
                    shot.XVelocity = XSpeed;
                    shot.YVelocity = YSpeed;
                }
                else
                {
                    shot.XVelocity = SavedXVelocity;
                    shot.YVelocity = SavedYVelocity;
                }

                XSpeed = 0;
                YSpeed = 0;
            }
        }

        if (AnimationEvent == 1)
        {
            world.SetAnimation(this, 1); // Animation 1: return to exposed/aiming pose.
            Behavior = TrapBlastBehavior.Aim;
            SubState = 0;
        }

        world.Animate(this);
    }

    private void Despawn(ITrapBlastWorld world)
    {
        Unk7A = 0;
        Unk80 = 0;
        Unk84 = 0;
        SavedXVelocity = 0;
        SavedYVelocity = 0;
        Unk90 = 0;
        SavedBehavior = (TrapBlastBehavior)0;
        InvincibilityTimer = 0;
        world.DespawnObject(this);
    }

    // Animation indices selected by this behavior:
    // 0 = emergence; 1 = exposed/aiming; 2 = firing. Animations 3-8 are defined in the source,
    // but this object's behavior does not select them.
    private static readonly AnimationStep[][] Animations =
    {
        // Animation 0: emergence.
        new[]
        {
            new AnimationStep(2, 0, 1, 0x00),
            new AnimationStep(2, 0, 1, 0x01),
            new AnimationStep(2, 0, 1, 0x02),
            new AnimationStep(6, 0, 1, 0x03),
            new AnimationStep(6, 0, 1, 0x04),
            new AnimationStep(4, 0, 1, 0x05),
            new AnimationStep(5, 0, 1, 0x06),
            new AnimationStep(1, 0, 1, 0x07),
            new AnimationStep(1, 0, 1, 0x08),
            new AnimationStep(1, 0, 1, 0x09),
            new AnimationStep(2, 0, 1, 0x0A),
            new AnimationStep(2, 0, 1, 0x0B),
            new AnimationStep(2, 0, 1, 0x0C),
            new AnimationStep(2, 0, 1, 0x0D),
            new AnimationStep(1, 2, 1, 0x0E),
            new AnimationStep(5, 0, 1, 0x0E),
            new AnimationStep(3, 0, 1, 0x0F),
            new AnimationStep(3, 0, 1, 0x10),
            new AnimationStep(3, 0, 1, 0x11),
            new AnimationStep(6, 0, 1, 0x0E),
            new AnimationStep(1, 0, 1, 0x12),
            new AnimationStep(1, 0, 1, 0x13),
            new AnimationStep(1, 0, 1, 0x14),
            new AnimationStep(6, 0, 1, 0x15),
            new AnimationStep(2, 0, 1, 0x16),
            new AnimationStep(2, 0, 1, 0x17),
            new AnimationStep(6, 0, 1, 0x18),
            new AnimationStep(1, 0, 1, 0x19),
            new AnimationStep(1, 0, 1, 0x1A),
            new AnimationStep(6, 0, 1, 0x1B),
            new AnimationStep(5, 0, 1, 0x1C),
            new AnimationStep(8, 0, 1, 0x1D),
            new AnimationStep(5, 0, 1, 0x1C),
            new AnimationStep(4, 0, 1, 0x1B),
            new AnimationStep(1, 1, 0, 0x1B),
        },
        // Animation 1: exposed/aiming pose.
        new[] { new AnimationStep(1, 1, 0, 0x1B) },
        // Animation 2: firing.
        new[]
        {
            new AnimationStep(2, 0, 1, 0x1E),
            new AnimationStep(2, 0, 1, 0x1F),
            new AnimationStep(1, 0, 1, 0x20),
            new AnimationStep(1, 0, 1, 0x21),
            new AnimationStep(1, 0, 1, 0x22),
            new AnimationStep(2, 0, 1, 0x1B),
            new AnimationStep(2, 0, 1, 0x1E),
            new AnimationStep(2, 0, 1, 0x1F),
            new AnimationStep(1, 0, 1, 0x20),
            new AnimationStep(1, 0, 1, 0x21),
            new AnimationStep(1, 0, 1, 0x22),
            new AnimationStep(2, 0, 1, 0x1B),
            new AnimationStep(2, 0, 1, 0x23),
            new AnimationStep(2, 0, 1, 0x24),
            new AnimationStep(1, 0, 1, 0x25),
            new AnimationStep(1, 0, 1, 0x26),
            new AnimationStep(1, 0, 1, 0x27),
            new AnimationStep(2, 0, 1, 0x1C),
            new AnimationStep(2, 0, 1, 0x23),
            new AnimationStep(2, 0, 1, 0x24),
            new AnimationStep(1, 0, 1, 0x25),
            new AnimationStep(1, 0, 1, 0x26),
            new AnimationStep(1, 0, 1, 0x27),
            new AnimationStep(1, 2, 1, 0x28),
            new AnimationStep(1, 0, 1, 0x29),
            new AnimationStep(1, 0, 1, 0x2A),
            new AnimationStep(2, 0, 1, 0x2B),
            new AnimationStep(3, 0, 1, 0x2C),
            new AnimationStep(4, 0, 1, 0x2D),
            new AnimationStep(4, 0, 1, 0x2E),
            new AnimationStep(1, 1, 0, 0x2E),
        },
        // Animation 3: defined but not selected by this behavior.
        new[] { new AnimationStep(1, 1, 0, 0x34) },
        // Animation 4: defined but not selected by this behavior.
        new[] { new AnimationStep(1, 1, 0, 0x35) },
        // Animation 5: defined but not selected by this behavior.
        new[] { new AnimationStep(1, 1, 0, 0x36) },
        // Animation 6: defined but not selected by this behavior.
        new[]
        {
            new AnimationStep(2, 0, 1, 0x37),
            new AnimationStep(3, 0, 1, 0x38),
            new AnimationStep(3, 0, 1, 0x39),
            new AnimationStep(3, 0, 1, 0x3A),
            new AnimationStep(2, 0, 1, 0x3B),
            new AnimationStep(1, 0, 1, 0x3C),
            new AnimationStep(1, 0, 1, 0x3D),
            new AnimationStep(1, 1, 0, 0x3E),
        },
        // Animation 7: defined but not selected by this behavior.
        new[] { new AnimationStep(1, 1, 0, 0x3F) },
        // Animation 8: defined but not selected by this behavior.
        new[]
        {
            new AnimationStep(1, 0, 1, 0x2F),
            new AnimationStep(1, 0, 1, 0x30),
            new AnimationStep(1, 0, 1, 0x31),
            new AnimationStep(1, 0, 1, 0x32),
            new AnimationStep(1, 1, -4, 0x33),
        },
    };

    // These are populated by the engine's object/resource setup before a shot is created.
    public int SpriteTexture { get; set; }
    public int SpritePalette { get; set; }
    public byte[]? SpriteFrames { get; set; }
}

internal readonly record struct AnimationStep(int Duration, int Frame, int Step, int Event);

internal sealed class TrapBlastShot
{
    public int Active;
    public int Id;
    public int Kind;
    public int Facing;
    public int BackgroundOffset;
    public int X;
    public int Y;
    public int XVelocity;
    public int YVelocity;
    public int SpriteTexture;
    public int SpritePalette;
    public AnimationStep[][]? AnimationTable;
    public byte[]? SpriteFrames;
}

internal interface ITrapBlastWorld
{
    int PlayerBackgroundOffset { get; }
    int PlayerX { get; }
    int FacingRight { get; }

    void ConfigureCollisionAndAnimation(
        TrapBlast enemy,
        AnimationStep[][] animations,
        int[] attackBox,
        int[] hurtBox);
    void SetAnimation(TrapBlast enemy, int animation);
    void Animate(TrapBlast enemy);
    void PrepareHitCheck(TrapBlast enemy);
    int CheckHit(TrapBlast enemy);
    void RunEnemyHitReaction(TrapBlast enemy);
    void SpawnExplosion(TrapBlast enemy);
    void SpawnDebris(TrapBlast enemy, int[] debris, int count);
    void ApplyImpactEffect(TrapBlast enemy, int effectId, int x, int y);
    bool IsOutsideScreen(TrapBlast enemy, int marginX, int marginY);
    void UpdateOnScreen(TrapBlast enemy, int marginX, int marginY);
    void DespawnObject(TrapBlast enemy);
    void SpawnRubble(TrapBlast enemy, int[] particles, int count, int horizontalSpeed);
    void PlaySound(TrapBlast enemy, int channel, int playStationSound, int saturnSound);
    int AngleToPlayer(TrapBlast enemy);
    void SetVelocityFromAngle(TrapBlast enemy, int angle);
    TrapBlastShot? FindFreeShot();
}
