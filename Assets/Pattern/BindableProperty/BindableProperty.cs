using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invoke your action when its value is changed
/// </summary>
[Serializable]
public class BindableProperty<T> {
    [SerializeField] private T m_Value = default;
    public UnityEvent<T> OnChanged { get; } = new();
    public T Value {
        get => m_Value;
        set {
            if (m_Value == null ? value != null : !m_Value.Equals(value))
                OnChanged.Invoke(m_Value = value);
        }
    }
    public override string ToString() => m_Value.ToString();
    public static implicit operator string(BindableProperty<T> obj) => obj.ToString();
}

#if UNITY_EDITOR
/// <summary>
/// Enable edit PropertySet in Inspector
/// </summary>
[CustomPropertyDrawer(typeof(BindableProperty<>), true)]
public class BindablePropertyDrawer : PropertyDrawer {
    private SerializedProperty m_Value;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        m_Value = property.FindPropertyRelative(nameof(m_Value));
        return EditorGUI.GetPropertyHeight(m_Value);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        position.height = EditorGUI.GetPropertyHeight(m_Value);
        EditorGUI.PropertyField(position, m_Value, label);
    }
}
#endif
