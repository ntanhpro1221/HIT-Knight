using Firebase.Firestore;
using System;
using UnityEngine;

/// <summary>
/// Buff data for stats of object.
/// </summary>
/// <typeparam name="T">What type of stat will be buffed.</typeparam>
[FirestoreData]
[Serializable]
public class BuffStaticData<T> where T : Enum {
    [FirestoreProperty]
    [field: SerializeField]
    public bool isForever { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public float existTime { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public float value { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public BuffType buffType { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public T statType { get; set; }
}
