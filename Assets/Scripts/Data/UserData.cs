using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Local data of user
/// </summary>
[Serializable]

public class UserData : ScriptableObject{
    public string id;
    public string name;
    public int gold;
    public Dictionary<string, HeroDynamicData> heroData;
    public Dictionary<string, WeaponDynamicData> weaponData;
    public List<RuneDynamicData> actorRuneData;
    public List<RuneDynamicData> weaponRuneData;

}
