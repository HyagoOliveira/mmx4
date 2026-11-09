// Conceptual C# translation of main_42_homing_orb.c.
// This reference file is not included in the game's C build.
// The source identifies the object as homing_orb; TriScan is the requested alias.
using System;

namespace Mmx4.Reference;

internal enum HomingOrbLifecycle
{
    Initialize,
    Active,
    Despawn
}

internal enum HomingOrbBehavior
{
    HitReaction,
    StartIdle,
    Wait,
    Lunge,
    Leave
}

// Behavior overview (timings are game updates, not seconds):
// - Lifecycle: Initialize runs once, then Active. A fatal hit spawns effects and advances to Despawn; leaving the
//   32-pixel screen margin removes the object directly.
// - Wait: starts with a 180-update timer. At zero, aim toward the player, increment BackgroundRelative, set a 90-count
//   timer, and enter Lunge.
// - Lunge: decrements the timer twice per update, so 90 counts take about 45 updates. At zero, BackgroundRelative < 5
//   resets movement and returns to Wait for 180 updates; otherwise set upward speed to 2 pixels/update and enter Leave.
// - Leave keeps moving until the object leaves the screen margin, then it is removed.
// - HitReaction and StartIdle exist in the behavior table, but normal transitions do not select them. On a fatal hit,
//   the main lifecycle advances directly to Despawn without running HitReaction; StartIdle only selects animation 0.
//   Animations are separate from these behavior states.
internal sealed class HomingOrbTriScan
{
    public HomingOrbLifecycle Lifecycle;
    public HomingOrbBehavior Behavior;

    public int Hp;
    public int ContactDamage;
    public int InvincibilityTimer;
    public int BackgroundOffset;
    public int XSpeed; // 16.16 fixed point: 65536 = 1 pixel per update.
    public int YSpeed; // Positive Y speed moves upward; movement subtracts it from screen Y.
    public int XAcceleration; // 16.16 fixed-point change to X speed per update.
    public int Gravity; // 16.16 fixed-point change to Y speed per update.
    public int Timer; // unk7C
    public int SpriteFlags; // unk42
    public int AnimationEvent;
    public int X;
    public int Y;

    public int BackgroundRelative; // ext.main_42.background_relative
    public HomingOrbMisc? CurrentMisc; // ext.main_42.unk84

    private static readonly int[] Debris = { 4, 5, 6, 4, 5, 6, 0, 0 };
    private static readonly int[] HomingOrbHurtBox = { -6, -6, 11, 11 };
    private static readonly int[] HomingOrbAttackBox = { -10, -9, 17, 17 };

    public void Update(IHomingOrbWorld world)
    {
        switch (Lifecycle)
        {
            case HomingOrbLifecycle.Initialize:
                Initialize(world);
                break;
            case HomingOrbLifecycle.Active:
                UpdateActive(world);
                break;
            case HomingOrbLifecycle.Despawn:
                world.DespawnObject(this);
                break;
        }
    }

    private void Initialize(IHomingOrbWorld world)
    {
        Behavior = HomingOrbBehavior.Wait;
        Lifecycle = HomingOrbLifecycle.Active;
        BackgroundOffset = world.PlayerBackgroundOffset;
        Hp = 3;
        ContactDamage = 3;
        InvincibilityTimer = 0;
        XSpeed = 0;
        XAcceleration = 0;
        YSpeed = 0;
        Gravity = 0;
        Timer = 0xB4; // 180 update ticks before the first lunge.
        BackgroundRelative = 0;
        CurrentMisc = null;
        world.ConfigureCollisionAndAnimation(
            this,
            Animations,
            attackBox: HomingOrbHurtBox,
            hurtBox: HomingOrbAttackBox
        );
        world.SetAnimation(this, 0);
        world.UpdateOnScreen(this, 0x20, 0x20); // 32-pixel screen margins.
    }

    private void UpdateActive(IHomingOrbWorld world)
    {
        if (world.CheckHit(this) < 0)
        {
            Lifecycle = HomingOrbLifecycle.Despawn;
            Behavior = HomingOrbBehavior.HitReaction;
            SpriteFlags &= 0x7FFF;
            world.SpawnExplosion(this);
            world.SpawnDebris(this, Debris);
            return;
        }

        RunBehavior(world);
        world.UpdateCollision(this);

        if (!world.IsOutsideScreen(this, 0x20, 0x20)) // 32-pixel screen margins.
        {
            world.UpdateOnScreen(this, 0x20, 0x20); // 32-pixel screen margins.
            return;
        }

        world.DespawnObject(this);
    }

