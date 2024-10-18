public class WeaponAnyState : IWeaponState {
    public WeaponAnyState(IStateMachine sm, AnimInfo anim, IAnimUsable animUser) : base(sm, anim, animUser) {
    }
}
