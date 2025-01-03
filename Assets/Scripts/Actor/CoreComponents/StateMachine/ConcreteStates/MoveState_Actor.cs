using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MoveState_Actor : ConnectedState<ActorState, MoveState_Actor.HeadState> {
    public class HeadState : ActorState {
        protected WeaponHandler weaponHandler;

        [Obsolete(__HeadStateWarning)]
        public HeadState(ActorSM sm, AnimInfo anim, IActor actor) : base(sm, anim, actor) {
            weaponHandler = actor.WeaponHandler;
        }

        public override void Enter() {
            bodyHandler.PlayAnim(animInfo);
            UpdateMovement();
        }

        public override IState GetNextState() {
            /* To Dead: processed in parrent */

            /* To Idle
             * -- no command
             * -- not move by pos */
            if (controlHelper.NoCommand &&
                moveHandler.CurState != MoveHandler.MoveState.MoveByPos)
                return sm.IdleState;

            /* To Attack
             * -- have attack command
             * -- ready to attack */
            if (controlHelper.Attack != default &&
                weaponHandler.IsReadyToAttack)
                return sm.AttackState;

            /* To Dash
             * -- have dash command
             * -- ready to dash*/
            if (controlHelper.Dash != default && 
                moveHandler.IsReadyToDash)
                return sm.DashState;

            return null;
        }

        public override void Update() {
            UpdateMovement();
        }

        public override void FixedUpdate() { }

        public override void Exit() {
            moveHandler.StopMove();
        }

        protected void UpdateMovement() {
            if (controlHelper.MoveByDir != default)
                moveHandler.MoveByDir(controlHelper.MoveByDir);
            else if (controlHelper.MoveByPos != default)
                moveHandler.MoveByPos((Vector2)controlHelper.MoveByPos);

            navigator.CurDir = actor.RB.velocity;
        }
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public MoveState_Actor(ActorSM sm, AnimInfo anim, IActor actor) : base(
        new(sm, anim, actor),
        new(sm, anim, actor)) { }
#pragma warning restore CS0618 // Type or member is obsolete
}