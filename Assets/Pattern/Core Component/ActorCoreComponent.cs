public class ActorCoreComponent : CoreComponent {
    protected new ActorCore Core => base.Core as ActorCore;
    public IActor Actor => Core.Actor;
}
