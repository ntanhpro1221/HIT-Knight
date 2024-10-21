using UnityEngine;

/// <summary>
/// Manage all target need to stalk
/// </summary>
public abstract class IStalker : CoreComponent {
    /// <summary>
    /// Most attention target (determine by CompareTarget function).
    /// </summary>
    public BindableProperty<GameObject> CurrentTarget => throw new System.NotImplementedException();
    /// <summary>
    /// Get GameObject that need to stalk from its collider.
    /// </summary>
    public abstract GameObject ToTargetType(Collider2D coll);
    /// <summary>
    /// Check if this target need to stalk.
    /// </summary>
    public abstract bool ValidateTarget(GameObject target);
    /// <summary>
    /// Compare the priority of two target.
    /// </summary>
    public virtual int CompareTarget(GameObject a, GameObject b) => 0;
}
