using System;
using System.Collections.Generic;

[Serializable]
public class UserData {
    public string id;
    public string name;
    public int gold;
    public Dictionary<string, HeroDynamicData> heroData;
    public List<RuneDynamicData> runeData;
}
