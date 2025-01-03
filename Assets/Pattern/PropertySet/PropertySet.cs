using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Set of value with the same type. Each element can be access easier through enum key
/// </summary>
/// <typeparam name="TKey">Enum type of key</typeparam>
/// <typeparam name="TValue">Data type of each element</typeparam>
[Serializable]
public class PropertySet<TKey, TValue> : 
    Dictionary<string, TValue>, ISerializationCallbackReceiver 
    where TKey : Enum 
    where TValue : new() {
    /// <summary>
    /// to get enum type
    /// </summary>
    [SerializeField] private TKey m_KeyType;
    /// <summary>
    /// to restore value with corresponding key when enum's script change
    /// </summary>
    [SerializeField] private string[] m_Keys;
    [SerializeField] private TValue[] m_Values;
    
    public new string[] Keys => m_Keys;
    public new TValue[] Values => m_Values;

    private void InitEnumField() {
        m_Keys = Enum.GetNames(typeof(TKey));
        m_Values = new TValue[Enum.GetNames(typeof(TKey)).Length];
        for (int i = 0; i < m_Values.Length; i++) m_Values[i] = new();
    }

    public PropertySet() : base() => InitEnumField();

    private int EToInt(TKey key) => Convert.ToInt32(key);

    public TValue this[TKey key] {
        get => m_Values[EToInt(key)];
        set => m_Values[EToInt(key)] = value;
    }

    public void OnBeforeSerialize() {
        if (m_Keys.Length == 0) InitEnumField();
        foreach (TKey key in Enum.GetValues(typeof(TKey))) 
            if (base.Keys.Contains(key.ToString())) 
                this[key] = base[key.ToString()];
    }

    public void OnAfterDeserialize() {
        if (m_Keys.Length == 0) InitEnumField();
        base.Clear();
        foreach (TKey key in Enum.GetValues(typeof(TKey)))
            base.Add(key.ToString(), this[key]);
    }
    
    //public new TValue this[string key] {
    //    get => throw new Exception("Dont use this function!!!");
    //    set => throw new Exception("Dont use this function!!!");
    //}
    public new void Add(string key, TValue value) 
        => throw new Exception("Dont use this function!!!");
    public new bool TryAdd(string key, TValue value) 
        => throw new Exception("Dont use this function!!!");
    public new void Clear() 
        => throw new Exception("Dont use this function!!!");
    public new bool Remove(string key) 
        => throw new Exception("Dont use this function!!!");
    public new bool Remove(string key, out TValue value) 
        => throw new Exception("Dont use this function!!!");
}

#if UNITY_EDITOR
/// <summary>
/// Enable edit PropertySet in Inspector
/// </summary>
[CustomPropertyDrawer(typeof(PropertySet<,>), true)]
public class PropertySetDrawer : PropertyDrawer {
    private SerializedProperty m_KeyType;
    private SerializedProperty m_Keys;
    private SerializedProperty m_Values;
    private bool m_Dirty;
    private float LineHeight => EditorGUIUtility.singleLineHeight;
    private float LineSpace => 2;

    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        m_Dirty = true;
        return base.CreatePropertyGUI(property);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        m_KeyType = property.FindPropertyRelative(nameof(m_KeyType));
        m_Keys = property.FindPropertyRelative(nameof(m_Keys));
        m_Values = property.FindPropertyRelative(nameof(m_Values));
        if (m_Dirty) Clean();

        float height = LineHeight;
        if (property.isExpanded)
            for (int i = 0; i < m_KeyType.enumNames.Length; ++i)
                height += 
                    EditorGUI.GetPropertyHeight(m_Values.GetArrayElementAtIndex(i)) + 
                    (i + 1 != m_KeyType.enumNames.Length ? LineSpace : 0);
        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);
        Rect labelPosition = new Rect(position.x, position.y, position.width, LineHeight);
        if (property.isExpanded = EditorGUI.Foldout(labelPosition, property.isExpanded, label, true)) {
            ++EditorGUI.indentLevel;
            ++EditorGUI.indentLevel;
            float addY = LineHeight;
            for (int i = 0; i < m_KeyType.enumNames.Length; ++i)
                EditorGUI.PropertyField(
                    new Rect(
                        position.x,
                        position.y + addY,
                        position.width,
                        addY += 
                            EditorGUI.GetPropertyHeight(m_Values.GetArrayElementAtIndex(i)) +
                            (i + 1 != m_KeyType.enumNames.Length ? LineSpace : 0)),
                    m_Values.GetArrayElementAtIndex(i),
                    new GUIContent(m_KeyType.enumNames[i]),
                    true);
            --EditorGUI.indentLevel;
            --EditorGUI.indentLevel;
        }
        EditorGUI.EndProperty();
    }

    private bool CheckEnumKeyIntact() {
        if (m_Keys.arraySize != m_KeyType.enumNames.Length) return false;
        for (int i = 0; i < m_KeyType.enumNames.Length; ++i)
            if (false == m_Keys.GetArrayElementAtIndex(i).stringValue.Equals(m_KeyType.enumNames[i])) 
                return false;
        return true;
    }

    private void UpdateEnumKey() {
        // Key algorithm: foreach element in new enum key, restore value if this key exist in old enum key
        
        // add more element if size of the new enum key is BIGGER then old one
        // (don't remove element now in the opposite case and assign empty to new key string) because we need restore old value (if possible)
        while (m_Keys.arraySize < m_KeyType.enumNames.Length) {
            m_Keys.InsertArrayElementAtIndex(0);
            m_Values.InsertArrayElementAtIndex(0);
            m_Keys.GetArrayElementAtIndex(0).stringValue = string.Empty;
        }
        
        // restore value of old enum key (if possible)
        for (int i = 0; i < m_KeyType.enumNames.Length; ++i) {
            int? match_id = null;
            for (int j = 0; j < m_Keys.arraySize; ++j)
                if (m_Keys.GetArrayElementAtIndex(j).stringValue.Equals(m_KeyType.enumNames[i]))
                    match_id = j;

            if (match_id == null || match_id == i) continue;

            int left = i, right = (int)match_id;
            if (left > right) (left, right) = (right, left);
            m_Keys.MoveArrayElement(left, right);
            m_Values.MoveArrayElement(left, right);
            m_Keys.MoveArrayElement(right - 1, left);
            m_Values.MoveArrayElement(right - 1, left);
        }

        // remove element if if size of the new enum key is SMALLER then old one
        while (m_Keys.arraySize > m_KeyType.enumNames.Length) {
            int last_id = m_Keys.arraySize - 1;
            m_Keys.DeleteArrayElementAtIndex(last_id);
            m_Values.DeleteArrayElementAtIndex(last_id);
        }

        // assign correct enum key
        for (int i = 0; i < m_Keys.arraySize; ++i)
            m_Keys.GetArrayElementAtIndex(i).stringValue = m_KeyType.enumNames[i];
    }

    private void Clean() {
        m_Dirty = false;
        if (CheckEnumKeyIntact() == false) UpdateEnumKey();
    }
}
#endif
