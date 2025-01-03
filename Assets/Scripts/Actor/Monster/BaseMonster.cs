public class BaseMonster : IActor {
    /// <summary>
    /// brain of monster
    /// </summary>
    public ActorCommander Commander 
        => Core.GetCoreComponent<ActorCommander>();
}
