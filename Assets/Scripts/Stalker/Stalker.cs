
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Stalker : IStalker
{
    [SerializeField] protected List<GameObject> stalkerList;
    public override GameObject ToTargetType(Collider2D coll)
    {
        return coll.transform.parent.parent.gameObject;
    }

    public override bool ValidateTarget(GameObject target)
    {
        return target != null;
    }
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
