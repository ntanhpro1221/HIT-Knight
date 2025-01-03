using System;
using UnityEngine;

/// <summary>
/// Manage all state of object.
/// </summary>
public abstract class IStateMachine : CoreComponent {
    [field: SerializeField]
    public IState CurState { get; protected set; }
    protected virtual IState DefState { get; }

    #region START MACHINE
    private bool isStarted = false;
    private void StartMachine() {
        isStarted = true;
        if (DefState == null) {
            Util.LogError_WithAddress(gameObject, "Default state has not been set yet");
        }
        CurState = DefState;
        CurState.Enter();
    }
    #endregion

    /// <summary>
    /// Try to move to next state.
    /// </summary>
    public void UpdateState() {
        ChangeState(CurState?.GetNextState());
    }

    private void Update() {
        if (isStarted is false) StartMachine();
        UpdateState();
        CurState?.Update();
    }

    private void FixedUpdate() {
        CurState?.FixedUpdate();
    }

    private void ChangeState(IState newState) {
        if (newState == null) return;
        if (newState == CurState) return;
        CurState?.Exit();
        CurState = newState;
        CurState?.Enter();
    }
    
    protected virtual void Awake() { }
}