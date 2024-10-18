using UnityEngine;

/// <summary>
/// Manage actor movement through rigidbody
/// </summary>
public class MoveHandler : CoreComponent, IMoveHandler {
    /// <summary>
    /// Current velocity
    /// </summary>
    public Vector2 Velocity => throw new System.NotImplementedException();
    /// <summary>
    /// Move actor with the given direction
    /// </summary>
    public virtual void MoveByDir(Vector2 dir) {
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// Move actor to the given position
    /// </summary>
    public void MoveByPos(Vector2 pos) {
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// Dash with the given direction
    /// </summary>
    public void Dash(Vector2 dir) {
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// Stop all actor's movement immediately
    /// </summary>
    public void StopMove() {
        throw new System.NotImplementedException();
    }
}