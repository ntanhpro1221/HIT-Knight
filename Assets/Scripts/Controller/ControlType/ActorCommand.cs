
/// <summary>
/// Type of control of actor
/// </summary>
public enum ActorCommand {
    /// <summary>
    /// param: (Vector2 pos)
    /// </summary>
    MoveByPos,
    /// <summary>
    /// param: (Vector2 dir)
    /// </summary>
    MoveByDir,
    /// <summary>
    /// param: null
    /// </summary>
    Idle,
    /// <summary>
    /// param: Vector2 dir
    /// </summary>
    Dash,
    /// <summary>
    /// param: null
    /// </summary>
    Attack
}
