using AYellowpaper.SerializedCollections;
using Firebase.Database;
using Firebase.Firestore;
using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Allow to perform CRUD operations on data
/// </summary>
public class DataManager : Singleton<DataManager>, IDataManager {
    private DatabaseReference RootDB => FirebaseDatabase.DefaultInstance.RootReference;
    private DatabaseReference UserDataRef => RootDB.Child("UserData");
    private FirebaseFirestore RootFS => FirebaseFirestore.DefaultInstance;
    private DocumentReference SystemDataRef => RootFS.Collection("SystemData").Document("Ver1.0");

    private async Task<T> LoadObjectAsync<T>(DatabaseReference dataRef) {
        print($"Start load {typeof(T).Name} ({Time.time})");
        T obj = default;
        try {
            obj = JsonConvert.DeserializeObject<T>((await dataRef.GetValueAsync()).GetRawJsonValue());
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
        print($"Done load {typeof(T).Name} ({Time.time})");
        return obj;
    }
    private async Task<T> LoadObjectAsync<T>(DocumentReference dataRef) {
        print($"Start load {typeof(T).Name} ({Time.time})");
        T obj = default;
        try {
            obj = (await dataRef.GetSnapshotAsync()).ConvertTo<T>();
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
        print($"Done load {typeof(T).Name} ({Time.time})");
        return obj;
    }
    private async Task SaveObjectAsync(DatabaseReference dataRef, object data) {
        print($"Start save {data.GetType().Name} ({Time.time})");
        try {
            await dataRef.SetRawJsonValueAsync(JsonConvert.SerializeObject(data));
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
        print($"Done save {data.GetType().Name} ({Time.time})");
    }
    private async Task SaveObjectAsync(DocumentReference dataRef, object data) {
        print($"Start save {data.GetType().Name} ({Time.time})");
        try {
            await dataRef.SetAsync(data);
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
        print($"Done save {data.GetType().Name} ({Time.time})");
    }

    protected override void Awake() {
        base.Awake();
        LoadUserDataAsync();
        LoadSystemDataAsync();
    }

    [SerializeField] private UserData m_UserData;
    public UserData UserData => m_UserData;
    [SerializeField] private SystemData m_SystemData;
    public SystemData SystemData => m_SystemData;

    public async Task LoadUserDataAsync() => 
        m_UserData = await LoadObjectAsync<UserData>(UserDataRef);
    public async Task SaveUserDataAsync() =>
        await SaveObjectAsync(UserDataRef, m_UserData);
    public async Task LoadSystemDataAsync() =>
        m_SystemData = await LoadObjectAsync<SystemData>(SystemDataRef);
    public async Task SaveSystemDataAsync() =>
        await SaveObjectAsync(SystemDataRef, m_SystemData);
}

#if UNITY_EDITOR
[CustomEditor(typeof(DataManager))]
public class DataManagerEditor : Editor {
    private class EditorButton {
        public string name;
        public Action onClick;
        public EditorButton(string name, Action onClick) {
            this.name = name;
            this.onClick = onClick;
        }
        public void Display() {
            if (GUILayout.Button(name)) 
                onClick.Invoke();
        }
    }

    private DataManager m_Target;
    private SerializedProperty m_UserData;
    private SerializedProperty m_SystemData;
    private void OnEnable() {
        m_Target = (DataManager)target;
        m_UserData = serializedObject.FindProperty(nameof(m_UserData));
        m_SystemData = serializedObject.FindProperty(nameof(m_SystemData));
    }
    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        RenderDataField(
            m_UserData,
            new("Load", () => _ = m_Target.LoadUserDataAsync()),
            new("Save", () => _ = m_Target.SaveUserDataAsync()));
        RenderDataField(
            m_SystemData,
            new("Load", () => _ = m_Target.LoadSystemDataAsync()),
            new("Save", () => _ = m_Target.SaveSystemDataAsync()));

        EditorGUI.EndChangeCheck();
        serializedObject.ApplyModifiedProperties();
    }
    
    private void RenderDataField(SerializedProperty data, EditorButton loadBtn, EditorButton saveBtn) { 
        // Label
        EditorGUILayout.BeginHorizontal();
        bool fold = EditorGUILayout.PropertyField(data, false);
        loadBtn?.Display();
        saveBtn?.Display();
        EditorGUILayout.EndHorizontal();

        // Child element
        EditorGUI.indentLevel++;
        if (fold) foreach(var ite in GetDirectChilds(data)) 
            EditorGUILayout.PropertyField(ite);
        EditorGUI.indentLevel--;
    }

    private IEnumerable<SerializedProperty> GetDirectChilds(SerializedProperty prop) {
        if (!prop.hasVisibleChildren) yield break;
        var ite = prop.Copy(); ite.NextVisible(true);
        var end = prop.GetEndProperty();
        do {
            EditorGUILayout.PropertyField(ite);
            ite.NextVisible(false);
        } while (!SerializedProperty.EqualContents(ite, end));
    }
}
#endif
