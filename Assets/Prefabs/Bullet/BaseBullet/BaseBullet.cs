using UnityEngine;

public class BaseBullet : IBullet {
    private bool isExploding = false;
    protected override void OnTriggerEnter2D(Collider2D coll) {
        if (m_Weapon.WeaponHandler.Stalker.ValidateTarget_FromTheBeginning(coll) is false) return;
        GameObject target = m_Weapon.WeaponHandler.Stalker.ToTargetType(coll);
        if (m_Weapon.WeaponHandler.Stalker.ValidateTarget_ByCurBehaviour(target) is false) return;
        
        IHealthHandler targetHealth = target.GetComponent<IActor>().HealthHandler;
        targetHealth.TakeDamage(m_Weapon.StatsHandler.CurStats[WeaponStatType.Atk].Value);

        if (isExploding == false) {
            isExploding = true;
            m_Rb.velocity = Vector2.zero;
            BodyHandler.PlayAnim(BulletBodyHandler.Explode);
            BodyHandler.RegisterAnimEvent(BulletAnimEventType.DONE_EXPLODE, OnDoneExplode);
        }
    }
    private void OnDoneExplode() {
        Destroy(gameObject); 
    }
    private void OnDestroy() {
        BodyHandler.RemoveAnimEvent(BulletAnimEventType.DONE_EXPLODE, OnDoneExplode);
    }
}
