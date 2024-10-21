#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(AspectRatioFitterLayoutElement), true)]
[CanEditMultipleObjects]
/// <summary>
///   Custom Editor for the AspectRatioFitter component.
///   Extend this class to write a custom editor for a component derived from AspectRatioFitter.
/// </summary>
public class AspectRatioFitterLayoutElementEditor : SelfControllerEditor {
    SerializedProperty m_VerticalPadding;
    SerializedProperty m_HorizontalPadding;
    SerializedProperty m_AspectMode;
    SerializedProperty m_AspectRatio;

    AnimBool m_ModeBool;
    private AspectRatioFitterLayoutElement aspectRatioFitter;

    protected virtual void OnEnable() {
        m_VerticalPadding = serializedObject.FindProperty("m_VerticalPadding");
        m_HorizontalPadding = serializedObject.FindProperty("m_HorizontalPadding");
        m_AspectMode = serializedObject.FindProperty("m_AspectMode");
        m_AspectRatio = serializedObject.FindProperty("m_AspectRatio");
        aspectRatioFitter = target as AspectRatioFitterLayoutElement;

        m_ModeBool = new AnimBool(m_AspectMode.intValue != 0);
        m_ModeBool.valueChanged.AddListener(Repaint);
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        GUI.enabled = false;
        EditorGUILayout.PropertyField(m_AspectMode);
        GUI.enabled = true;

        m_ModeBool.target = m_AspectMode.intValue != 0;

        if (EditorGUILayout.BeginFadeGroup(m_ModeBool.faded)) {
            EditorGUILayout.PropertyField(m_AspectRatio);
            if (aspectRatioFitter.aspectMode == 
                AspectRatioFitterLayoutElement.AspectMode.WidthControlsHeight)
                EditorGUILayout.PropertyField(m_HorizontalPadding);
            else if (aspectRatioFitter.aspectMode == 
                AspectRatioFitterLayoutElement.AspectMode.HeightControlsWidth)
                EditorGUILayout.PropertyField(m_VerticalPadding);
        }
        EditorGUILayout.EndFadeGroup();

        serializedObject.ApplyModifiedProperties();

        if (aspectRatioFitter) {
            if (!aspectRatioFitter.IsAspectModeValid())
                ShowNoParentWarning();
            if (!aspectRatioFitter.IsComponentValidOnObject())
                ShowCanvasRenderModeInvalidWarning();
        }

        base.OnInspectorGUI();
    }

    protected virtual void OnDisable() {
        aspectRatioFitter = null;
        m_ModeBool.valueChanged.RemoveListener(Repaint);
    }

    private static void ShowNoParentWarning() {
        var text = L10n.Tr("You cannot use this Aspect Mode because this Component's GameObject does not have a parent object.");
        EditorGUILayout.HelpBox(text, MessageType.Warning, true);
    }

    private static void ShowCanvasRenderModeInvalidWarning() {
        var text = L10n.Tr("You cannot use this Aspect Mode because this Component is attached to a Canvas with a fixed width and height.");
        EditorGUILayout.HelpBox(text, MessageType.Warning, true);
    }
}
#endif
