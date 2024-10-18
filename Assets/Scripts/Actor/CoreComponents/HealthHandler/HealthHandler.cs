using UnityEngine;

public class HealthHandler : CoreComponent, IHealthHandlable {
    public bool IsDead => throw new System.NotImplementedException();

    public void Heal(float amount) {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage) {
        throw new System.NotImplementedException();
    }
}
