using System;

/// <summary>
/// Manage stats of actor with all buff
/// </summary>
public class ActorStatsHandler : IStatsHandler<ActorStatType> {
    protected new ActorCore Core => base.Core as ActorCore;
    public IActor Actor => Core.Actor;

    protected override void InitStats() {
        foreach (ActorStatType stat in Enum.GetValues(typeof(ActorStatType)))
            SetRawStat(stat, DataManager.Instance.SystemData.heroData[Actor.ActorId].stats[stat]);
    }
}
