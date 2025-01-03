using UnityEngine;

public class WeaponNavigator2D : Navigator2D {
    protected new WeaponCore Core => base.Core as WeaponCore;
    public IWeapon Weapon => Core.Weapon;

    protected override Transform Root
        => Weapon.transform;

    protected override SpriteRenderer SR
        => GetCoreComponent<WeaponBodyHandler>().GetComponent<SpriteRenderer>();
}
