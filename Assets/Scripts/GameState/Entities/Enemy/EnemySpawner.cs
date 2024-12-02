using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] EnemyWaveList waveList;
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<GameObject> wavePrefabs = new();

    private float timer;
    private List<EnemyWave> waves;

    private void Start()
    {
        waves = waveList.waves.Select(item => (EnemyWave)item.Clone()).ToList(); // clone items so we don't edit the prefab
        waves.Sort((w1, w2) => (w1.startTime.CompareTo(w2.startTime)));
    }


    void Update() {
        timer += Time.deltaTime;

        while (waves.Count > 0 && waves[0].startTime < timer)
        {
            SpawnWave(waves[0]);
            waves.RemoveAt(0);
        }
    }

    void SpawnWave(EnemyWave wave) {
        var pref = wavePrefabs[wave.wavePrefabIdx];
        Instantiate(pref, spawnPoint);
        GameStateManager.Instance.EnemySpawned(pref.transform.childCount);
        Debug.Log(pref.transform.childCount + " " + GameStateManager.Instance.GetTotalEnemies());
    }
}
