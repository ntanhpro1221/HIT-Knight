using UnityEngine;

/// <summary>
/// Base of weapon object
/// </summary>
public abstract class IWeapon : MonoBehaviour {
    protected Core core;
    /// <summary>
    /// manage state
    /// </summary>
    public IStateMachine stateMachine;
    /// <summary>
    /// manage stat
    /// </summary>
    public WeaponStatsHandler statsHandler;
    /// <summary>
    /// manage body
    /// </summary>
    public WeaponBodyHandler bodyHandler;
    /// <summary>
    /// Perform close range attack
    /// </summary>
    public abstract void MeleAttack();
}

