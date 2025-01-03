using UnityEngine;

public class RangedWeaponAttacker : WeaponAttacker {
    protected new IRangedWeapon Weapon => base.Weapon as IRangedWeapon;

    [SerializeField]
    protected bool IsRangedAttacking = false;
    protected Transform BulletStartPoint { get; private set; }

    protected override void Awake() {
        base.Awake();

        BulletStartPoint = GetCoreComponent<RangedWeaponBodyHandler>().BulletStartPoint;
    }

    protected override void BindAnimEvents() {
        base.BindAnimEvents();

        WeaponBodyHandler bodyHandler = GetCoreComponent<WeaponBodyHandler>();
        bodyHandler.RegisterAnimEvent(WeaponAnimEventType.ON_DONE_BEGIN_ATTACK, () => {
            if (IsRangedAttacking) {
                Instantiate(Weapon.m_BulletObj).GetComponent<IBullet>()
                .Launch(BulletStartPoint, Weapon.Navigator.CurDir, Weapon);
            }
        });
    }

    protected override void OnTouchedTarget(Collider2D coll) {
        if (IsRangedAttacking == true) return;

        base.OnTouchedTarget(coll);
    }

    protected virtual void OnStart_Ranged() {
        OnStart_Melee();
        IsRangedAttacking = true;
    }

    protected virtual void OnDone_Ranged() {
        OnDone_Melee();
        IsRangedAttacking = false;
    }

    public void RangedAttack() 
        => Attack(OnStart_Ranged, OnDone_Ranged);
}