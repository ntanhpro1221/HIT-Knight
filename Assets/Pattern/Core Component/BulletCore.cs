public class BulletCore : Core {
    private IWeapon m_Weapon;
    public IWeapon Weapon
        => m_Weapon ??= transform.parent.GetComponent<IWeapon>();
}
