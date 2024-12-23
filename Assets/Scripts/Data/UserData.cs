using AYellowpaper.SerializedCollections;
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
    public SerializedDictionary<string, HeroDynamicData> heroData;
    public SerializedDictionary<string, WeaponDynamicData> weaponData;
    public List<RuneDynamicData> actorRuneData;
    public List<RuneDynamicData> weaponRuneData;
}
