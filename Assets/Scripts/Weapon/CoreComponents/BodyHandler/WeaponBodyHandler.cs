/// <summary>
/// Common body handler of weapon
/// </summary>
public class WeaponBodyHandler : IBodyHandler<WeaponAnimEventType> {
    public static readonly AnimInfo Idle = new("Idle");
    public static readonly AnimInfo Aim = new("Aim");
    public static readonly AnimInfo MeleeAttack = new("MeleeAttack");
}

