using UnityEngine;

public class ActorNavigator : Navigator {
    protected new ActorCore Core => base.Core as ActorCore;
    public IActor Actor => Core.Actor;

    protected override Transform Root
        => Actor.transform;

    protected override SpriteRenderer SR
        => GetCoreComponent<ActorBodyHandler>().GetComponent<SpriteRenderer>();
}
