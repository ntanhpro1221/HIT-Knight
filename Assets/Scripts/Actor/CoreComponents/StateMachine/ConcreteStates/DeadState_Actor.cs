using System;

public class DeadState_Actor : ConnectedState<ActorState, DeadState_Actor.HeadState> {
    public class HeadState : ActorState {
        protected WeaponHandler weaponHandler;
        protected HealthHandler healthHandler;

        [Obsolete(__HeadStateWarning)]
        public HeadState(ActorSM sm, AnimInfo anim, IActor actor) : base(sm, anim, actor) {
            weaponHandler = actor.WeaponHandler;
            healthHandler = actor.HealthHandler;
        }

        public override void Enter() {
            bodyHandler.PlayAnim(animInfo, 0);
            weaponHandler.StoreWeapon();
        }

        public override IState GetNextState() {
            /* To Idle
             * -- no command
             * -- dead done */
            if (controlHelper.NoCommand &&
                healthHandler.IsDead == false)
                return sm.IdleState;

            /* To Attack
             * -- have attack command
             * -- ready to attack
             * -- dead done */
            if (controlHelper.Attack != default &&
                weaponHandler.IsReadyToAttack &&
                healthHandler.IsDead == false)
                return sm.AttackState;

            /* To Move
             * -- have move by dir command || have move by pos command
             * -- dead done */
            if ((controlHelper.MoveByDir != default || controlHelper.MoveByPos != default) &&
                healthHandler.IsDead == false)
                return sm.MoveState;

            /* To Dash
             * -- have dash command
             * -- ready to dash
             * -- dead done */
            if (controlHelper.Dash != default &&
                moveHandler.IsReadyToDash &&
                healthHandler.IsDead == false)
                return sm.DashState;

            return null;
        }

        public override void Update() { }

        public override void FixedUpdate() { }

        public override void Exit() {
            weaponHandler.HoldFirstWeapon();
        }
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public DeadState_Actor(ActorSM sm, AnimInfo anim, IActor actor) : base(
        new(sm, anim, actor),
        new(sm, anim, actor)) { }
#pragma warning restore CS0618 // Type or member is obsolete
}