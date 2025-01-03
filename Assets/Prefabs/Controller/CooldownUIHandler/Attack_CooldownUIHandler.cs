using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

[RequireComponent(typeof(OnScreenButton))]
public class Attack_CooldownUIHandler : CooldownUIHandler {
    private OnScreenButton onScreenBtn;

    protected override void Awake() {
        base.Awake();
        onScreenBtn = GetComponent<OnScreenButton>();
    }

    protected override void Update() {
        base.Update();
        var (cur, max) = getCooldownInfo.Invoke();
    }
}

