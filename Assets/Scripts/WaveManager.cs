using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Wave[] wave;
    [SerializeField] private GameObject spawnPoint;
    private int current;

    private int waveNumber;
    public int Current { get => current; set { current = value; SetEnemyToSpawn(); } }

    private GameManager gm;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        StartCoroutine(SpawnRate());
        current = 0;
        waveNumber = 0;
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
