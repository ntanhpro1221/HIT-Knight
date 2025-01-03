public class WeaponCommander : Commander<WeaponCommand> {
    protected new ActorCore Core => base.Core as ActorCore;
    public IActor Actor => Core.Actor;
}