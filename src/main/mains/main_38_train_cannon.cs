// Conceptual C# translation of main_38_train_cannon.c.
// This reference file is not included in the game's C build.
using System;

namespace Mmx4.Reference;

internal enum TrainCannonLifecycle
{
    Initialize,
    Active,
    Exploding,
    Despawn
}

internal enum TrainCannonBehavior
{
    HitReaction,
    ResumePrevious,
    Idle,
    Charge,
    Blast,
    Volley,
    Recoil
}

internal sealed class TrainCannon
{
    public TrainCannonLifecycle Lifecycle;
    public TrainCannonBehavior Behavior;
    public int SubState;

    public int Hp;
    public int ContactDamage;
    public int InvincibilityTimer;
    public bool IsActive;
    public int Timer; // unk7C
    public int AnimationTimer; // unk7E
    public int SavedBehavior;

    // These fields are still unnamed in the original C structure.
    public int Unk84;
    public int Unk88;
    public int Unk8C;

    public int BackgroundOffset;
    public int X;
    public int Y;
    public int PreviousX;
    public int PreviousY;

    public void Update(ITrainCannonWorld world)
    {
        switch (Lifecycle)
        {
            case TrainCannonLifecycle.Initialize:
                Initialize(world);
                break;
            case TrainCannonLifecycle.Active:
                UpdateActive(world);
                break;
            case TrainCannonLifecycle.Exploding:
                UpdateExplosion(world);
                break;
            case TrainCannonLifecycle.Despawn:
                Despawn(world);
                break;
        }
    }

    private void Initialize(ITrainCannonWorld world)
    {
        IsActive = true;
        Hp = 0x12;
        ContactDamage = 6;
        InvincibilityTimer = 0;
        BackgroundOffset = world.PlayerBackgroundOffset;
        world.ConfigureCollisionAndAnimation(this);
        world.SetAnimation(this, 0);

        AnimationTimer = 0xA;
        Timer = 0;
        SavedBehavior = 0;
        Unk84 = 0;
        Unk88 = 0;
        Unk8C = 0;
        Behavior = TrainCannonBehavior.Idle;
        SubState = 0;
        Lifecycle = TrainCannonLifecycle.Active;
    }

    private void UpdateActive(ITrainCannonWorld world)
    {
        PreviousX = X;
        PreviousY = Y;
        RunBehavior(world);

        int collision = world.CheckTerrainCollision(this);
        world.UpdateCollision(this);
        world.CollideWithPlayer(this);

        if (Behavior != TrainCannonBehavior.HitReaction)
            SavedBehavior = (int)Behavior;

        if (collision < 0)
        {
            world.SpawnDebris(this, count: 6);
            if (world.Stage == 8)
                world.ApplyTileEffect(0xB, X - 0x28, Y);

            Unk88 = 2;
            Unk8C = 1;
            Timer = 0x20;
            AnimationTimer = 5;
            Lifecycle = TrainCannonLifecycle.Exploding;
            world.SetOnScreen(this, false);
            return;
        }

        if (!world.IsNearScreen(this, 0x50, 0x40))
        {
            world.UpdateOnScreen(this, 0x40, 0x20);
            return;
        }

        if (Behavior == TrainCannonBehavior.Blast)
            return;

        if (world.PlayerX > X)
            Lifecycle = TrainCannonLifecycle.Despawn;
        else
            Behavior = TrainCannonBehavior.Idle;
    }

    private void RunBehavior(ITrainCannonWorld world)
    {
        switch (Behavior)
        {
            case TrainCannonBehavior.HitReaction:
                world.RunEnemyHitReaction(this);
                break;
            case TrainCannonBehavior.ResumePrevious:
                Behavior = (TrainCannonBehavior)SavedBehavior;
                break;
            case TrainCannonBehavior.Idle:
                UpdateIdle(world);
                break;
            case TrainCannonBehavior.Charge:
                UpdateCharge(world);
                break;
            case TrainCannonBehavior.Blast:
                UpdateBlast(world);
                break;
            case TrainCannonBehavior.Volley:
                UpdateVolley(world);
                break;
            case TrainCannonBehavior.Recoil:
                UpdateRecoil(world);
                break;
        }
    }

    private void UpdateIdle(ITrainCannonWorld world)
    {
        world.Animate(this);
        if (SubState == 0)
        {
            world.SetAnimation(this, 0);
            SubState++;
            return;
        }

        if (--AnimationTimer != 0)
            return;

        switch (Unk84)
        {
            case 0:
                Behavior = TrainCannonBehavior.Charge;
                AnimationTimer = 0x14;
                break;
            case 1:
                Behavior = TrainCannonBehavior.Volley;
                AnimationTimer = 0x14;
                break;
            case 2:
                Behavior = TrainCannonBehavior.Volley;
                AnimationTimer = 0xA;
                break;
        }

        SubState = 0;
        Unk84 = (Unk84 + 1) % 3;
    }

    private void UpdateCharge(ITrainCannonWorld world)
    {
        world.Animate(this);
        if (SubState == 0)
        {
            world.SetAnimation(this, 1);
            world.PlaySound(this, 0x60);
            SpawnChargeGlow(world);
            Timer = 0x50;
            SubState++;
            return;
        }

        if (--Timer == 0)
        {
            Behavior = TrainCannonBehavior.Blast;
            Unk88 = 1;
            SubState = 0;
        }
    }

