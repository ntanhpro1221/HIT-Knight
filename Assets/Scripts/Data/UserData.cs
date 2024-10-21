using System;
using System.Collections.Generic;

/// <summary>
/// Local data of user
/// </summary>
[Serializable]
public class UserData {
    public string id;
    public string name;
    public int gold;
    public Dictionary<string, HeroDynamicData> heroData;
    public Dictionary<string, WeaponDynamicData> weaponData;
    public List<RuneDynamicData> actorRuneData;
    public List<RuneDynamicData> weaponRuneData;
}
