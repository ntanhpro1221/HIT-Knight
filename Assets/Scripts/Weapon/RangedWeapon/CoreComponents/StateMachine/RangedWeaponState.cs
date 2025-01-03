using System;

public class RangedWeaponState : WeaponState {
    protected new RangedWeaponSM sm => base.sm as RangedWeaponSM;
    protected new IRangedWeapon weapon => base.weapon as IRangedWeapon;
    protected new RangedWeaponBodyHandler bodyHandler => base.bodyHandler as RangedWeaponBodyHandler;

    protected bool EnoughMPToRangedAttack 
        => weapon.WeaponHandler.EnoughMPToRangedAttack;
    protected bool EnoughDistanceToRangedAttack 
        => weapon.WeaponHandler.EnoughDistanceToRangedAttack;

    [Obsolete(__GeneralStateWarning)]
    public RangedWeaponState(RangedWeaponSM sm, AnimInfo animInfo, IRangedWeapon weapon) : base(sm, animInfo, weapon) { }
}
