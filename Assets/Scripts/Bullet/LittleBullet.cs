using System;
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
        healthEnemy = m_Core.GetCoreComponent<HealthHandler>();
        healthEnemy.CurHealth.Value -= _weapon.StatsHandler.CurStats[WeaponStatType.Atk].Value;
        Destroy(gameObject);
    }
}
