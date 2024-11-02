﻿/// <summary>
/// Manage current health of actor.
/// </summary>
public interface IHealthHandler {
    bool IsDead { get; }
    BindableProperty<float> CurHealth { get; }
    void TakeDamage(float damage);
    void Heal(float amount);
}