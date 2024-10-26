
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float curHealth;

    public float CurHealth => curHealth;

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            curHealth = maxHealth;
        }
    }

    public void TakeDame(float damage)
    {
        curHealth -= damage;
    }

    public void Health()
    {
        curHealth = maxHealth;
    }

    public bool IsDead()
    {
        return curHealth <= 0;
    }

    protected void OnDead()
    {
        // thực hiện các hành động trong khi actor chết tại đấy
    }
}
