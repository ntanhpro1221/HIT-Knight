using System.Collections.Generic;
/// <summary>
/// Handle weapon to attack.
/// </summary>
public class WeaponHandler : CoreComponent, IWeaponHandler {
    /// <summary>
    /// Current equipted weapon.
    /// </summary>
    public IWeapon CurWeapon => throw new System.NotImplementedException();
    /// <summary>
    /// List weapon
    /// </summary>
    public List<IWeapon> ListWeapon => throw new System.NotImplementedException();
    /// <summary>
    /// replace current weapon by new weapon :v
    /// </summary>
    public void ReplaceWeapon(IWeapon weapon) {
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// use next weapon in ListWeapon
    /// </summary>
    public void RollWeapon() {
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// Perform normal attack
    /// </summary>
    public void Attack() {
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// Stop attack
    /// </summary>
    public void StopAttack() => CurWeapon?.StopAttack();
}

