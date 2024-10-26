using UnityEngine;

/// <summary>
/// Manage all target need to stalk
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class IStalker : CoreComponent {
    /// <summary>
    /// Most attention target (determine by CompareTarget function).
    /// </summary>
    public BindableProperty<GameObject> TopTarget => throw new System.NotImplementedException();
    /// <summary>
    /// ReCalculate who is top target.
    /// </summary>
    public void ReCalcTopTarget() => throw new System.NotImplementedException();
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
