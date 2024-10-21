using UnityEngine;

/// <summary>
/// Base of ranged weapon.
/// </summary>
public abstract class IRangedWeapon : IWeapon {
    /// <summary>
    /// Bullet that will be used when perform long range attack.
    /// </summary>
    [SerializeField] protected GameObject bulletObj;
    /// <summary>
    /// Perform long range attack.
    /// </summary>
    public abstract void RangedAttack();
}
