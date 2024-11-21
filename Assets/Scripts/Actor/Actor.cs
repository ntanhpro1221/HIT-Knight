using UnityEngine;

public class Actor : IActor
{
    private void Awake()
    {
        WeaponHandler = GetComponent<IWeaponHandler>();
        HealthHandler = GetComponent<IHealthHandler>();
        MovementHandler = GetComponent<IMoveHandler>();
        Stalker = GetComponent<IStalker>();
        StateMachine = GetComponent<IActorSM>();
        StatsHandler = GetComponent<ActorStatsHandler>();
        BodyHandler = GetComponent<ActorBodyHandler>();
        
        
    }

}