    private void RunBehavior(IHomingOrbWorld world)
    {
        switch (Behavior)
        {
            case HomingOrbBehavior.HitReaction:
                world.RunEnemyHitReaction(this);
                break;
            case HomingOrbBehavior.StartIdle:
                world.SetAnimation(this, 0);
                break;
            case HomingOrbBehavior.Wait:
                Wait(world);
                break;
            case HomingOrbBehavior.Lunge:
                Lunge(world);
                break;
            case HomingOrbBehavior.Leave:
                world.Animate(this);
                world.Move(this);
                break;
        }
    }

    private void Wait(IHomingOrbWorld world)
    {
        if (--Timer == 0)
        {
            Timer = 0x5A; // 90 timer counts; Lunge decrements twice per update, so it lasts about 45 updates.
            world.SetVelocityFromAngle(this, world.AngleToPlayer(this) & 0xFF);

            XAcceleration = -((XSpeed * 0x2D) >> 8); // 0x2D = 45; scales speed by 45/256 ~= 0.176.
            int previousYSpeed = YSpeed;
            XSpeed = 0;
            YSpeed = 0;
            Gravity = -((previousYSpeed * 0x2D) >> 8); // Same 45/256 ~= 0.176 scale for vertical speed.
            world.SetAnimation(this, 1);
            Behavior = HomingOrbBehavior.Lunge;
            BackgroundRelative++;
            CurrentMisc = null;
        }

        world.Animate(this);
    }

    private void Lunge(IHomingOrbWorld world)
    {
        // The original decrements the timer twice per update.
        if (--Timer == 0x5A) // Compare against 90 after decrementing; preserve the source's exact condition.
        {
            XAcceleration = -XAcceleration;
            Gravity = -Gravity;
        }

        if (--Timer == 0)
        {
            if (BackgroundRelative < 5)
            {
                Timer = 0xB4; // 180 update ticks before the next lunge.
                XAcceleration = 0;
                Gravity = 0;
                XSpeed = 0;
                YSpeed = 0;
                world.SetAnimation(this, 0);
                Behavior = HomingOrbBehavior.Wait;
            }
            else
            {
                XSpeed = 0;
                YSpeed = 2 * 0x10000; // FIXED(2): 131072 = 2.0 pixels per update, upward.
                world.SetAnimation(this, 3);
                Behavior = HomingOrbBehavior.Leave;
                CurrentMisc = null;
            }
        }

        if ((Timer & 3) == 0)
        {
            HomingOrbMisc? misc = world.GetOrCreateMisc(CurrentMisc, 0);
            if (misc != null)
            {
                misc.Active = 0x41;
                misc.Id = 0x0E;
                misc.Owner = this;
                misc.X = X;
                misc.Y = Y;
                misc.AnimationEvent = AnimationEvent;
                CurrentMisc = misc;
            }
        }

        world.Animate(this);
        world.MoveWithGravity(this); // Shared helper clamps downward Y speed to -6.5 pixels per update.
    }

