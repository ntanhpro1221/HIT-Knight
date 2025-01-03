using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class IStatsHandler<TStatType> : CoreComponent where TStatType : Enum {
    // lưu các buff hiện tại
    [SerializeField] 
    private List<BuffStaticData<TStatType>> buffs = new();
    // lưu chỉ số gốc
    [field: SerializeField] 
    public PropertySet<TStatType, BindableProperty<float>> RawStats { get; private set; } = new();
    // cs cuối ( chỉ số sau khi đã áp dụng buff)
    [field: SerializeField] 
    public PropertySet<TStatType, BindableProperty<float>> CurStats { get; private set; } = new();

    public bool IsStatsCalculated { get; private set; } = false;

    protected abstract void InitStats();

    private void Awake() {
        InitStats();
        IsStatsCalculated = true;
    }

    public void AddBuff(BuffStaticData<TStatType> buff) {
        buffs.Add(buff);
        UpdateStats();
    }

    private void RemoveBuff(BuffStaticData<TStatType> buff) {
        buffs.Remove(buff);
        UpdateStats();
    }

    public void SetRawStat(TStatType type, float value) {
        RawStats[type].Value = value;
        UpdateStats();
    }

    private void UpdateStats() {
        foreach (TStatType statType in Enum.GetValues(typeof(TStatType))) {
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
        foreach (BuffStaticData<TStatType> buff in buffs) {
            //giảm thời gian tồn tại của buff
            buff.existTime -= Time.deltaTime;
        }
        UpdateStats();
    }
}