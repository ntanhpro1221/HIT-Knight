public interface IStatsHandlable {
    ActorStats<Observable<float>> CurStats { get; }
    void AddBuff(Buff buff);
    void SetRawStat(StatType type, float value);
}
