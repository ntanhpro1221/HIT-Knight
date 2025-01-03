using System;
using UnityEngine;

public class AttackState_Actor : ConnectedState<ActorState, AttackState_Actor.HeadState> {
    public class HeadState : ActorState {
        protected WeaponHandler weaponHandler;

        [Obsolete(__HeadStateWarning)]
        public HeadState(ActorSM sm, AnimInfo anim, IActor actor) : base(sm, anim, actor) {
            weaponHandler = actor.WeaponHandler;
        }

        public override void Enter() {
            bodyHandler.PlayAnim(animInfo, 0);
            if (actor.Stalker.TopTarget.Value != null)
                navigator.LookAt(actor.Stalker.TopTarget.Value.transform.position);
            weaponHandler.Attack();
        }

        public override IState GetNextState() {
            /* To Dead: processed in parrent */

            /* To Idle
             * -- no command
             * -- attack done */
            if (controlHelper.NoCommand &&
                weaponHandler.IsDoneAttack)
                return sm.IdleState;

            /* To Dash
             * -- have dash command
             * -- ready to dash
             * -- not in the first stages of an attack (attacking but not actually dealing damage yet) */
            if (controlHelper.Dash != default &&
                moveHandler.IsReadyToDash &&
                weaponHandler.IsBeginAttack == false)
                return sm.DashState;

            /* To Move
             * -- have move by dir command || have move by pos command
             * -- not in the first stages of an attack (attacking but not actually dealing damage yet) */
            if ((controlHelper.MoveByDir != default || controlHelper.MoveByPos != default) &&
                weaponHandler.IsBeginAttack == false)
                return sm.MoveState;

            return null;
        }

        public override void Update() {
            if (weaponHandler.IsReadyToAttack)
                weaponHandler.Attack();
        }

        public override void FixedUpdate() { }

        public override void Exit() {
            weaponHandler.StopAttack();
        }
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public AttackState_Actor(ActorSM sm, AnimInfo anim, IActor actor) : base(
        new(sm, anim, actor),
        new(sm, anim, actor)) { }
#pragma warning restore CS0618 // Type or member is obsolete
}