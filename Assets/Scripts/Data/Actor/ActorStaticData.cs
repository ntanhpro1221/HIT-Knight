using Firebase.Firestore;
using System;
using UnityEngine;

[FirestoreData]
[Serializable]
public class ActorStaticData {
    [FirestoreProperty]
    [field: SerializeField]
    public string id { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public string name { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public PropertySet<ActorStatType, float> stats { get; set; }
}