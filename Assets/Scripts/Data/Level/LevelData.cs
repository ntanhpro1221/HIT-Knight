using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData {
    [Serializable]
    public class MonsterSpawnData {
        [field: SerializeField]
        public string MonsterId { get; private set; }
        [field: SerializeField]
        public Vector2 SpawnPos { get; private set; }
    }
    [field: SerializeField]
    public GameObject Environment { get; private set; }
    [field: SerializeField]
    public Vector2 PlayerStartPoint { get; private set; }
    [field: SerializeField]
    public List<MonsterSpawnData> MonsterList { get; private set; }
}
