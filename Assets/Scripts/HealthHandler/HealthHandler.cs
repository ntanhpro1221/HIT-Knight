using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Manage current health of actor.
/// </summary>
public class HealthHandler : CoreComponent, IHealthHandler {
    private ActorStatsHandler StatsHandler => GetCoreComponent<ActorStatsHandler>();
    private bool isHPInitialized = false;

    private void Awake() {
        if (StatsHandler.IsStatsCalculated) {
            isHPInitialized = true;
            CurHealth.Value = MaxHealth.Value;
        } else {
            MaxHealth.OnChanged.AddListener(hp => {
                if (isHPInitialized) return;
                isHPInitialized = true;
                CurHealth.Value = hp;
            });
        }
    }

    public bool IsDead
        => CurHealth.Value <= 0;

    [field: SerializeField] public BindableProperty<float> CurHealth 
        { get; private set; } = new();

    public BindableProperty<float> MaxHealth 
        => GetCoreComponent<ActorStatsHandler>().CurStats[ActorStatType.HP];

    public void Heal(float amount) 
        => CurHealth.Value = Mathf.Min(CurHealth.Value + amount, MaxHealth.Value);
    
    public void TakeDamage(float damage) 
        => CurHealth.Value = Mathf.Max(CurHealth.Value - damage, 0);
}
