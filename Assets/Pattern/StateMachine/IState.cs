using System;
using UnityEngine;

/// <summary>
/// Single state of a state machine.
/// </summary>
[Serializable]
public class IState {
    [field: SerializeField]
    public string Name { get; private set; }
    protected readonly IStateMachine sm;

    [Obsolete(__GeneralStateWarning)]
    public IState(string name, IStateMachine sm) {
        this.Name = name;
        this.sm = sm; 
    }

    /// <summary>
    /// Be called when start a state.
    /// </summary>
    public virtual void Enter() { }
    /// <summary>
    /// Find out if it can move to any other state.
    /// Return null if it cannot move to other state.
    /// </summary>
    public virtual IState GetNextState() => null;
    /// <summary>
    /// Update object when it is in this state.
    /// </summary>
    public virtual void Update() { }
    /// abstr;
    /// FixedUpdate object when it is in this state.
    /// </summary>
    public virtual void FixedUpdate() { }
    /// <summary>
    /// called when exit a st;
    /// </summary>
    public virtual void Exit() { }

    #region NEVER MIND
    public const string __GeneralStateWarning = "This is genaral state constructor, dont use it!! (except in the class that inherit it)";
    public const string __HeadStateWarning = "This is state's head constructor, dont use it!! (except in the ConnectedState)";
    #endregion
}
