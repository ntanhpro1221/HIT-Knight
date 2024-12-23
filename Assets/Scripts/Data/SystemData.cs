using AYellowpaper.SerializedCollections;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Default data of game system.
/// </summary>
[FirestoreData]
[Serializable]
public class SystemData {
    [FirestoreProperty]
    [field: SerializeField]
    public SerializedDictionary<string, HeroStaticData> heroData { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public SerializedDictionary<string, EnemyStaticData> enemyData {get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public SerializedDictionary<string, RuneStaticData<ActorStatType>> actorRuneData {get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public SerializedDictionary<string, RuneStaticData<WeaponStatType>> weaponRuneData {get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public SerializedDictionary<string, WeaponStaticData> weaponData {get; set; }
}
