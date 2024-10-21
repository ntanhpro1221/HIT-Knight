using System;
using System.Collections.Generic;

/// <summary>
/// Default data of game system.
/// </summary>
[Serializable]
public class SystemData {
    public Dictionary<string, HeroStaticData> heroData;
    public Dictionary<string, EnemyStaticData> enemyData;
    public Dictionary<string, RuneStaticData<ActorStatType>> actorRuneData;
    public Dictionary<string, RuneStaticData<WeaponStatType>> weaponRuneData;
    public Dictionary<string, WeaponStaticData> weaponData;
}
