using UnityEngine;

/// <summary>
/// Bullet object.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class IBullet : MonoBehaviour {
    /// <summary>
    /// Launch this bullet
    /// </summary>
    public virtual void Launch(Transform root, Vector2 dir, float speed) {
        throw new System.NotImplementedException();
    }
}
