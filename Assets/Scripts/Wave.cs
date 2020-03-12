using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    [SerializeField] private GameObject[] enemies;
    [SerializeField]private float spawnRate;
    public GameObject[] Enemies { get => enemies; set => enemies = value; }
    public float SpawnRate { get => spawnRate; set => spawnRate = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
