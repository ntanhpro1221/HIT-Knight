using UnityEngine;

/// <summary>
/// Base of actor
/// </summary>
public abstract class IActor : MonoBehaviour {
    protected Core m_Core;
    /// <summary>
    /// Use weapon
    /// </summary>
    public IWeaponHandler WeaponHandler { get; set; }
    /// <summary>
    /// Manage health
    /// </summary>
    public IHealthHandler HealthHandler { get; set; }
    /// <summary>
    /// Manage movement
    /// </summary>
    public IMoveHandler MovementHandler { get; set; }
    /// <summary>
    /// Stalk object to attack
    /// </summary>
    public IStalker Stalker { get; set; }
    /// <summary>
    /// Manage state
    /// </summary>
    public IActorSM StateMachine { get; set; }
    /// <summary>
    /// Manage stats
    /// </summary>
    public ActorStatsHandler StatsHandler { get; set; }
    /// <summary>
    /// Manage body
    /// </summary>
    public ActorBodyHandler BodyHandler { get; set; }
}
