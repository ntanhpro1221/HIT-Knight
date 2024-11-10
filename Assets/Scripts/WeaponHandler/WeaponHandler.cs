using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle weapon to attack.
/// </summary>
public class WeaponHandler : CoreComponent, IWeaponHandler
{
    private IStalker iStalker;
    
    private int curWeaponIndex;
    
    /// <summary>
    /// Current equipted weapon.
    /// </summary>
    public IWeapon CurWeapon { get; private set; }

    /// <summary>
    /// List weapon
    /// </summary>
    public List<IWeapon> ListWeapon { get; }
    
    /// <summary>
    /// replace current weapon by new weapon :v
    /// </summary>
    public void ReplaceWeapon(IWeapon weapon) {
        if (CurWeapon == null)
        {
            CurWeapon = weapon;
            ListWeapon.Add(weapon);
            curWeaponIndex = ListWeapon.IndexOf(weapon);

        }
        else
        {
            ListWeapon.Remove(CurWeapon);
            CurWeapon = weapon;
            ListWeapon.Add(weapon);
            curWeaponIndex = ListWeapon.IndexOf(weapon);
        }
    }
    
    /// <summary>
    /// use next weapon in ListWeapon
    /// </summary>
    public void RollWeapon() {
        if (ListWeapon.Count > 1)
        {
            if (curWeaponIndex + 1 == ListWeapon.Count)
            {
                curWeaponIndex = 0;
                CurWeapon = ListWeapon[curWeaponIndex];
            }
            else
            {
                curWeaponIndex++;
                CurWeapon = ListWeapon[curWeaponIndex];
            }
        }
        else
        {
            Debug.Log("You only have 1 weapon!");
        }
    }
    
    /// <summary>
    /// Perform normal attack
    /// </summary>
    public void Attack()
    {
        if (CurWeapon == null)
        {
            Debug.Log("You don't have any weapon, run for your life!");
            return;
        }
        
        if (CurWeapon is IRangedWeapon rangedWeapon)
        {
            GameObject enemy = iStalker.TopTarget.Value;
            float distance = Vector3.Distance(enemy.transform.position, CurWeapon.transform.position);
            
            if (distance > 10f)
            {
                rangedWeapon.RangedAttack();
            }
            else
            {
                CurWeapon.MeleAttack();
            }
        }
        else if (CurWeapon is IWeapon)
        {
            CurWeapon.MeleAttack();
        }
    }
    
    /// <summary>
    /// Stop attack
    /// </summary>
    public void StopAttack() => CurWeapon?.StopAttack();
}

