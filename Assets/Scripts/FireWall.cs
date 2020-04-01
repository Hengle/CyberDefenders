using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public int defense;
    private GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        switch (gm.upgradeLevel[0])
        {
            case 0:
                defense = 300;
                break;
            case 1:
                defense = 400;
                break;
            case 2:
                defense = 500;
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (defense <= 0)
        {
            Destroy(gameObject);
        }
    }    
}
