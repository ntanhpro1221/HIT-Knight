using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bullet object.
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent((typeof(Rigidbody2D)))]
public abstract class IBullet : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Core m_Core;
    protected IRangedWeapon _weapon;
    /// <summary>
    /// Manage body
    /// </summary>
    public BulletBodyHandler BodyHandler { get; set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// Launch this bullet
    /// </summary>
    public virtual void Launch(Transform root, Vector2 dir, IRangedWeapon weapon)
    {
        if (!weapon)
        {
            Debug.LogWarning("Weapon is NULL");
            return;
        }
        rb.position = root.position;
        rb.velocity = dir.normalized * weapon.StatsHandler.CurStats[WeaponStatType.BulletSpeed].Value;
        _weapon = weapon;
    }
}
