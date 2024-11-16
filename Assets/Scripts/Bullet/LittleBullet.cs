using Unity.VisualScripting;
using UnityEngine;

public class LittleBullet : IBullet {
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        {
            return;
        }
        HealthHandler healthEnemy;
        healthEnemy = other.GetComponent<HealthHandler>();
        if (!healthEnemy)
        {
            healthEnemy = other.GetComponentInChildren<HealthHandler>();
        }

        if (!healthEnemy)
        {
            Debug.LogWarning($"{other.name} does not have a HealthHandler");
            return;
        }
        healthEnemy.CurHealth.Value -= _weapon.StatsHandler.CurStats[WeaponStatType.Atk].Value;
    }
}
