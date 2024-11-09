using System;

/// <summary>
/// Manage stats of actor with all buff
/// </summary>
public abstract class IStatsHandler<T> : CoreComponent where T : Enum {
    /// <summary>
    /// Current stats with all buff
    /// </summary>
    public PropertySet<T, BindableProperty<float>> CurStats { get; }
    /// <summary>
    /// Add buff to actor
    /// </summary>
    public void AddBuff(Buff<T> buff) {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Set raw stat of actor (without any buff)
    /// </summary>
    public void SetRawStat(T type, float value) {
        throw new NotImplementedException();
    }
}