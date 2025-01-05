using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

[Serializable]
public class SpriteData {
    public SerializedDictionary<string, Sprite> HeroIllus;
    public SerializedDictionary<string, Sprite> MonsterIllus;
    public SerializedDictionary<string, Sprite> WeaponIllus;
    public SerializedDictionary<string, Sprite> RuneIllus;
}
