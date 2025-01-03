using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlHelper : IActorControlHelper {
    private PlayerControllerInBattle controller;
    private readonly AntiNull<PlayerInput> input = new(() => new());

    private PlayerInput.InBattleActions InBattle // Just shortcut
        => input.Value.InBattle;

    private void Awake() {
        controller = TmpGameManager.Instance.controller;
        InBattle.Dash.canceled += obj => {
            if (controller.IsDiscard == false)
                Dash = controller.CurDir;
            GetCoreComponent<IStateMachine>().UpdateState();
            Dash = default;
        };
    }

    private void OnEnable()
        => input.Value.Enable();

    private void OnDisable()
        => input.Value.Disable();

    private void Start() {
        InBattle.Attack.performed += obj => Attack = true;
        InBattle.Attack.canceled += obj => Attack = false;
    }

    public override bool NoCommand { 
        get => false == input.Value.asset.FindActionMap(nameof(InBattle)).actions.
            Any(action => action.phase != InputActionPhase.Waiting);
        protected set { }
    }

    public override Vector2 MoveByDir { 
        get => InBattle.Move.ReadValue<Vector2>();
        protected set { }
    }

    public override Vector2? MoveByPos { 
        get;
        protected set;
    }

    public override bool Attack {
        get => InBattle.Attack.phase == InputActionPhase.Performed;
        protected set { }
    }

    public override Vector2 Dash { 
        get; 
        protected set; 
    }
}
