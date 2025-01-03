public class ActorSM : IStateMachine {
    protected new ActorCore Core => base.Core as ActorCore;
    public IActor Actor => Core.Actor;

    #region STATE
    protected override IState DefState => IdleState;

    public IState AttackState { get; protected set; }
    public IState DashState { get; protected set; }
    public IState DeadState { get; protected set; }
    public IState IdleState { get; protected set; }
    public IState MoveState { get; protected set; }

    protected virtual void InitAllState() {
        AttackState = new AttackState_Actor(this, ActorBodyHandler.Attack, Actor);
        DashState = new DashState_Actor(this, ActorBodyHandler.Dash, Actor);
        DeadState = new DeadState_Actor(this, ActorBodyHandler.Dead, Actor);
        IdleState = new IdleState_Actor(this, ActorBodyHandler.Idle, Actor);
        MoveState = new MoveState_Actor(this, ActorBodyHandler.Move, Actor);
    }
    #endregion

    protected override void Awake() {
        InitAllState();
    }
}
