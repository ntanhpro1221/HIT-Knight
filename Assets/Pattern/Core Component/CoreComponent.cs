using System.Linq;
using UnityEngine;

/// <summary>
/// Managed by core.
/// </summary>
public class CoreComponent : MonoBehaviour {
    private Core m_core;
    protected Core Core 
        => m_core ??= GetComponentsInParent<Core>().FirstOrDefault(cpn => cpn.transform == transform.parent);
    protected T GetCoreComponent<T>() where T : CoreComponent 
        => Core.GetCoreComponent<T>();
}
