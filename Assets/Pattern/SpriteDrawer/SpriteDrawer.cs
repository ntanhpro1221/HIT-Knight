#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Sprite))]
public class SpriteDrawer : PropertyDrawer {
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return 64;
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);
        property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(Sprite), false);
        EditorGUI.EndProperty();
    }
}
#endif
