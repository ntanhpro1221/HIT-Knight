using System;
using UnityEngine;

public class RangedAttackState_RangedWeapon : ConnectedState<RangedWeaponState, RangedAttackState_RangedWeapon.HeadState> {
    public class HeadState : RangedWeaponState {
        private RangedWeaponAttacker attacker;

        [Obsolete(__HeadStateWarning)]
        public HeadState(RangedWeaponSM sm, AnimInfo anim, IRangedWeapon weapon) : base(sm, anim, weapon) {
            attacker = weapon.Attacker;
        }

        public override void Enter() {
            bodyHandler.PlayAnim(animInfo, 0);
            attacker.RangedAttack();
            navigator.FocusOn(stalker.TopTarget);
        }

        public override IState GetNextState() {
            /* To Aim
             * -- attack done || have stop command
             * -- have target */
            if ((attacker.IsDoneAttack || controlAdapter.StopAttack != default) &&
                stalker.TopTarget.Value != null)
                return sm.AimState;

            /* To Idle
             * -- no command || have stop command
             * -- no target */
            if ((attacker.IsDoneAttack || controlAdapter.StopAttack != default) &&
                stalker.TopTarget.Value == null)
                return sm.IdleState;

            /* To Melee Attack
             * -- have melee attack command */
            if (controlAdapter.MeleeAttack != default)
                return sm.MeleeAttackState;
            /* -- not enough distance to ranged attack */
            if (EnoughDistanceToRangedAttack == false)
                return sm.MeleeAttackState;
            /* -- not enough mp to ranged attack */
            if (EnoughMPToRangedAttack == false)
                return sm.MeleeAttackState;

            return null;
        }

        public override void Update() { }

        public override void FixedUpdate() { }

        public override void Exit() {
            attacker.StopAttack();
            navigator.StopFocus();
            navigator.ResetRotation();
        }
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public RangedAttackState_RangedWeapon(RangedWeaponSM sm, AnimInfo anim, IRangedWeapon weapon) : base(
        new(sm, anim, weapon),
        new(sm, anim, weapon)) { }
#pragma warning restore CS0618 // Type or member is obsolete
}