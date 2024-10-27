using UnityEngine;

/// <summary>
/// Manage actor movement through rigidbody
/// </summary>
public interface IMoveHandler {
    /// <summary>
    /// Current velocity
    /// </summary>
    Vector2 Velocity { get; }
    /// <summary>
    /// Move actor with the given direction
    /// </summary>
    void MoveByDir(Vector2 dir);
    /// <summary>
    /// Move actor to the given position
    /// </summary>
    void MoveByPos(Vector2 pos);
    /// <summary>
    /// Dash with the given direction
    /// </summary>
    void Dash(Vector2 dir);
    /// <summary>
    /// Stop all actor's movement immediately
    /// </summary>
    void StopMove();
}

