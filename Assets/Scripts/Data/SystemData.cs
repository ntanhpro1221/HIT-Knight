using System;
using System.Collections.Generic;

[Serializable]
public class SystemData {
    public Dictionary<string, HeroStaticData> heroData;
    public Dictionary<string, EnemyStaticData> enemyData;
    public Dictionary<string, RuneStaticData> runeData;
}
