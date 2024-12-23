using AYellowpaper.SerializedCollections;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using UnityEngine;

//[FirestoreData]
//[Serializable]
//public class SystemData2 {
//    [FirestoreProperty]
//    [field: SerializeField]
//    public SerializedDictionary<string, HeroStaticData> heroData { get; set; }
//    [FirestoreProperty]
//    [field: SerializeField]
//    HeroStaticData> heroData { get; set; }
//}

public class Alice : MonoBehaviour {
    [SerializeField]
    private PropertySet<ActorStatType, int> lmao;
    [SerializeField]
    private SerializedDictionary<string, PropertySet<ActorStatType, int>> data;
    [SerializeField]
    private List<PropertySet<ActorStatType, int>> burh;
}
