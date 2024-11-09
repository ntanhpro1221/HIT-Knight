public class ActorAnyState : IActorState {
    public ActorAnyState(IStateMachine sm, AnimInfo anim, IAnimUsable animUser) : base(sm, anim, animUser) {
    }
}
