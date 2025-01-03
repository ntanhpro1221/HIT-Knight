using System;

public class ConnectedState<T1, T2> : IState
    where T1 : IState
    where T2 : IState {
    private readonly T1 state_1;
    private readonly T2 state_2;

    [Obsolete(__GeneralStateWarning)]
    public ConnectedState(
        T1 state_1, 
        T2 state_2) : base(state_2.Name, null) {
        this.state_1 = state_1;
        this.state_2 = state_2;
    }

    public override void Enter() {
        state_1.Enter();
        state_2.Enter();
    }

    public override IState GetNextState() {
        return state_1.GetNextState()
            ?? state_2.GetNextState();
    }

    public override void Update() {
        state_1.Update();
        state_2.Update();
    }

    public override void FixedUpdate() {
        state_1.FixedUpdate();
        state_2.FixedUpdate();
    }

    public override void Exit() {
        state_1.Exit();
        state_2.Exit();
    }

    public static explicit operator T1(ConnectedState<T1, T2> origin) => origin.state_1;
    public static explicit operator T2(ConnectedState<T1, T2> origin) => origin.state_2;
}

public class ConnectedState<T1, T2, T3> : IState
    where T1 : IState
    where T2 : IState
    where T3 : IState {
    private readonly T1 state_1;
    private readonly T2 state_2;
    private readonly T3 state_3;

    [Obsolete(__GeneralStateWarning)]
    public ConnectedState(
        T1 state_1, 
        T2 state_2,
        T3 state_3) : base(state_3.Name, null) {
        this.state_1 = state_1;
        this.state_2 = state_2;
        this.state_3 = state_3;
    }

    public override void Enter() {
        state_1.Enter();
        state_2.Enter();
        state_3.Enter();
    }

    public override IState GetNextState() {
        return state_1.GetNextState()
            ?? state_2.GetNextState()
            ?? state_3.GetNextState();
    }

    public override void Update() {
        state_1.Update();
        state_2.Update();
        state_3.Update();
    }

    public override void FixedUpdate() {
        state_1.FixedUpdate();
        state_2.FixedUpdate();
        state_3.FixedUpdate();
    }

    public override void Exit() {
        state_1.Exit();
        state_2.Exit();
        state_3.Exit();
    }

    public static explicit operator T1(ConnectedState<T1, T2, T3> origin) => origin.state_1;
    public static explicit operator T2(ConnectedState<T1, T2, T3> origin) => origin.state_2;
    public static explicit operator T3(ConnectedState<T1, T2, T3> origin) => origin.state_3;
}
