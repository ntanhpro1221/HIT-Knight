using System;

[Serializable]
public class ActorStaticData {
    public string id;
    public string name;
    public PropertySet<ActorStatType, float> stats;
}