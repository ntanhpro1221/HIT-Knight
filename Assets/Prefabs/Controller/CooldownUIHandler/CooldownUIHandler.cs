using AYellowpaper.SerializedCollections.Editor.Data;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUIHandler : MonoBehaviour {
    [SerializeField] private Image image;
    protected Func<(float, float)> getCooldownInfo;
    
    protected virtual void Awake() { }

    /// <summary>
    /// </summary>
    /// <param name="getCooldownInfo">return curCooldown && maxCooldown</param>
    public void Init(Func<(float, float)> getCooldownInfo) {
        this.getCooldownInfo = getCooldownInfo;
    }

    protected virtual void Update() {
        var (cur, max) = getCooldownInfo.Invoke();
        image.fillAmount = Math.Max(0, cur / max);
    }
}
