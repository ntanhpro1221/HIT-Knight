using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bullet object.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class IBullet : MonoBehaviour {
    protected Core m_Core;
    protected IRangedWeapon _weapon;
    /// <summary>
    /// Manage body
    /// </summary>
    public BulletBodyHandler BodyHandler { get; set; }
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
        root.Translate(dir*Time.deltaTime*weapon.
            StatsHandler.CurStats[WeaponStatType.BulletSpeed].Value);
        _weapon = weapon;
    }
}
