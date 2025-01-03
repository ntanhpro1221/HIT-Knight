public class RangedWeaponSM : WeaponSM {
    protected new IRangedWeapon Weapon => base.Weapon as IRangedWeapon;

    #region STATE
    public IState RangedAttackState { get; protected set; }

    protected override void InitAllState() {
        base.InitAllState();

        MeleeAttackState = new MeleeAttackState_RangedWeapon(this, RangedWeaponBodyHandler.MeleeAttack, Weapon);
        IdleState = new IdleState_RangedWeapon(this, RangedWeaponBodyHandler.Idle, Weapon);
        AimState = new AimState_RangedWeapon(this, RangedWeaponBodyHandler.Aim, Weapon);
        RangedAttackState = new RangedAttackState_RangedWeapon(this, RangedWeaponBodyHandler.RangedAttack, Weapon);
    }
    #endregion
} 
