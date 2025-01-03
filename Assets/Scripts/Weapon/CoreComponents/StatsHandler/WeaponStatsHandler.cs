using System;

/// <summary>
/// Manage stats of weapon with all buff
/// </summary>
public class WeaponStatsHandler : IStatsHandler<WeaponStatType> {
    protected new WeaponCore Core => base.Core as WeaponCore;
    public IWeapon Weapon => Core.Weapon;

    protected override void InitStats() {
        foreach (WeaponStatType stat in Enum.GetValues(typeof(WeaponStatType)))
            SetRawStat(stat, DataManager.Instance.SystemData.weaponData[Weapon.WeaponId].stats[stat]);
    }
}