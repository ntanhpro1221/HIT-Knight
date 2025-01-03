using EnhancedOnCreenStick;
using UnityEngine;

[RequireComponent(typeof(EnhancedOnScreenStick))]
public class Dash_CooldownUIHandler : CooldownUIHandler {
    private EnhancedOnScreenStick stick;
 
    protected override void Awake() {
        base.Awake();
        stick = GetComponent<EnhancedOnScreenStick>();
    }

    protected override void Update() {
        base.Update();
        var (cur, max) = getCooldownInfo.Invoke();
        stick.enabled = cur <= 0;
    }
}

