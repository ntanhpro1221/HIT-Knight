using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUserData", menuName = "Game Data/User Data")]
public class UserDataSO : ScriptableObject
{
    public string id;
    public string name;
    public int gold;
    //public Dictionary<string, HeroDynamicData> heroData;
    //public Dictionary<string, WeaponDynamicData> weaponData;

    public List<HeroDynamicData> heroDataList;
    public List<WeaponDynamicData> weaponDataList;

    public List<RuneDynamicData> actorRuneData;
    public List<RuneDynamicData> weaponRuneData;

    public Dictionary<string, HeroDynamicData> GetHeroDataDictionary()
    {
        Dictionary<string, HeroDynamicData> heroDataDict = new Dictionary<string, HeroDynamicData>();
        foreach (var hero in heroDataList)
        {
            heroDataDict[hero.id] = hero;
        }
        return heroDataDict;
    }

    public Dictionary<string, WeaponDynamicData> GetWeaponDataDictionary()
    {
        Dictionary<string, WeaponDynamicData> weaponDataDict = new Dictionary<string, WeaponDynamicData>();
        foreach (var weapon in weaponDataList)
        {
            weaponDataDict[weapon.id] = weapon;
        }
        return weaponDataDict;
    }

    public bool AddHero(HeroDynamicData newHero)
    {
        if (heroDataList.Exists(hero => hero.id == newHero.id))
        {
            Debug.LogWarning("Hero ID '{newHero.id}' has existed!");
            return false; 
        }
        heroDataList.Add(newHero);
        return true;
    }

    public bool AddWeapon(WeaponDynamicData newWeapon)
    {
        if (weaponDataList.Exists(weapon => weapon.id == newWeapon.id))
        {
            Debug.LogWarning($"Weapon ID '{newWeapon.id}' has existed!");
            return false;
        }
        weaponDataList.Add(newWeapon);
        return true; 
    }

    public bool AddRune(RuneDynamicData newRune, List<RuneDynamicData> runeList)
    {
        if (runeList.Exists(rune => rune.id == newRune.id))
        {
            Debug.LogWarning($"Rune ID '{newRune.id}' has existed!");
            return false;
        }
        runeList.Add(newRune);
        return true;
    }
}
