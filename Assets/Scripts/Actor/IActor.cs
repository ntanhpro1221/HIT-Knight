using UnityEngine;

/// <summary>
/// Base of actor
/// </summary>
public abstract class IActor : MonoBehaviour {
    protected Core core;
    /// <summary>
    /// Use weapon
    /// </summary>
    public IWeaponHandler weaponHandler;
    /// <summary>
    /// Manage health
    /// </summary>
    public IHealthHandler healthHandler;
    /// <summary>
    /// Manage movement
    /// </summary>
    public IMoveHandler movementHandler;
    /// <summary>
    /// Stalk object to attack
    /// </summary>
    public IStalker stalker;
    /// <summary>
    /// Manage state
    /// </summary>
    public IStateMachine stateMachine;
    /// <summary>
    /// Manage stats
    /// </summary>
    public ActorStatsHandler statsHandler;
    /// <summary>
    /// Manage body
    /// </summary>
    public ActorBodyHandler bodyHandler;
}
