using System.Collections.Generic;
/// <summary>
/// Handle weapon to attack.
/// </summary>
public interface IWeaponHandler {
    float CurCoolDown { get; }
    float MaxCoolDown { get; }
    bool IsDoneAttack { get; }
    bool EnoughMPToRangedAttack { get; }
    bool EnoughDistanceToRangedAttack { get; }
    /// <summary>
    /// Current MP of actor
    /// </summary>
    BindableProperty<float> CurMP { get; }
    /// <summary>
    /// provide for weapon to aim
    /// </summary>
    IStalker Stalker { get; }
    /// <summary>
    /// Actor use this
    /// </summary>
    IActor Actor { get; }
    /// <summary>
    /// There are two phases when attacking: before and after you actually deal damage 
    /// (or when the bullet is actually fired if you use a ranged weapon).
    ///  This variable will be true if you are in the first phase
    /// </summary>
    bool IsBeginAttack { get; }
    /// <summary>
    /// Something relative to attack speed
    /// </summary>
    bool IsReadyToAttack { get; }
    /// <summary>
    /// Current equipted weapon.
    /// </summary>
    IWeapon CurWeapon { get; }
    /// <summary>
    /// List weapon
    /// </summary>
    List<IWeapon> ListWeapon { get; }
    /// <summary>
    /// pick up weapon
    /// </summary>
    void AddWeapon(IWeapon weapon);
    /// <summary>
    /// remove current weapon
    /// </summary>
    void RemoveCurrentWeapon();
    /// <summary>
    /// replace current weapon by new weapon :v
    /// </summary>
    void ReplaceWeapon(IWeapon weapon);
    /// <summary>
    /// use next weapon in ListWeapon
    /// </summary>
    void RollWeapon();
    /// <summary>
    /// Perform normal attack.
    /// </summary>
    void Attack();
    /// <summary>
    /// Stop attack
    /// </summary>
    void StopAttack();
    /// <summary>
    /// Put down weapon, dont hold weapon but it still in your inventory
    /// </summary>
    void StoreWeapon();
    /// <summary>
    /// Hold first weapon in you inventory
    /// </summary>
    void HoldFirstWeapon();
}

