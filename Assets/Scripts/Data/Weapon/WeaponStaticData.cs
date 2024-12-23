using System;

[Serializable]
public class WeaponStaticData {
    public string id;
    public string name;
    public PropertySet<WeaponStatType, float> stats;
}

