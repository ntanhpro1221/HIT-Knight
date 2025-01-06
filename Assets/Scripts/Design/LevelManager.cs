using UnityEngine;

public class LevelManager : SceneSingleton<LevelManager> {
    protected override void Awake() {
        LevelGenerator.Instance.Generate(DataManager.Instance.LevelData["001"], "001"); 
    }
}
