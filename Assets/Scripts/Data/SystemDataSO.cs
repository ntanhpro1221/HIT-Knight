using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SystemData", menuName = "Game Data/System Data")]
public class SystemDataSO : ScriptableObject
{
    //public Dictionary<string, HeroStaticData> heroData;
    //public Dictionary<string, EnemyStaticData> enemyData;
    //public Dictionary<string, RuneStaticData<ActorStatType>> actorRuneData;
    //public Dictionary<string, RuneStaticData<WeaponStatType>> weaponRuneData;
    //public Dictionary<string, WeaponStaticData> weaponData;

    public List<HeroStaticData> heroDataList; 
    public List<EnemyStaticData> enemyDataList; 
    public List<RuneStaticData<ActorStatType>> actorRuneDataList; 
    public List<RuneStaticData<WeaponStatType>> weaponRuneDataList; 
    public List<WeaponStaticData> weaponDataList;

    public Dictionary<string, HeroStaticData> GetHeroDataDictionary()
    {
        var dict = new Dictionary<string, HeroStaticData>();
        foreach (var hero in heroDataList)
        {
            if (!dict.ContainsKey(hero.id))
                dict.Add(hero.id, hero);
        }
        return dict;
    }

    public Dictionary<string, EnemyStaticData> GetEnemyDataDictionary()
    {
        var dict = new Dictionary<string, EnemyStaticData>();
        foreach (var enemy in enemyDataList)
        {
            if (!dict.ContainsKey(enemy.id))
                dict.Add(enemy.id, enemy);
        }
        return dict;
    }

    public Dictionary<string, RuneStaticData<ActorStatType>> GetActorRuneDataDictionary()
    {
        var dict = new Dictionary<string, RuneStaticData<ActorStatType>>();
        foreach (var rune in actorRuneDataList)
        {
            if (!dict.ContainsKey(rune.id))
                dict.Add(rune.id, rune);
        }
        return dict;
    }

    public Dictionary<string, RuneStaticData<WeaponStatType>> GetWeaponRuneDataDictionary()
    {
        var dict = new Dictionary<string, RuneStaticData<WeaponStatType>>();
        foreach (var rune in weaponRuneDataList)
        {
            if (!dict.ContainsKey(rune.id))
                dict.Add(rune.id, rune);
        }
        return dict;
    }

    public Dictionary<string, WeaponStaticData> GetWeaponDataDictionary()
    {
        var dict = new Dictionary<string, WeaponStaticData>();
        foreach (var weapon in weaponDataList)
        {
            if (!dict.ContainsKey(weapon.id))
                dict.Add(weapon.id, weapon);
        }
        return dict;
    }
}
