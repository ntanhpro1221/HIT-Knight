using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T m_Instance;
    public static T Instance => m_Instance ??= 
        FindObjectOfType<T>() ?? 
        new GameObject("(Singleton) " + typeof(T).Name).AddComponent<T>();
    protected virtual void Awake() {
        if (m_Instance != null && m_Instance.GetInstanceID() != GetInstanceID()) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
