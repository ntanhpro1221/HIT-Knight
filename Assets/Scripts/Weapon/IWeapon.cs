using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base of weapon object
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class IWeapon : MonoBehaviour {
    [field: SerializeField] public string WeaponId { get; private set; } = "001";

    /// <summary>
    /// WeaponHandler that handle this weapon now
    /// </summary>
    public WeaponHandler WeaponHandler { get; protected set; }

    #region CORE COMPONENT
    private Core m_Core; 
    public Core Core => m_Core ??= GetComponentsInChildren<Core>().FirstOrDefault(cpn => transform == cpn.transform.parent);
    /// <summary>
    /// manage state
    /// </summary>
    public WeaponSM StateMachine 
        => Core.GetCoreComponent<WeaponSM>();
    /// <summary>
    /// manage stat
    /// </summary>
    public WeaponStatsHandler StatsHandler 
        => Core.GetCoreComponent<WeaponStatsHandler>();
    /// <summary>
    /// manage body
    /// </summary>
    public WeaponBodyHandler BodyHandler 
        => Core.GetCoreComponent<WeaponBodyHandler>();
    /// <summary>
    /// manage attack of this weapon
    /// </summary>
    public WeaponAttacker Attacker 
        => Core.GetCoreComponent<WeaponAttacker>();
    /// <summary>
    /// Control adapter
    /// </summary>
    public IWeaponControlHelper ControlHelper 
        => Core.GetCoreComponent<IWeaponControlHelper>();
    /// <summary>
    /// Control direction
    /// </summary>
    public WeaponNavigator2D Navigator
        => Core.GetCoreComponent<WeaponNavigator2D>();

    #region RIGID BODY
    public Rigidbody2D RB 
        => GetComponent<Rigidbody2D>();
    public UnityEvent<Collider2D> OnTouchedTarget { get; } = new();
    private void OnTriggerEnter2D(Collider2D coll)
        => OnTouchedTarget.Invoke(coll);
    #endregion
    #endregion

    public void Init(WeaponHandler weaponHandler) {
        UnbindCommander();
        WeaponHandler = weaponHandler;
        Core.GetCoreComponent<IWeaponControlHelper>().Init(
            Core.GetCoreComponent<WeaponSM>().UpdateState, weaponHandler);
    }
    
    public void UnbindCommander() {
        if (WeaponHandler == null) return;
        ControlHelper.UnbindCommander(Core.GetCoreComponent<WeaponSM>().UpdateState, WeaponHandler);
        WeaponHandler = null;
    }

    protected virtual void Awake() { }
}

