using UnityEngine;

/// <summary>
/// Manage current health of actor.
/// </summary>
public class HealthHandler : CoreComponent, IHealthHandler {
    public bool IsDead => throw new System.NotImplementedException();

    public BindableProperty<float> CurHealth => throw new System.NotImplementedException();

    public void Heal(float amount) {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage) {
        throw new System.NotImplementedException();
    }
}
