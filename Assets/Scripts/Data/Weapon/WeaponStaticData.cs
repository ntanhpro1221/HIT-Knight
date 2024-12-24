using Firebase.Firestore;
using System;
using UnityEngine;

[FirestoreData]
[Serializable]
public class WeaponStaticData {
    [FirestoreProperty]
    [field: SerializeField]
    public string id { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public string name { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public PropertySet<WeaponStatType, float> stats { get; set; }
}

