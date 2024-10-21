using System;
using System.Collections.Generic;

[Serializable]
public class RuneStaticData<T> where T : Enum {
    public string id;
    public string name;
    public List<Buff<T>> listBuff;
    public int cost;
}
