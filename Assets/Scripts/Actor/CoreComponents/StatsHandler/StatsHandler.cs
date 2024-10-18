public class StatsHandler : CoreComponent, IStatsHandlable {
    public ActorStats<Observable<float>> CurStats => throw new System.NotImplementedException();

    public void AddBuff(Buff buff) {
        throw new System.NotImplementedException();
    }

    public void SetRawStat(StatType type, float value) {
        throw new System.NotImplementedException();
    }
}