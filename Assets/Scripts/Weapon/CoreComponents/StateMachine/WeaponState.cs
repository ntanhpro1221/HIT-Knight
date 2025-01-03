using System;

public class WeaponState : IState {
    protected new WeaponSM sm => base.sm as WeaponSM;
    protected AnimInfo animInfo;
    protected IWeapon weapon;
    protected WeaponBodyHandler bodyHandler;
    protected IWeaponControlHelper controlAdapter;
    protected Navigator navigator;

    protected IStalker stalker => weapon.WeaponHandler.Stalker;
    protected BindableProperty<float> curMP => weapon.WeaponHandler.CurMP;

    [Obsolete(__GeneralStateWarning)]
    public WeaponState(WeaponSM sm, AnimInfo animInfo, IWeapon weapon) : base(animInfo.Name, sm) {
        this.animInfo = animInfo;
        this.weapon = weapon;
        this.bodyHandler = weapon.BodyHandler;
        this.controlAdapter = weapon.ControlHelper;
        this.navigator = weapon.Navigator;
    }
}