    private void UpdateBlast(ITrainCannonWorld world)
    {
        world.Animate(this);
        if (SubState == 0)
        {
            world.PlaySound(this, 0x61);
            world.SetAnimation(this, 2);
            Unk88 = 2;
            FireBlast(world);
            Timer = 0x6E;
            SubState++;
            return;
        }

        if (--Timer == 0)
        {
            world.SetAnimation(this, 0);
            Unk88 = 0;
            Behavior = TrainCannonBehavior.Idle;
            SubState = 0;
        }
    }

    private void UpdateVolley(ITrainCannonWorld world)
    {
        world.Animate(this);
        switch (SubState)
        {
            case 0:
                world.PlaySound(this, 0x62);
                world.SetAnimation(this, 5);
                Timer = 0xA;
                SubState++;
                break;
            case 1:
                if (--Timer == 0)
                {
                    FireVolley(world);
                    Timer = 0x14;
                    SubState++;
                }
                break;
            case 2:
                if (--Timer == 0)
                {
                    Behavior = TrainCannonBehavior.Recoil;
                    SubState = 0;
                }
                break;
        }
    }

    private void UpdateRecoil(ITrainCannonWorld world)
    {
        world.Animate(this);
        if (SubState == 0)
        {
            world.SetAnimation(this, 6);
            Timer = 0x28;
            SubState++;
            return;
        }

        if (--Timer == 0)
        {
            Behavior = TrainCannonBehavior.Idle;
            SubState = 0;
        }
    }

    private void UpdateExplosion(ITrainCannonWorld world)
    {
        if (--Timer == 0)
        {
            world.DropItem(this, 8);
            Lifecycle = TrainCannonLifecycle.Despawn;
        }

        if (--AnimationTimer == 0)
        {
            world.SpawnExplosionEffect(this, 0, 0x18, 0x10);
            AnimationTimer = 5;
        }
    }

    private void Despawn(ITrainCannonWorld world)
    {
        Unk88 = 2;
        if (Unk8C != 0)
            world.ClearObjectState(this);
        else
            world.DespawnObject(this);
    }

    private void FireBlast(ITrainCannonWorld world)
    {
        for (int i = 0; i < 2; i++)
        {
            TrainCannonShot? shot = world.TryGetFreeShot();
            if (shot == null)
                continue;

            shot.Initialize(this, id: 0x14, subId: i, backgroundOffset: BackgroundOffset);
            shot.Layer = world.OwnerUnk2(this) == 0 ? 2 : 1;
        }
    }

    private void FireVolley(ITrainCannonWorld world)
    {
        for (int i = 0; i < 2; i++)
        {
            TrainCannonShot? shot = world.TryGetFreeShot();
            if (shot == null)
                continue;

            shot.Initialize(this, id: 0x15, subId: i, backgroundOffset: BackgroundOffset);
            shot.Layer = i == 0 ? world.Layer4 : world.Layer6;
        }
    }

    private void SpawnChargeGlow(ITrainCannonWorld world)
    {
        TrainCannonVisual? glow = world.TryGetFreeVisual();
        glow?.Initialize(this, BackgroundOffset, X, Y);
    }
}

internal sealed class TrainCannonShot
{
    public int Id;
    public int SubId;
    public int Layer;
    public TrainCannon? Owner;
    public int BackgroundOffset;

    public void Initialize(TrainCannon owner, int id, int subId, int backgroundOffset)
    {
        Owner = owner;
        Id = id;
        SubId = subId;
        BackgroundOffset = backgroundOffset;
    }
}

internal sealed class TrainCannonVisual
{
    public TrainCannon? Owner;
    public int BackgroundOffset;
    public int X;
    public int Y;

    public void Initialize(TrainCannon owner, int backgroundOffset, int x, int y)
    {
        Owner = owner;
        BackgroundOffset = backgroundOffset;
        X = x;
        Y = y;
    }
}

internal interface ITrainCannonWorld
{
    int Stage { get; }
    int PlayerBackgroundOffset { get; }
    int PlayerX { get; }
    int Layer4 { get; }
    int Layer6 { get; }

    int OwnerUnk2(TrainCannon cannon);
    void ConfigureCollisionAndAnimation(TrainCannon cannon);
    int CheckTerrainCollision(TrainCannon cannon);
    void UpdateCollision(TrainCannon cannon);
    void CollideWithPlayer(TrainCannon cannon);
    bool IsNearScreen(TrainCannon cannon, int marginX, int marginY);
    void UpdateOnScreen(TrainCannon cannon, int marginX, int marginY);
    void SetOnScreen(TrainCannon cannon, bool onScreen);
    void SetAnimation(TrainCannon cannon, int animation);
    void Animate(TrainCannon cannon);
    void PlaySound(TrainCannon cannon, int soundId);
    void RunEnemyHitReaction(TrainCannon cannon);
    void SpawnDebris(TrainCannon cannon, int count);
    void ApplyTileEffect(int effectId, int x, int y);
    void DropItem(TrainCannon cannon, int itemId);
    void SpawnExplosionEffect(TrainCannon cannon, int xOffset, int yOffset, int size);
    void ClearObjectState(TrainCannon cannon);
    void DespawnObject(TrainCannon cannon);
    TrainCannonShot? TryGetFreeShot();
    TrainCannonVisual? TryGetFreeVisual();
}
