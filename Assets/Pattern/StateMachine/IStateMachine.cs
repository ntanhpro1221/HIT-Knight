using System;

/// <summary>
/// Manage all state of object.
/// </summary>
/// <typeparam name="T">Type of body handler</typeparam>
public abstract class IStateMachine : CoreComponent {
    protected IState anyState;
    /// <summary>
    /// Alway try to move to next state of "AnyState" regardless what current state is
    /// </summary>
    protected abstract IState AnyState { get; }
    protected IState curState;
    /// <summary>
    /// Current state of object
    /// </summary>
    protected abstract IState CurState { get; set; }
    /// <summary>
    /// Try to move to next state.
    /// </summary>
    private void UpdateState() {
        IState newState =
            AnyState?.GetNextState() ??
            CurState?.GetNextState();
        if (newState != null) ChangeState(newState);
    }
    private void Update() {
        UpdateState();
        CurState?.Update();
    }
    private void FixedUpdate() {
        CurState?.FixedUpdate();
    }
    private void ChangeState(IState newState) {
        if (CurState == newState) return;
        CurState?.Exit();
        CurState = newState;
        CurState?.Enter();
    }
}