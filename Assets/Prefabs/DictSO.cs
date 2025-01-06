using AYellowpaper.SerializedCollections;
using UnityEngine;

public class DictSO<TKey, TValue> : ScriptableObject {
    [SerializeField]
    private SerializedDictionary<TKey, TValue> m_Dict;

    public TValue this[TKey key] => m_Dict[key];
}
