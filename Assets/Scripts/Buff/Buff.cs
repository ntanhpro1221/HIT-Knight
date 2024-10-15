using System;

/// <summary>
/// Buff data for stats of object.
/// </summary>
/// <typeparam name="T">What type of stat will be buffed.</typeparam>
[Serializable]
public class Buff<T> where T  : Enum {
    public float existTime;
    public float value;
    public BuffType buffType;
    public T statType;
}
