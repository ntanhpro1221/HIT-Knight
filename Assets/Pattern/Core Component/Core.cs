using UnityEngine;

/// <summary>
/// Manage core component. They can access to each other through their class
/// </summary>
public class Core : MonoBehaviour {
    public T GetCoreComponent<T>() where T : CoreComponent =>
        GetComponentInChildren<T>();
}