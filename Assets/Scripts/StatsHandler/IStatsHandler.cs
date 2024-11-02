using System;
using System.Collections.Generic;
using System.Linq;

public abstract class IStatsHandler<T> : CoreComponent where T : Enum {
    // lưu các buff hiện tại
    private List<Buff<T>> buffs = new List<Buff<T>>();

    // lưu chỉ số gốc
    private Dictionary<T, float> rawStats = new Dictionary<T, float>();

    // cs cuối
    public PropertySet<T, BindableProperty<float>> CurStats { get; }

    public void AddBuff(Buff<T> buff) {
        buffs.Add(buff);
        UpdateStats();
    }

    public void RemoveBuff(Buff<T> buff) {
        
        buffs.Remove(buff);
        UpdateStats();
    }

    //set default
    public void SetRawStat(T type, float value) {
        rawStats[type] = value;
        UpdateStats();
    }
    // tính chỉ số cuối
    private void UpdateStats() {
        foreach (T statType in Enum.GetValues(typeof(T))) {
            float rawStat = rawStats.ContainsKey(statType) ? rawStats[statType] : 0;
            float addBuff = buffs.Where(b => b.statType.Equals(statType) && b.buffType == BuffType.Add).Sum(b => b.value);
            float mulBuff = buffs.Where(b => b.statType.Equals(statType) && b.buffType == BuffType.Mul).Sum(b => b.value);

            float finalStat = rawStat + addBuff + (mulBuff * rawStat);

            CurStats[statType].Value = finalStat;
        }

        // loại bỏ các buff hết thời gian
        buffs.RemoveAll(b => b.existTime <= 0);
    }
    private void Update() {
        UpdateStats();
    }
}

