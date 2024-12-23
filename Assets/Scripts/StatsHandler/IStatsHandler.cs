using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public abstract class IStatsHandler<T> : CoreComponent where T : Enum {
    // lưu các buff hiện tại
    private List<BuffStaticData<T>> buffs = new List<BuffStaticData<T>>();
    // lưu chỉ số gốc
    public PropertySet<T, BindableProperty<float>> RawStats { get; }
    // cs cuối ( chỉ số sau khi đã áp dụng buff)
    public PropertySet<T, BindableProperty<float>> CurStats { get; }
    public IStatsHandler() {
        RawStats = new PropertySet<T, BindableProperty<float>>();
        CurStats = new PropertySet<T, BindableProperty<float>>();
    }
    public void AddBuff(BuffStaticData<T> buff) {
        buffs.Add(buff);
        UpdateStats();
    }
    private void RemoveBuff(BuffStaticData<T> buff) {
        buffs.Remove(buff);
        UpdateStats();
    }

    public void SetRawStat(T type, float value) {
        RawStats[type].Value = value;
        UpdateStats();
    }

    private void UpdateStats() {
        foreach (T statType in Enum.GetValues(typeof(T))) {
            float rawStat = RawStats[statType].Value;
            float addBuff = buffs.Where(b => b.statType.Equals(statType) && b.buffType == BuffType.Add).Sum(b => b.value);
            float mulBuff = buffs.Where(b => b.statType.Equals(statType) && b.buffType == BuffType.Mul).Sum(b => b.value);

            float finalStat = rawStat + addBuff + (mulBuff * rawStat);

            CurStats[statType].Value = finalStat;
        }

        // tự động loại bỏ các buff hết thời gian
        buffs.RemoveAll(b => b.existTime <= 0);
    }

    // cập nhật thời gian tồn tại của buff
    public void Update() {
        foreach (BuffStaticData<T> buff in buffs) {
            //giảm thời gian tồn tại của buff
            buff.existTime -= Time.deltaTime;
        }
        UpdateStats();
    }
}