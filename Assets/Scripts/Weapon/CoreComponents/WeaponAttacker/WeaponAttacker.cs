using System;
using System.Collections;
using UnityEngine;

public class WeaponAttacker : WeaponCoreComponent {
    protected BindableProperty<float> CurDamage 
        => GetCoreComponent<WeaponStatsHandler>().CurStats[WeaponStatType.Atk];

    public float CurCooldown { get; private set; }

    public float MaxCooldown => 1.0f / Weapon.StatsHandler.CurStats[WeaponStatType.Speed].Value;

    /// <summary>
    /// There are two phases when attacking: before and after you actually deal damage 
    /// (or when the bullet is actually fired if you use a ranged weapon).
    ///  This variable will be true if you are in the first phase
    /// </summary>
    [field: SerializeField]
    public bool IsBeginAttack { get; protected set; } = false;
    /// <summary>
    /// Done attack
    /// </summary>
    [field: SerializeField]
    public bool IsDoneAttack { get; protected set; } = true;
    /// <summary>
    /// Something relative to attack speed
    /// </summary>
    public virtual bool IsReadyToAttack => IsDoneAttack == true;

    protected virtual void Awake() {
        Weapon.OnTouchedTarget.AddListener(OnTouchedTarget);

        BindAnimEvents();
    }
    
    protected virtual void Update() {
        CurCooldown = Math.Max(-1, CurCooldown - Time.deltaTime);
    }

    protected void Attack(Action onStart, Action onDone) {
        onStart.Invoke();
        IEnumerator Cooldown() {
            yield return new WaitForSeconds(MaxCooldown);
            onDone.Invoke();
        }
        StartCoroutine(Cooldown());
    }

    protected virtual void BindAnimEvents() {
        WeaponBodyHandler bodyHandler = GetCoreComponent<WeaponBodyHandler>();
        bodyHandler.RegisterAnimEvent(WeaponAnimEventType.ON_DONE_BEGIN_ATTACK, () => {
            IsBeginAttack = false;
        });
    }
    
    protected virtual void OnTouchedTarget(Collider2D coll) {
        // valid target
        if (IsDoneAttack == true) return;

        if (Weapon.WeaponHandler.Stalker.ValidateTarget_FromTheBeginning(coll) is false) return;
        GameObject target = Weapon.WeaponHandler.Stalker.ToTargetType(coll);
        if (Weapon.WeaponHandler.Stalker.ValidateTarget_ByCurBehaviour(target) is false) return;

        IHealthHandler targetHealth = target.GetComponent<IActor>().HealthHandler;
        targetHealth.TakeDamage(CurDamage.Value);
    }
    
    protected virtual void OnStart_Melee() {
        IsBeginAttack = true;
        IsDoneAttack = false;
        CurCooldown = MaxCooldown;
    }

    protected virtual void OnDone_Melee() {
        IsDoneAttack = true;
    }

    public void MeleeAttack() 
        => Attack(OnStart_Melee, OnDone_Melee);

    public virtual void StopAttack() {
        IsBeginAttack = false;
    }
}
