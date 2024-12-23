using Firebase.Firestore;
using System;

[FirestoreData]
[Serializable]
public class ActorStats<T> : PropertySet<ActorStatType, T> { }
