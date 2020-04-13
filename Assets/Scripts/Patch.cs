using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{
    private int defense;
    
    public int Defense { get => defense; set { defense = value;if (defense == 0) { Destroy(gameObject); } } }
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetGameManager();
        switch (gm.upgradeLevel[0]) {
            case 0:
                Defense = 3;
                break;
            case 1:
                Defense = 4;
                break;
            case 2:
                Defense = 5;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Malware")) {
            Defense--;
            
        }
    }
    
}
