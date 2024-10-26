using UnityEngine;

/// <summary>
/// Manage actor movement (only manage actor's transform)
/// </summary>
public interface IMoveHandler {
    void Move(Vector2 dir);
}

