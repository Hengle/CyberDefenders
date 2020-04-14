using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    private int defense;
    private GameManager gm;

    public int Defense { get => defense; set => defense = value; }

    void Start()
    {
        gm = GameManager.GetGameManager();

        switch (gm.upgradeLevel[0])
        {
            case 0:
                Defense = 300;
                break;
            case 1:
                Defense = 400;
                break;
            case 2:
                Defense = 500;
                break;
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (Defense <= 0)
        {
            Destroy(gameObject);
        }
    }    
}
