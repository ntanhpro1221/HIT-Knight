using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ActorStalker : CoreComponent, IStalkable<ActorBase> {
    public Observable<ActorBase> CurrentTarget => throw new System.NotImplementedException();
}
