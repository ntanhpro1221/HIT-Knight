using Firebase.Firestore;
using System;
using System.Collections.Generic;
using UnityEngine;

[FirestoreData]
[Serializable]
public class RuneStaticData<T> where T : Enum {
    [FirestoreProperty]
    [field: SerializeField]
    public string id { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public string name { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public List<BuffStaticData<T>> listBuff { get; set; }
    [FirestoreProperty]
    [field: SerializeField]
    public int cost { get; set; }
}
