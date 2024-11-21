using UnityEngine;

/// <summary>
/// Bullet object.
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent((typeof(Rigidbody2D)))]
public abstract class IBullet : MonoBehaviour {
    protected Rigidbody2D m_Rb;
    protected Core m_Core;
    protected IRangedWeapon m_Weapon;
    /// <summary>
    /// Manage body
    /// </summary>
    public BulletBodyHandler BodyHandler { get; set; }
    private void Awake() {
        m_Rb = GetComponent<Rigidbody2D>();
        
        m_Core = GetComponentInChildren<Core>();
        BodyHandler = m_Core.GetCoreComponent<BulletBodyHandler>();
    }
    /// <summary>
    /// Launch this bullet
    /// </summary>
    public virtual void Launch(Transform root, Vector2 dir, IRangedWeapon weapon) {
        if (!weapon) {
            Debug.LogWarning("Weapon is NULL");
            return;
        }
        m_Rb.position = root.position;
        m_Rb.velocity = dir.normalized * weapon.StatsHandler.CurStats[WeaponStatType.BulletSpeed].Value;
        m_Weapon = weapon;

        BodyHandler.PlayAnim(BulletBodyHandler.Move);
    }
}
