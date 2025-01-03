using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPShower : MonoBehaviour {
    [SerializeField] private SpriteRenderer bg;
    [SerializeField] private SpriteRenderer fg;
    private float curHP;
    private float maxHP;

    private void UpdateHPUI() {
        bg.enabled = fg.enabled = curHP > 0;
        Vector3 scale = fg.transform.localScale;
        scale.x = Mathf.Clamp01(curHP / maxHP);
        fg.transform.localScale = scale;
    }

    private void Awake() {
        HealthHandler healthHandler = GetComponentInParent<HealthHandler>();
        curHP = healthHandler.CurHealth.Value;
        healthHandler.CurHealth.OnChanged.AddListener(newCurHP => {
            this.curHP = newCurHP;
            UpdateHPUI();
        });
        maxHP = healthHandler.MaxHealth.Value;
        healthHandler.MaxHealth.OnChanged.AddListener(newMaxHP => {
            this.maxHP = newMaxHP;
            UpdateHPUI();
        });
    }
}
