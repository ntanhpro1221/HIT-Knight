using UnityEngine;
public class ActorBase : MonoBehaviour {
    protected Core core;
    public Attacker attacker;
    public HealthHandler healthHandler;
    public Mover mover;
    public ActorStalker stalker;
    public StatsHandler statsHandler;
    public ActorModel actorModel;
}
