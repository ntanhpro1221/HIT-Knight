using System;
using UnityEngine;

public class MeleeAttackState_RangedWeapon : ConnectedState<RangedWeaponState, MeleeAttackState_Weapon.HeadState, MeleeAttackState_RangedWeapon.HeadState> {
    public class HeadState : RangedWeaponState {
        [Obsolete(__HeadStateWarning)]
        public HeadState(RangedWeaponSM sm, AnimInfo animInfo, IRangedWeapon weapon) : base(sm, animInfo, weapon) { }
    
        public override void Enter() { }

        public override IState GetNextState() {
            /* To Ranged Attack 
             * -- have ranged attack command
             * -- enough mp to ranged attack */
            if (controlAdapter.RangedAttack != default &&
                EnoughMPToRangedAttack)
                return sm.RangedAttackState;
            /* -- enough distance to ranged attack
             * -- enough mp to ranged attack */
            if (EnoughDistanceToRangedAttack &&
                EnoughMPToRangedAttack)
                return sm.RangedAttackState;

            return null;
        }

        public override void Update() { }

        public override void FixedUpdate() { }

        public override void Exit() { }
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public MeleeAttackState_RangedWeapon(RangedWeaponSM sm, AnimInfo animInfo, IRangedWeapon weapon) : base(
        new(sm, animInfo, weapon),
        new(sm, animInfo, weapon),
        new(sm, animInfo, weapon)) { }
#pragma warning restore CS0618 // Type or member is obsolete
}