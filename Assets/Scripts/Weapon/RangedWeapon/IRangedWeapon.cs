using UnityEngine;

/// <summary>
/// Base of ranged weapon.
/// </summary>
public abstract class IRangedWeapon : IWeapon {
    #region CORE COMPONENT
    /// <summary>
    /// manage state
    /// </summary>
    public new RangedWeaponSM StateMachine => base.StateMachine as RangedWeaponSM;
    /// <summary>
    /// manage body
    /// </summary>
    public new RangedWeaponBodyHandler BodyHandler => base.BodyHandler as RangedWeaponBodyHandler;
    /// <summary>
    /// manage attack of this weapon
    /// </summary>
    public new RangedWeaponAttacker Attacker => base.Attacker as RangedWeaponAttacker;
    #endregion

    /// <summary>
    /// Bullet that will be used when perform long range attack.
    /// </summary>
    [SerializeField] public GameObject m_BulletObj;
}
