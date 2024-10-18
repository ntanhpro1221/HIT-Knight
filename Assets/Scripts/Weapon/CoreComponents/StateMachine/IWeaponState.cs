public abstract class IWeaponState : IState {
    protected IWeaponState(IStateMachine sm, AnimInfo anim, IAnimUsable animUser) : base(sm, anim, animUser) {
    }
}
