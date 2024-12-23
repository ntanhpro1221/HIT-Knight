using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Set of value with the same type. Each element can be access easier through enum key
/// </summary>
/// <typeparam name="TKey">Enum type of key</typeparam>
/// <typeparam name="TValue">Data type of each element</typeparam>
//[FirestoreData]
[Serializable]
public class PropertySet<TKey, TValue> where TKey : Enum {
    //[FirestoreProperty]
    //[field: SerializeField]
    [SerializeField]
    private TKey m_Type;
    //[FirestoreProperty]
    //[field: SerializeField]
    [SerializeField]
    private TValue[] m_Values;

    public PropertySet() {
        m_Values = new TValue[Enum.GetValues(typeof(TKey)).Length];
        //Debug.Log(m_Values.Length);
    }
    private int EToInt(TKey key) => Convert.ToInt32(key);
    public TValue this[TKey key] {
        get => m_Values[EToInt(key)];
        set => m_Values[EToInt(key)] = value;
    }
    public static implicit operator Dictionary<TKey, TValue>(PropertySet<TKey, TValue> obj)
        => ((TKey[])Enum.GetValues(typeof(TKey))).ToDictionary(key => key, key => obj[key]);
}

#if UNITY_EDITOR
/// <summary>
/// Enable edit PropertySet in Inspector
/// </summary>
[CustomPropertyDrawer(typeof(PropertySet<,>), true)]
public class StatsDrawer : PropertyDrawer {
    private const float ValueSpace = 2;

    private SerializedProperty m_Type;
    private SerializedProperty m_Values;
    private float lineHeight => EditorGUIUtility.singleLineHeight;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        m_Type ??= property.FindPropertyRelative(nameof(m_Type));
        m_Values ??= property.FindPropertyRelative(nameof(m_Values));
        float height = lineHeight - ValueSpace;
        if (property.isExpanded)
            for (int i = 0; i < m_Values.arraySize; ++i)
                height += 
                    EditorGUI.GetPropertyHeight(m_Values.GetArrayElementAtIndex(i))
                    + ValueSpace;
        height = Math.Max(lineHeight, height);
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        Debug.Log(m_Values.arraySize);
        EditorGUI.BeginProperty(position, label, property);
        Rect labelPosition = new Rect(position.x, position.y, position.width, lineHeight);
        if (property.isExpanded = EditorGUI.Foldout(labelPosition, property.isExpanded, label, true)) {
            ++EditorGUI.indentLevel;
            float addY = lineHeight;
            for (int i = 0; i < m_Values.arraySize; ++i)
                EditorGUI.PropertyField(
                    new Rect(
                        position.x,
                        position.y + addY,
                        position.width,
                        addY += 
                            EditorGUI.GetPropertyHeight(m_Values.GetArrayElementAtIndex(i))
                            + ValueSpace),
                    m_Values.GetArrayElementAtIndex(i),
                    new GUIContent(m_Type.enumNames[i]),
                    true);
            --EditorGUI.indentLevel;
        }
        EditorGUI.EndProperty();
    }
}
#endif
