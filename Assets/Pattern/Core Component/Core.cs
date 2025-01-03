using System.Linq;
using UnityEngine;

/// <summary>
/// Manage core component. They can access to each other through their class
/// </summary>
public abstract class Core : MonoBehaviour {
    public T GetCoreComponent<T>() where T : CoreComponent =>
        GetComponentsInChildren<T>().FirstOrDefault(cpn => cpn.transform.parent == transform);
}
