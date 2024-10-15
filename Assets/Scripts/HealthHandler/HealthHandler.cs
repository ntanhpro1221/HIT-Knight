using System;
using UnityEngine;

/// <summary>
/// Manage current health of actor.
/// </summary>
public class HealthHandler : CoreComponent, IHealthHandler
{
    [SerializeField] protected float maxHp;

    private readonly BindableProperty<float> curHp = new BindableProperty<float>();
    public bool IsDead => curHp.Value <= 0;


    public void Init(float newMaxHp)
    {
        this.maxHp = newMaxHp;
        curHp.Value = maxHp;
    }
    public BindableProperty<float> CurHealth => curHp;
    public void Heal(float amount)
    {
        curHp.Value = Mathf.Min( curHp.Value + amount,maxHp);
    }

    public void TakeDamage(float damage)
    {
        curHp.Value = Mathf.Max(curHp.Value - damage, 0);
    }
}
