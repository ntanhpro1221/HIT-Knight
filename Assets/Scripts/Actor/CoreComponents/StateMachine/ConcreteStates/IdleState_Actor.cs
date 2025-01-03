using System;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public class IdleState_Actor : ConnectedState<ActorState, IdleState_Actor.HeadState> {
    public class HeadState : ActorState {
        private WeaponHandler weaponHandler;

        [Obsolete(__HeadStateWarning)]
        public HeadState(ActorSM sm, AnimInfo anim, IActor actor) : base(sm, anim, actor) {
            weaponHandler = actor.WeaponHandler;
        }

        public override void Enter() {
            bodyHandler.PlayAnim(animInfo, 0);
        }

        public override IState GetNextState() {
            /* To Dead: processed in parrent */

            /* To Attack
             * -- have attack command
             * -- ready to attack */
            if (controlHelper.Attack != default &&
                weaponHandler.IsReadyToAttack)
                return sm.AttackState;

            /* To Dash
             * -- have dash command
             * -- ready to dash */
            if (controlHelper.Dash != default &&
                moveHandler.IsReadyToDash)
                return sm.DashState;

            /* To Move
             * -- have move by dir command || have move by pos command */
            if (controlHelper.MoveByDir != default || controlHelper.MoveByPos != default)
                return sm.MoveState;

            return null;
        }

        public override void Update() { }

        public override void FixedUpdate() { }

        public override void Exit() { }
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public IdleState_Actor(ActorSM sm, AnimInfo anim, IActor actor) : base(
        new(sm, anim, actor),
        new(sm, anim, actor)) { }
#pragma warning restore CS0618 // Type or member is obsolete
}