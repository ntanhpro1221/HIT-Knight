public class ActorCommander : Commander<ActorCommand> {
    protected new ActorCore Core => base.Core as ActorCore;
    public IActor Actor => Core.Actor;
}
