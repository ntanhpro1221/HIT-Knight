public interface IHealthHandlable {
    bool IsDead { get; }
    void TakeDamage(float damage);
    void Heal(float amount);
}
