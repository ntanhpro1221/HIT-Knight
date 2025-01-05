using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

/// <summary>
/// Base of actor
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SortingGroup))]
public abstract class IActor : MonoBehaviour {
    #region CONFIG
    [field: SerializeField] public string ActorId { get; private set; } = "001";
    #endregion

    #region CORE COMPONENT
    private Core m_Core;
    public Core Core => m_Core ??= GetComponentsInChildren<Core>().FirstOrDefault(cpn => transform == cpn.transform.parent);
    /// <summary>
    /// Use weapon
    /// </summary>
    public WeaponHandler WeaponHandler 
        => Core.GetCoreComponent<WeaponHandler>();
    /// <summary>
    /// Manage health
    /// </summary>
    public HealthHandler HealthHandler 
        => Core.GetCoreComponent<HealthHandler>();
    /// <summary>
    /// Manage movement
    /// </summary>
    public ActorMoveHandler MoveHandler 
        => Core.GetCoreComponent<ActorMoveHandler>();
    /// <summary>
    /// Stalk object to attack
    /// </summary>
    public IStalker Stalker 
        => Core.GetCoreComponent<IStalker>();
    /// <summary>
    /// Manage state
    /// </summary>
    public ActorSM StateMachine 
        => Core.GetCoreComponent<ActorSM>();
    /// <summary>
    /// Manage stats
    /// </summary>
    public ActorStatsHandler StatsHandler 
        => Core.GetCoreComponent<ActorStatsHandler>();
    /// <summary>
    /// Manage body
    /// </summary>
    public ActorBodyHandler BodyHandler 
        => Core.GetCoreComponent<ActorBodyHandler>();
    /// <summary>
    /// Control adapter
    /// </summary>
    public IActorControlHelper ControlHelper 
        => Core.GetCoreComponent<IActorControlHelper>();
    /// <summary>
    /// Control direction
    /// </summary>
    public ActorNavigator Navigator 
        => Core.GetCoreComponent<ActorNavigator>();

    public Rigidbody2D RB 
        => GetComponent<Rigidbody2D>();

    public NavMeshAgent Agent
        => GetComponent<NavMeshAgent>();
    #endregion
    
    protected virtual void Awake() {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        Agent.acceleration = 1e9f;
        Agent.autoBraking = false;
    }
}

