using UnityEngine;

/// <summary>
/// Base of weapon object
/// </summary>
public abstract class IWeapon : MonoBehaviour {
    protected Core m_Core;
    /// <summary>
    /// manage state
    /// </summary>
    public IWeaponSM StateMachine { get; set; }
    /// <summary>
    /// manage stat
    /// </summary>
    public WeaponStatsHandler StatsHandler { get; set; }
    /// <summary>
    /// manage body
    /// </summary>
    public WeaponBodyHandler BodyHandler { get; set; }
    /// <summary>
    /// Perform close range attack
    /// </summary>
    public abstract void MeleAttack();
}

