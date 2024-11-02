using System.Collections.Generic;
/// <summary>
/// Handle weapon to attack.
/// </summary>
public interface IWeaponHandler 
{
    /// <summary>
    /// Current equipted weapon.
    /// </summary>
    IWeapon CurWeapon { get; }
    /// <summary>
    /// List weapon
    /// </summary>
    List<IWeapon> ListWeapon { get; }
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
}

