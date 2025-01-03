using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Handle weapon to attack.
/// </summary>
public class WeaponHandler : WeaponCommander, IWeaponHandler
#if UNITY_EDITOR
    , ISerializationCallbackReceiver
#endif
    {
#region JUST SHOW SOMETHING IN IN SPECTOR
#if UNITY_EDITOR
    [SerializeField] private string CurWeaponShower = "Nope";

    public void OnBeforeSerialize() {
        if (EditorApplication.isPlaying == false) return;
        CurWeaponShower = CurWeapon?.gameObject.name ?? "Nope";
    }

    public void OnAfterDeserialize() { }
#endif
#endregion
    private IWeapon m_CurWeapon;

    protected virtual void Awake() {
        foreach (IWeapon weapon in GetComponentsInChildren<IWeapon>().Where(ele => transform == ele.transform.parent))
            AddWeapon(weapon);
    }

    public BindableProperty<float> CurMP
        => GetCoreComponent<ActorStatsHandler>().CurStats[ActorStatType.MP];

    public IStalker Stalker
        => GetCoreComponent<IStalker>();

    public float CurCoolDown
        => CurWeapon?.Attacker.CurCooldown ?? 1;

    public float MaxCoolDown
        => CurWeapon?.Attacker.MaxCooldown ?? 1;

    public bool IsDoneAttack 
        => CurWeapon?.Attacker.IsDoneAttack ?? false;

    public bool IsBeginAttack 
        => CurWeapon?.Attacker.IsBeginAttack ?? false;

    public bool IsReadyToAttack 
        => CurWeapon?.Attacker.IsReadyToAttack ?? false;

    public bool EnoughMPToRangedAttack =>
        CurWeapon != null &&
        CurMP.Value >= CurWeapon.StatsHandler.CurStats[WeaponStatType.MPCost].Value;

    public bool EnoughDistanceToRangedAttack =>
        Stalker.TopTarget.Value != null &&
        CurWeapon != null &&
        2 < Vector2.Distance(
            Stalker.TopTarget.Value.transform.position, 
            CurWeapon.transform.position);

    public IWeapon CurWeapon {
        get => m_CurWeapon;
        private set {
            m_CurWeapon?.UnbindCommander();
            m_CurWeapon?.gameObject.SetActive(false);

            m_CurWeapon = value;

            m_CurWeapon?.Init(this);
            m_CurWeapon?.gameObject.SetActive(true);
        }
    }

    public List<IWeapon> ListWeapon 
        { get; } = new();

    public void AddWeapon(IWeapon weapon) {
        ListWeapon.Add(weapon);
        CurWeapon = weapon;
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = new Vector3(0, 0.5f, -0.001f);
    }
    
    public void RemoveCurrentWeapon() {
        if (CurWeapon == null) return;

        CurWeapon.transform.localPosition = CurWeapon.transform.position;
        CurWeapon.transform.SetParent(null);
        IWeapon tmpWeapon = CurWeapon;
        ListWeapon.Remove(CurWeapon);
        CurWeapon = ListWeapon.FirstOrDefault();
        tmpWeapon.gameObject.SetActive(true);
    }

    public void ReplaceWeapon(IWeapon weapon) {
        RemoveCurrentWeapon();
        AddWeapon(weapon);
    }

    public void RollWeapon() {
        if (ListWeapon.Count > 1)
            CurWeapon = ListWeapon[(ListWeapon.IndexOf(CurWeapon) + 1) % ListWeapon.Count];
        else
            Debug.Log("You only have <= 1 weapon!");
    }

    public void Attack() {
        if (CurWeapon == null) {
            Debug.Log("You don't have any weapon, run for your life!");
            return;
        }

        if (CurWeapon is IRangedWeapon &&
            EnoughDistanceToRangedAttack &&
            EnoughMPToRangedAttack) 
            PostCommand(WeaponCommand.RangedAttack);
        else if (CurWeapon is IWeapon) 
            PostCommand(WeaponCommand.MeleeAttack);
    }

    public void StopAttack() 
        => PostCommand(WeaponCommand.StopAttack);

    public void StoreWeapon() {
        CurWeapon = null;
    }

    public void HoldFirstWeapon() {
        CurWeapon = ListWeapon.FirstOrDefault();
    }
}

