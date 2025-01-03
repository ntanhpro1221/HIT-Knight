using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Alice : MonoBehaviour {
    private readonly AntiNull<PlayerInput> input = new(() => new());

    private void OnEnable()
        => input.Value.Enable();

    private void OnDisable()
        => input.Value.Disable();

    public bool NoCommand 
        => false == input.Value.asset.FindActionMap(nameof(input.Value.InBattle)).actions.
            Any(action => action.phase != InputActionPhase.Waiting);
    private void Update() {
        print(NoCommand);
    }
}

