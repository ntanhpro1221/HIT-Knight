public class WeaponCoreComponent : CoreComponent {
    protected new WeaponCore Core => base.Core as WeaponCore;
    public IWeapon Weapon => Core.Weapon;
}
