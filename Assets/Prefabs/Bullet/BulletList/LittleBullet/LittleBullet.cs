using UnityEngine;

public class LittleBullet : IBullet {
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Enemy")) return;

        IHealthHandler targetHealth = other.gameObject.GetComponent<IActor>().HealthHandler;
        targetHealth.TakeDamage(m_Weapon.StatsHandler.CurStats[WeaponStatType.Atk].Value);

        BodyHandler.PlayAnim(BulletBodyHandler.Explode);
        BodyHandler.RegisterAnimEvent(BulletAnimEventType.DONE_EXPLODE, OnDoneExplode);
    }
    private void OnDoneExplode() {
        Destroy(gameObject); 
    }
    private void OnDestroy() {
        BodyHandler.RemoveAnimEvent(BulletAnimEventType.DONE_EXPLODE, OnDoneExplode);
    }
}
