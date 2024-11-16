using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Manage current health of actor.
/// </summary>
public class HealthHandler : CoreComponent, IHealthHandler
{
    protected BindableProperty<float> maxHp = new BindableProperty<float>();

    private readonly BindableProperty<float> curHp = new BindableProperty<float>();
    
    public bool IsDead => curHp.Value <= 0;
    public void Init(BindableProperty<float> newMaxHp)
    {
        maxHp = newMaxHp;
        curHp.Value = maxHp.Value;
    }

    public void ChangeMaxHp(BindableProperty<float> newMax)
    {
        maxHp.OnChanged.RemoveListener(OnMaxHpChanged);
        maxHp.OnChanged.AddListener(OnMaxHpChanged);
        maxHp.Value = newMax.Value;
    }

    protected void OnMaxHpChanged(float newHp)
    {
        curHp.Value = Mathf.Min(curHp.Value, newHp);
    }
    public BindableProperty<float> CurHealth => curHp;
    public void Heal(float amount)
    {
        curHp.Value = Mathf.Min( curHp.Value + amount,maxHp.Value);
    }
    
    public void TakeDamage(float damage)
    {
        curHp.Value = Mathf.Max(curHp.Value - damage, 0);
    }
}