    private static readonly AnimationStep[][] Animations =
    {
        new[]
        {
            new AnimationStep(3, 0, 1, 0x00),
            new AnimationStep(3, 0, 1, 0x01),
            new AnimationStep(3, 0, 1, 0x02),
            new AnimationStep(3, 0, 1, 0x03),
            new AnimationStep(2, 0, 1, 0x04),
            new AnimationStep(1, 0, -5, 0x04),
        },
        new[]
        {
            new AnimationStep(3, 0, 1, 0x00),
            new AnimationStep(2, 1, 1, 0x14),
            new AnimationStep(2, 2, 1, 0x15),
            new AnimationStep(2, 3, 1, 0x16),
            new AnimationStep(2, 4, 1, 0x17),
            new AnimationStep(2, 5, 1, 0x18),
            new AnimationStep(2, 6, 1, 0x19),
            new AnimationStep(2, 7, 1, 0x1A),
            new AnimationStep(2, 8, 1, 0x1B),
            new AnimationStep(2, 9, 1, 0x1C),
            new AnimationStep(2, 10, 1, 0x1D),
            new AnimationStep(2, 11, 1, 0x1E),
            new AnimationStep(2, 12, 1, 0x1F),
            new AnimationStep(2, 13, 1, 0x20),
            new AnimationStep(1, 14, 1, 0x04),
            new AnimationStep(1, 14, -15, 0x04),
        },
        new[]
        {
            new AnimationStep(2, 0, 1, 0x05),
            new AnimationStep(2, 0, 1, 0x06),
            new AnimationStep(2, 0, 1, 0x07),
            new AnimationStep(2, 0, 1, 0x08),
            new AnimationStep(2, 0, 1, 0x09),
            new AnimationStep(2, 0, 1, 0x0A),
            new AnimationStep(2, 0, 1, 0x0B),
            new AnimationStep(2, 1, 1, 0x0C),
            new AnimationStep(2, 0, 1, 0x0D),
            new AnimationStep(2, 0, 1, 0x0E),
            new AnimationStep(2, 0, 1, 0x0F),
            new AnimationStep(2, 0, 1, 0x10),
            new AnimationStep(2, 0, 1, 0x11),
            new AnimationStep(2, 0, 1, 0x12),
            new AnimationStep(1, 0, 1, 0x13),
            new AnimationStep(1, 0, -15, 0x13),
        },
        new[]
        {
            new AnimationStep(2, 0, 1, 0x00),
            new AnimationStep(2, 1, 1, 0x01),
            new AnimationStep(2, 2, 1, 0x02),
            new AnimationStep(2, 3, 1, 0x03),
            new AnimationStep(2, 4, 1, 0x04),
            new AnimationStep(2, 5, 1, 0x00),
            new AnimationStep(2, 6, 1, 0x01),
            new AnimationStep(2, 7, 1, 0x02),
            new AnimationStep(2, 8, 1, 0x03),
            new AnimationStep(2, 9, 1, 0x04),
            new AnimationStep(2, 10, 1, 0x00),
            new AnimationStep(2, 11, 1, 0x01),
            new AnimationStep(2, 12, 1, 0x02),
            new AnimationStep(2, 13, 1, 0x03),
            new AnimationStep(1, 14, 1, 0x04),
            new AnimationStep(1, 14, -15, 0x04),
        },
        new[] { new AnimationStep(1, 0, 0, 0x21) },
        new[] { new AnimationStep(1, 0, 0, 0x22) },
        new[] { new AnimationStep(1, 0, 0, 0x23) },
    };
}

internal readonly record struct AnimationStep(int Duration, int Frame, int Step, int Event);

internal sealed class HomingOrbMisc
{
    public int Active;
    public int Id;
    public HomingOrbTriScan? Owner;
    public int X;
    public int Y;
    public int AnimationEvent;
}

internal interface IHomingOrbWorld
{
    int PlayerBackgroundOffset { get; }

    void ConfigureCollisionAndAnimation(
        HomingOrbTriScan orb,
        AnimationStep[][] animations,
        int[] attackBox,
        int[] hurtBox);
    int CheckHit(HomingOrbTriScan orb);
    void RunEnemyHitReaction(HomingOrbTriScan orb);
    void SpawnExplosion(HomingOrbTriScan orb);
    void SpawnDebris(HomingOrbTriScan orb, int[] debris);
    void UpdateCollision(HomingOrbTriScan orb);
    bool IsOutsideScreen(HomingOrbTriScan orb, int marginX, int marginY);
    void UpdateOnScreen(HomingOrbTriScan orb, int marginX, int marginY);
    void DespawnObject(HomingOrbTriScan orb);
    int AngleToPlayer(HomingOrbTriScan orb);
    void SetVelocityFromAngle(HomingOrbTriScan orb, int angle);
    void SetAnimation(HomingOrbTriScan orb, int animation);
    void Animate(HomingOrbTriScan orb);
    void Move(HomingOrbTriScan orb);
    void MoveWithGravity(HomingOrbTriScan orb);
    HomingOrbMisc? GetOrCreateMisc(HomingOrbMisc? previous, int argument);
}
