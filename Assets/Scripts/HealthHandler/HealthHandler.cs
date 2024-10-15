using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Manage current health of actor.
/// </summary>
public class HealthHandler : CoreComponent, IHealthHandler {
    private BindableProperty<float> m_MaxHealth;
    private readonly BindableProperty<float> m_CurHealth = new();

    private void OnMaxHpChanged(float newMaxHp) {
        m_CurHealth.Value = Mathf.Min(m_CurHealth.Value, newMaxHp);
    }
    private void ChangeHealthSource(BindableProperty<float> newHealthSource) {
        if (newHealthSource is null) {
            Debug.Log(this.GetType().Name + ": Health source is null");
            return;
        }
        m_MaxHealth?.OnChanged.RemoveListener(OnMaxHpChanged);
        m_MaxHealth = newHealthSource;
        m_MaxHealth.OnChanged.AddListener(OnMaxHpChanged);
        m_CurHealth.Value = m_MaxHealth.Value;
    }

    public void Init(BindableProperty<float> newHealthSource) => ChangeHealthSource(newHealthSource);
    public bool IsDead => m_CurHealth.Value <= 0;
    public BindableProperty<float> CurHealth => m_CurHealth;
    public void Heal(float amount) => m_CurHealth.Value = Mathf.Min(m_CurHealth.Value + amount, m_MaxHealth.Value);
    public void TakeDamage(float damage) => m_CurHealth.Value = Mathf.Max(m_CurHealth.Value - damage, 0);
}
