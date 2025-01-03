using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class HeroStalker : IStalker
#if UNITY_EDITOR
    , ISerializationCallbackReceiver
#endif
    {
#if UNITY_EDITOR
#region JUST SHOW SOMETHING IN INSPECTOR
    [SerializeField] private string[] targetTagShower;

    public void OnBeforeSerialize() {
        if (EditorApplication.isPlaying == false) return;
        targetTagShower = TargetTag.ToArray();
    }

    public void OnAfterDeserialize() { }
#endregion
#endif

    private List<string> m_TargetTag = new() { "Monster" };
    protected virtual List<string> TargetTag => m_TargetTag;

    public override GameObject ToTargetType(Collider2D coll) 
        => coll.transform.parent.parent.gameObject;

    public override bool ValidateTarget_FromTheBeginning(Collider2D target)
        => TargetTag.Any(target.CompareTag);

    public override bool ValidateTarget_ByCurBehaviour(GameObject target)
        => target.GetComponent<IActor>().HealthHandler.IsDead == false;

    public override int CompareTarget(GameObject a, GameObject b)
        => Vector2.Distance(b.transform.position, transform.position).CompareTo(
           Vector2.Distance(a.transform.position, transform.position));
}