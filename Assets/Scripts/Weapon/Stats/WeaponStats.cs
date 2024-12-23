using Firebase.Firestore;
using System;

/// <summary>
/// Stats of weapon
/// </summary>
[FirestoreData]
[Serializable]
public class WeaponStats<T> : PropertySet<WeaponStatType, T> { }

