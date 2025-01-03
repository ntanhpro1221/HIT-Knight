using System;

public class MonsterStatsHandler : ActorStatsHandler {
    protected override void InitStats() {
        foreach (ActorStatType stat in Enum.GetValues(typeof(ActorStatType)))
            SetRawStat(stat, DataManager.Instance.SystemData.enemyData[Actor.ActorId].stats[stat]);
    }
}