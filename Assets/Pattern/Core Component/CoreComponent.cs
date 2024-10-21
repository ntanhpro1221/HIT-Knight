using UnityEngine;

/// <summary>
/// Managed by core.
/// </summary>
public class CoreComponent : MonoBehaviour {
    private Core m_core;
    private Core Core => m_core ??= GetComponentInParent<Core>();
    protected T GetCoreComponent<T>() where T : CoreComponent =>
        Core.GetCoreComponent<T>();
}