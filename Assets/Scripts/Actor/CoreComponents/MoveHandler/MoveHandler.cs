using System;
using UnityEngine;

/// <summary>
/// Manage actor movement through rigidbody
/// </summary>
public class MoveHandler : CoreComponent, IMoveHandler
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Move actor with the given direction
    /// </summary>
    public Vector2 Velocity => rb.velocity;
    public virtual void MoveByDir(Vector2 dir)
    {
        rb.velocity = dir.normalized * moveSpeed;
    }
    /// <summary>
    /// Move actor to the given position
    /// </summary>
    public void MoveByPos(Vector2 pos)
    {
        Vector2 direction = (pos - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }
    /// <summary>
    /// Dash with the given direction
    /// </summary>
    public void Dash(Vector2 dir)
    {
        rb.velocity = dir.normalized * dashSpeed;
    }
    /// <summary>
    /// Stop all actor's movement immediately
    /// </summary>
    public void StopMove()
    {
        rb.velocity = Vector2.zero;
    }
}