using UnityEngine;

/// <summary>
/// Bullet object.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class IBullet : MonoBehaviour {
    protected Core m_Core;
    /// <summary>
    /// Manage body
    /// </summary>
    public BulletBodyHandler BodyHandler { get; set; }
    /// <summary>
    /// Launch this bullet
    /// </summary>
    public virtual void Launch(Transform root, Vector2 dir, IRangedWeapon weapon) {
        throw new System.NotImplementedException();
    }
}
