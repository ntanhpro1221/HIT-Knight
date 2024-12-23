using Firebase.Firestore;
using System;
using UnityEngine;

[FirestoreData]
[Serializable]
public class HeroStaticData : ActorStaticData {
    [FirestoreProperty]
    [field: SerializeField]
    public int cost { get; set; }
}
