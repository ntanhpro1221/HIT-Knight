using Cinemachine;
using NavMeshPlus.Components;
using NavMeshPlus.Extensions;
using System.Collections;
using UnityEngine;

public class LevelGenerator : SceneSingleton<LevelGenerator> {
    [SerializeField] private CinemachineVirtualCamera cinemachineCam;
    
    public void Generate(LevelData data, string selectedHero) {
        // spawn environment   
        GameObject env = Instantiate(data.Environment);

        // gen mesh surface
        IEnumerator GenNavMesh() {
            yield return new WaitForFixedUpdate();
            GetComponentInChildren<RootSources2d>().RootSources.Add(env);
            GetComponentInChildren<NavMeshSurface>().BuildNavMesh();
        }
        StartCoroutine(GenNavMesh());

        // spawn hero
        GameObject hero = Instantiate(
            AllPrefabRef.Instance.Hero[selectedHero], 
            data.PlayerStartPoint, 
            Quaternion.identity);

        // attach controller to player
        PlayerControllerInBattle.Instance.Init(hero);

        // attach cinemachine to hero
        cinemachineCam.Follow = hero.transform;
        cinemachineCam.LookAt = hero.transform;

        // spawn all monster
        foreach (var spawnData in data.MonsterList)
            Instantiate(
                AllPrefabRef.Instance.Monster[spawnData.MonsterId],
                spawnData.SpawnPos,
                Quaternion.identity);
    }
}
