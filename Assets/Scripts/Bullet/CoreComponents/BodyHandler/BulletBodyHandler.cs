public class BulletBodyHandler : IBodyHandler<BulletAnimEventType> {
    public static readonly AnimInfo Idle = new("Idle");
    public static readonly AnimInfo Move = new("Move");
    public static readonly AnimInfo Explode = new("Explode");
}
