using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] wave;
    [SerializeField] private GameObject spawnPoint;
    private int current;
    private Coroutine spawn;
    private Coroutine wormSpawn;
    private Coroutine criminalSpawn;
    private int waveNumber;
    public int Current { get => current; set { current = value;  } }

    private GameManager gm;
    private static WaveManager instance;
    public static WaveManager GetWaveManager() => instance;
    void Start() {
        gm = GameManager.GetGameManager();
        UIManager.startGame += StartSpawning;
        GameManager.stopSpawning += StopSpawning;
        current = 0;
        waveNumber = 0;
    }
    private void StartSpawning() {
        spawn=StartCoroutine(SpawnRate());
        wormSpawn = StartCoroutine(WormSpawnRate());
        criminalSpawn = StartCoroutine(CriminalSpawnRate());
    }
    private void StopSpawning() {
        StopCoroutine(spawn);
        StopCoroutine(wormSpawn);
    }
    private IEnumerator SpawnRate() {
        while (isActiveAndEnabled)
        {
            YieldInstruction wait = new WaitForSeconds(wave[Current].SpawnRate);
            yield return wait;
            SpawnEnemy(0);
        }
    }
    private IEnumerator WormSpawnRate() {
        while (isActiveAndEnabled) {
            YieldInstruction wait = new WaitForSeconds((wave[Current].SpawnRate*2));
            yield return wait;
            SpawnEnemy(1);
        }
    }
    private IEnumerator CriminalSpawnRate() {
        while (isActiveAndEnabled) {
            YieldInstruction wait = new WaitForSeconds((wave[Current].SpawnRate * 20));
            yield return wait;
            SpawnEnemy(2);
        }
    }
    private GameObject SetEnemyToSpawn(int index) {
        return wave[Current].Enemies[index];
    }
    private void SpawnEnemy(int enemy) {

        Instantiate(SetEnemyToSpawn(enemy), spawnPoint.transform.position, Quaternion.identity);

    }
   
}
