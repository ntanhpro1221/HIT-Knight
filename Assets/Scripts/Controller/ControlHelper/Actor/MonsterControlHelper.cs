using System;
using UnityEngine;

public class MonsterControlHelper : IActorControlHelper {
    public BaseMonster Monster => (Core as ActorCore).Actor as BaseMonster;

    private Action onHaveTriggeredCommand;

    private void MoveByDir_Callback(object param) {
        MoveByDir = (Vector2)param;
        onHaveTriggeredCommand.Invoke();
        MoveByDir = default;
    }

    private void MoveByPos_Callback(object param) {
        MoveByPos = (Vector2?)param;
        onHaveTriggeredCommand.Invoke();
        MoveByPos = default;
    }

    private void Attack_Callback(object param) {
        Attack = true;
        onHaveTriggeredCommand.Invoke();
        Attack = default;
    }

    private void Dash_Callback(object param) {
        Dash = (Vector2)param;
        onHaveTriggeredCommand.Invoke();
        Dash = default;
    }

    public override bool NoCommand {
        get =>
            MoveByDir == default &&
            MoveByPos == default &&
            Attack == default &&
            Dash == default;
        protected set { }
    }

    public override Vector2 MoveByDir { get; protected set; }

    public override Vector2? MoveByPos { get; protected set; }

    public override bool Attack { get; protected set; }

    public override Vector2 Dash { get; protected set; }

    private void Awake() {
        onHaveTriggeredCommand = Monster.StateMachine.UpdateState;

        Monster.Commander.AddListener(ActorCommand.MoveByDir, MoveByDir_Callback);
        Monster.Commander.AddListener(ActorCommand.MoveByPos, MoveByPos_Callback);
        Monster.Commander.AddListener(ActorCommand.Attack, Attack_Callback);
        Monster.Commander.AddListener(ActorCommand.Dash, Dash_Callback);
    }
}
