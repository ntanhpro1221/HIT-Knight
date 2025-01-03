public class WeaponSM : IStateMachine {
    protected new WeaponCore Core => base.Core as WeaponCore;
    public IWeapon Weapon => Core.Weapon;

    #region STATE
    protected override IState DefState => IdleState;

    public IState MeleeAttackState { get; protected set; }
    public IState IdleState { get; protected set; }
    public IState AimState { get; protected set; }

    protected virtual void InitAllState() {
        MeleeAttackState = new MeleeAttackState_Weapon(this, WeaponBodyHandler.MeleeAttack, Weapon);
        IdleState = new IdleState_Weapon(this, WeaponBodyHandler.Idle, Weapon);
        AimState = new AimState_Weapon(this, WeaponBodyHandler.Aim, Weapon);
    }
    #endregion

    protected override void Awake() {
        base.Awake();
        InitAllState();
    }
}
