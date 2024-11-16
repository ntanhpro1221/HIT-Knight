using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage all target need to stalk
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class IStalker : CoreComponent
{
    [SerializeField] protected List<GameObject> stalkerList;
    private BindableProperty<GameObject> topTarget;

    /// <summary>
    /// Most attention target (determine by CompareTarget function).
    /// </summary>
    public BindableProperty<GameObject> TopTarget
    {
        get
        {
            stalkerList.Sort(CompareTarget);
            foreach (var taget in stalkerList)
            {
                if (ValidateTarget(taget))
                {
                    topTarget.Value = taget;
                    return topTarget;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Get GameObject that need to stalk from its collider.
    /// </summary>
    /// Implement by children
    public abstract GameObject ToTargetType(Collider2D coll);
    /// <summary>
    /// Check if this target need to stalk.
    /// </summary>
    /// Implement by children
    public abstract bool ValidateTarget(GameObject target);
    /// <summary>
    /// Compare the priority of two target.
    /// </summary>
    /// Implement by children
    public virtual int CompareTarget(GameObject a, GameObject b) => 0;
    
    /// <summary>
    /// add object in range
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject target = ToTargetType(other);
        if (ValidateTarget(target)) stalkerList.Add(target);
    }
    /// <summary>
    /// remove object if it is not in collider range
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject target = ToTargetType(other);
        if (ValidateTarget(target)) stalkerList.Remove(target);
    }
}
