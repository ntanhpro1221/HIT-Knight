using System;

[Obsolete("This class is only meant as a guide. If you have your own IState class, " +
    "your AnyState class must inherit from it instead of this class")]
public abstract class AnyState : IState {
    public AnyState(IStateMachine sm) : base(sm, null, null) { }
}