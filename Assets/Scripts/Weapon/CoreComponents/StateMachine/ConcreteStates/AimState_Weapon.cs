using System;
using UnityEngine;

public class AimState_Weapon : ConnectedState<WeaponState, AimState_Weapon.HeadState> {
    public class HeadState : WeaponState {
        [Obsolete(__HeadStateWarning)]
        public HeadState(WeaponSM sm, AnimInfo animInfo, IWeapon weapon) : base(sm, animInfo, weapon) { }

        public override void Enter() {
            bodyHandler.PlayAnim(animInfo, 0);
            navigator.FocusOn(stalker.TopTarget);
        }

        public override IState GetNextState() {
            /* To Melee Attack
             * -- have melee attack command */
            if (controlAdapter.MeleeAttack != default)
                return sm.MeleeAttackState;

            /* To Idle
             * -- no command
             * -- no target */
            if (controlAdapter.NoCommand &&
                stalker.TopTarget.Value == null)
                return sm.IdleState;

            return null;
        }

        public override void Update() { }

        public override void FixedUpdate() { }

        public override void Exit() {
            navigator.StopFocus();
            navigator.ResetRotation();
        }
    }

#pragma warning disable CS0618 // Type or member == obsolete
    public AimState_Weapon(WeaponSM sm, AnimInfo animInfo, IWeapon weapon) : base(
        new(sm, animInfo, weapon),
        new(sm, animInfo, weapon)) { }
#pragma warning restore CS0618 // Type or member == obsolete
}