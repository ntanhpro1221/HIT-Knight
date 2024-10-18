using System;

/// <summary>
/// Single state of a state machine.
/// </summary>
/// <typeparam name="T">Type of body handler</typeparam>
public abstract class IState {
    protected IStateMachine sm;
    protected AnimInfo anim;
    protected IAnimUsable animUser;

    public IState(IStateMachine sm, AnimInfo anim, IAnimUsable animUser) {
        this.sm = sm;
        this.anim = anim;
        this.animUser = animUser;
    }
    
    /// <summary>
    /// Be called when start a state.
    /// </summary>
    public virtual void Enter() => animUser?.PlayAnim(anim, 0);
    /// <summary>
    /// Find out if it can move to any other state.
    /// Return null if it cannot move to other state.
    /// </summary>
    public virtual IState GetNextState() => null;
    /// <summary>
    /// Update object when it is in this state.
    /// </summary>
    public virtual void Update() { }
    /// <summary>
    /// FixedUpdate object when it is in this state.
    /// </summary>
    public virtual void FixedUpdate() { }
    /// <summary>
    /// Be called when exit a state.
    /// </summary>
    public virtual void Exit() { }
}
