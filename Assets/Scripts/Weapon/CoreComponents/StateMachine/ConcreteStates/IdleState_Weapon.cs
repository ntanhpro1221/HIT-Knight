using System;
using UnityEngine;

public class IdleState_Weapon : ConnectedState<WeaponState, IdleState_Weapon.HeadState> {
    public class HeadState : WeaponState {
        [Obsolete(__HeadStateWarning)]
        public HeadState(WeaponSM sm, AnimInfo animInfo, IWeapon weapon) : base(sm, animInfo, weapon) { }

        public override void Enter() {
            bodyHandler.PlayAnim(animInfo, 0);
        }

        public override IState GetNextState() {
            /* To Melee Attack
             * -- have melee attack command */
            if (controlAdapter.MeleeAttack != default)
                return sm.MeleeAttackState;

            /* To Aim
             * -- no command
             * -- have target */
            if (controlAdapter.NoCommand &&
                stalker.TopTarget.Value != null)
                return sm.AimState;

            return null;
        }
 
        public override void Update() {
            navigator.CurDir = new Vector2(weapon.WeaponHandler.Actor.Navigator.CurDir.x, 0);
        }

        public override void FixedUpdate() { }

        public override void Exit() { }
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public IdleState_Weapon(WeaponSM sm, AnimInfo animInfo, IWeapon weapon) : base(
        new(sm, animInfo, weapon),
        new(sm, animInfo, weapon)) { }
#pragma warning restore CS0618 // Type or member is obsolete
}