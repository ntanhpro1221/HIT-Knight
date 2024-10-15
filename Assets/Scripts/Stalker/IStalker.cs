using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manage all target need to stalk
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class IStalker : CoreComponent {
    private readonly List<GameObject> m_TargetList = new();
    private readonly BindableProperty<GameObject> m_TopTarget = new();

    private void ReCalcTopTarget() {
        m_TargetList.Sort(CompareTarget);
        m_TopTarget.Value = m_TargetList.LastOrDefault(ValidateTarget);
    }

    private void Update() {
        ReCalcTopTarget();
    }

    /// <summary>
    /// Most attention target (determine by CompareTarget function).
    /// </summary>
    public BindableProperty<GameObject> TopTarget => m_TopTarget;
    /// <summary>
    /// add object in range
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other) {
        m_TargetList.Add(ToTargetType(other));
    }
    /// <summary>
    /// remove object if it is not in collider range
    /// </summary>
    private void OnTriggerExit2D(Collider2D other) {
        m_TargetList.Remove(ToTargetType(other));
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
}
