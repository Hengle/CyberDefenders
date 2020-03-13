using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public int defense;

    void Start()
    {
        defense = 300;
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            defense -= 100;
        }

        if (defense <= 0)
        {
            Destroy(gameObject);
        }
    }    
}
