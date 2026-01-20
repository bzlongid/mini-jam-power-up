#nullable enable

using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    Single,
    Triple,
    Shotgun,
    DoubleShotgun,
    Hadouken,
    TwinSnakes,
    BigLazer
}

public enum AmmoPrefab
{
    SphereSmall,
    SphereLarge,
    Capsule,
}

public enum AmmoSpread
{
    Single,
    TripleStraight,
    TripeAngled,
    TripeAngledDoubleLayer,
    TwinSnakes,
}

public static class CombatVariables
{
    public static readonly Dictionary<Weapon, WaitForSeconds> WeaponReloadTimeMap = new Dictionary<Weapon, WaitForSeconds>
    {
        { Weapon.Single, new WaitForSeconds(0.4f) },
        { Weapon.Triple, new WaitForSeconds(0.8f) },
        { Weapon.Shotgun, new WaitForSeconds(0.8f) },
        { Weapon.DoubleShotgun, new WaitForSeconds(0.714f) },
        { Weapon.Hadouken, new WaitForSeconds(0.556f) },
        { Weapon.TwinSnakes, new WaitForSeconds(0.833f) },
        { Weapon.BigLazer, new WaitForSeconds(0.1f) },
    };

    public static readonly Dictionary<Weapon, AmmoPrefab> WeaponPrefabMap = new Dictionary<Weapon, AmmoPrefab>
    {
        { Weapon.Single, AmmoPrefab.Capsule },
        { Weapon.Triple, AmmoPrefab.Capsule },
        { Weapon.Shotgun, AmmoPrefab.SphereSmall },
        { Weapon.DoubleShotgun, AmmoPrefab.SphereSmall },
        { Weapon.Hadouken, AmmoPrefab.SphereLarge },
        { Weapon.TwinSnakes, AmmoPrefab.SphereLarge },
        { Weapon.BigLazer, AmmoPrefab.SphereLarge },
    };

    public static readonly Dictionary<Weapon, AmmoSpread> WeaponSpreadMap = new Dictionary<Weapon, AmmoSpread>
    {
        { Weapon.Single, AmmoSpread.Single },
        { Weapon.Triple, AmmoSpread.TripleStraight },
        { Weapon.Shotgun, AmmoSpread.TripeAngled },
        { Weapon.DoubleShotgun, AmmoSpread.TripeAngledDoubleLayer },
        { Weapon.Hadouken, AmmoSpread.Single },
        { Weapon.TwinSnakes, AmmoSpread.TwinSnakes },
        { Weapon.BigLazer, AmmoSpread.Single },
    };

    public static float KNOCKBACK_MULTIPLIER = 100f;
    public static float? GetPlayerKnockback(Weapon ammo)
    {
        switch (ammo)
        {
            case Weapon.TwinSnakes:
                float twinSnakesKnockbackForce = 0.75f;
                return twinSnakesKnockbackForce * KNOCKBACK_MULTIPLIER;

            case Weapon.BigLazer:
                float bigLazerKnockbackForce = 0.8f;
                return bigLazerKnockbackForce * KNOCKBACK_MULTIPLIER;

            default:
                return null;
        }
    }
}
