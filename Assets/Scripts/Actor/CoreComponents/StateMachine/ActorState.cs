using System;

public class ActorState : IState {
    protected new ActorSM sm => base.sm as ActorSM;
    protected AnimInfo animInfo;
    protected IActor actor;
    protected ActorBodyHandler bodyHandler;
    protected IActorControlHelper controlHelper;
    protected ActorNavigator navigator;
    protected ActorMoveHandler moveHandler;

    private HealthHandler healthHandler;

    [Obsolete(__GeneralStateWarning)]
    public ActorState(ActorSM sm, AnimInfo animInfo, IActor actor) : base(animInfo.Name, sm) {
        this.animInfo = animInfo;
        this.actor = actor;
        this.bodyHandler = actor.BodyHandler;
        this.controlHelper = actor.ControlHelper;
        this.healthHandler = actor.HealthHandler;
        this.navigator = actor.Navigator;
        this.moveHandler = actor.MoveHandler;
    }

    private IState GetNextState_This() {
        /* To Dead
         * -- just dead */
        if (healthHandler.IsDead)
            return sm.DeadState;

        return null;
    }

    public override IState GetNextState() => GetNextState_This();
}
