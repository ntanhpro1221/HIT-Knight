using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manage all target need to stalk
/// </summary>
[RequireComponent(typeof(Collider2D))]
public abstract class IStalker : CoreComponent {
    [field: SerializeField] 
    private List<GameObject> TargetList { get; set; } = new();

    private void ReCalcTopTarget() {
        TargetList.Sort(CompareTarget);
        TopTarget.Value = TargetList.LastOrDefault(ValidateTarget_ByCurBehaviour);
    }

    private void Update() {
        ReCalcTopTarget();
    }

    [field: SerializeField]
    public BindableProperty<GameObject> TopTarget { get; private set; } = new();
    /// <summary>
    /// add object in range
    /// </summary>
    private void OnTriggerEnter2D(Collider2D coll) {
        if (ValidateTarget_FromTheBeginning(coll))
            TargetList.Add(ToTargetType(coll));
    }
    /// <summary>
    /// remove object if it != in collider range
    /// </summary>
    private void OnTriggerExit2D(Collider2D coll) {
        if (ValidateTarget_FromTheBeginning(coll))
            TargetList.Remove(ToTargetType(coll));
    }
    /// <summary>
    /// Get GameObject that need to stalk from its collider.
    /// </summary>
    /// Implement by children
    public abstract GameObject ToTargetType(Collider2D coll);
    /// <summary>
    /// Check if this obj should be stalked
    /// </summary>
    /// <returns></returns>
    public abstract bool ValidateTarget_FromTheBeginning(Collider2D target);
    /// <summary>
    /// Check if this obj deserves to be top target (still be stalked but not be top target)
    /// </summary>
    /// Implement by children
    public abstract bool ValidateTarget_ByCurBehaviour(GameObject target);
    /// <summary>
    /// Compare the priority of two target.
    /// </summary>
    /// Implement by children
    public virtual int CompareTarget(GameObject a, GameObject b) => 0;
}
