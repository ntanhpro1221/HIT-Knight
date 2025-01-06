using UnityEngine;

public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T m_Instance;
    public static T Instance => m_Instance ??= 
        FindObjectOfType<T>() ?? 
        new GameObject("(Scene Singleton) " + typeof(T).Name).AddComponent<T>();
    protected virtual void Awake() {
        if (m_Instance != null && m_Instance.GetInstanceID() != GetInstanceID()) {
            Destroy(gameObject);
        }
    }
}
