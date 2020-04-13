using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] wave;
    [SerializeField] private GameObject spawnPoint;
    private int current;
    private Coroutine spawn;
    private int waveNumber;
    public int Current { get => current; set { current = value; SetEnemyToSpawn(); } }

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
    }
    private void StopSpawning() {
        StopCoroutine(spawn);
    }
    private IEnumerator SpawnRate() {
        while (isActiveAndEnabled)
        {
            YieldInstruction wait = new WaitForSeconds(wave[Current].SpawnRate);
            yield return wait;
            SpawnEnemy();
        }
    }

    private GameObject SetEnemyToSpawn() {
        return wave[Current].Enemies[0];
    }
    private void SpawnEnemy() {
        Instantiate(SetEnemyToSpawn(), spawnPoint.transform.position, Quaternion.identity);
    }
}
