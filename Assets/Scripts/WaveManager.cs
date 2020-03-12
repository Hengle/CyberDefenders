using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    [SerializeField] private Wave[] wave;
    [SerializeField] private GameObject spawnPoint;
    private int current;

    public int Current { get => current; set { current = value; SetEnemyToSpawn(); } }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnRate());
        current = 0;
    }

    // Update is called once per frame
    void Update() {

    }
    private IEnumerator SpawnRate() {
        while (isActiveAndEnabled) {
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
