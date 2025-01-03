using System;
using Unity.VisualScripting;
using UnityEngine;

public class DashState_Actor : ConnectedState<ActorState, DashState_Actor.HeadState> {
    public class HeadState : ActorState {
        protected WeaponHandler weaponHandler;

        [Obsolete(__HeadStateWarning)]
        public HeadState(ActorSM sm, AnimInfo anim, IActor actor) : base(sm, anim, actor) {
            weaponHandler = actor.WeaponHandler;
        }
        
        protected Vector2 GetDashDir() { // hmmm
            Vector2 dashDir = navigator.CurDir;

            dashDir = controlHelper.Dash;

            return dashDir;
        }

        public override void Enter() {
            bodyHandler.PlayAnim(animInfo, 0);
            Vector2 dashDir = GetDashDir();
            navigator.CurDir = dashDir;
            moveHandler.Dash(dashDir);
        }

        public override IState GetNextState() {
            /* To Dead: processed in parrent */

            /* To Idle
             * -- no command
             * -- dash done */
            if (controlHelper.NoCommand &&
                moveHandler.CurState != MoveHandler.MoveState.Dash)
                return sm.IdleState;

            /* To Attack
             * -- have attack command
             * -- ready to attack
             * -- dash done */
            if (controlHelper.Attack != default &&
                weaponHandler.IsReadyToAttack &&
                moveHandler.CurState != MoveHandler.MoveState.Dash)
                return sm.AttackState;

            /* To Move
             * -- have move by dir command || have move by pos command
             * -- dash done */
            if ((controlHelper.MoveByDir != default || controlHelper.MoveByPos != default) &&
                moveHandler.CurState != MoveHandler.MoveState.Dash)
                return sm.MoveState;

            return null;
        }

        public override void Update() { }

        public override void FixedUpdate() { }

        public override void Exit() {
            moveHandler.StopMove();
        }
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public DashState_Actor(ActorSM sm, AnimInfo anim, IActor actor) : base(
        new(sm, anim, actor),
        new(sm, anim, actor)) { }
#pragma warning restore CS0618 // Type or member is obsolete
}