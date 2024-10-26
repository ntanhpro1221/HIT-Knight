using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float curHealth;

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            curHealth = maxHealth;
        }
    }

    public float CurHealth => curHealth;

    public bool IsDead()
    {
        return curHealth <= 0;
    }

    public void TakeDame(float damage)
    {
        curHealth -= damage;
    }

    public void Health()
    {
        curHealth = maxHealth;
    }

    protected void OnDead()
    {
        // xử lý hành động khi actor chết
    }
}
