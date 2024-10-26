public abstract class IActorState : IState {
    protected IActorState(IStateMachine sm, AnimInfo anim, IAnimUsable animUser) : base(sm, anim, animUser) {
    }
}
