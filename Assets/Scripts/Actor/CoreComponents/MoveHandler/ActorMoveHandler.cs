using UnityEngine;
using UnityEngine.AI;

public class ActorMoveHandler : MoveHandler {
    protected new ActorCore Core => base.Core as ActorCore;
    public IActor Actor => Core.Actor;

    protected override Rigidbody2D RB 
        => Actor.RB;

    protected override NavMeshAgent Agent 
        => Actor.Agent;

    protected override BindableProperty<float> MoveSpeed 
        => GetCoreComponent<ActorStatsHandler>().CurStats[ActorStatType.MoveSpeed];
}
