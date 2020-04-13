using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotNetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject botnet;
    private bool spawnable;
    private int spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        RandomizeSpawn();
        GameManager.stopEnemies += StopSpawning;
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void RandomizeSpawn() {
        spawnRate =Random.Range(4,15);
    }
    private void GameStart() {
        spawnable = true;
        StartCoroutine(WaitToSpawn());
    }
    private IEnumerator WaitToSpawn() {
        YieldInstruction wait = new WaitForSeconds(spawnRate);
        yield return wait;
        if (spawnable) {
            Instantiate(botnet, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    private void StopSpawning() {
        spawnable = false;
    }

}
