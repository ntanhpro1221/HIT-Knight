using System;
using System.IO.Compression;
using UnityEngine;

public class MeleeAttackState_Weapon : ConnectedState<WeaponState, MeleeAttackState_Weapon.HeadState> {
    public class HeadState : WeaponState {
        private WeaponAttacker attacker;

        [Obsolete(__HeadStateWarning)]
        public HeadState(WeaponSM sm, AnimInfo animInfo, IWeapon weapon) : base(sm, animInfo, weapon) {
            attacker = weapon.Attacker;
        }

        public override void Enter() {
            bodyHandler.PlayAnim(animInfo, 0,
                1.0f / weapon.StatsHandler.CurStats[WeaponStatType.Speed].Value);
            attacker.MeleeAttack();
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
             * -- attack done || have stop command
             * -- no target */
            if ((attacker.IsDoneAttack || controlAdapter.StopAttack != default) &&
                stalker.TopTarget.Value == null)
                return sm.IdleState;
 
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
    public MeleeAttackState_Weapon(WeaponSM sm, AnimInfo animInfo, IWeapon weapon) : base(
        new(sm, animInfo, weapon),
        new(sm, animInfo, weapon)) { }
#pragma warning restore CS0618 // Type or member is obsolete
}