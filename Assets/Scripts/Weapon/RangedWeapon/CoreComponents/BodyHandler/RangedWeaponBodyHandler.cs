using UnityEngine;

/// <summary>
/// Common body handler of ranged weapon
/// </summary>
public class RangedWeaponBodyHandler : WeaponBodyHandler {
    [field: SerializeField] public Transform BulletStartPoint { get; private set; }
    public static readonly AnimInfo RangedAttack = new("RangedAttack");
}

