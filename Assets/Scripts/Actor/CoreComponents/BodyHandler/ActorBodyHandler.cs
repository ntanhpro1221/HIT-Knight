/// <summary>
/// Common body handler of actor
/// </summary>
public class ActorBodyHandler : IBodyHandler<ActorAnimEventType> {
    public static readonly AnimInfo Idle = new("Idle");
    public static readonly AnimInfo Move = new("Move");
    public static readonly AnimInfo Dash = new("Dash");
    public static readonly AnimInfo Dead = new("Dead"); 
    public static readonly AnimInfo Attack = new("Attack");
}
