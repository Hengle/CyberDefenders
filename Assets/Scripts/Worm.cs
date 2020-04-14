using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Patch")) {
            gm.Currency += 100;
           
            
            Destroy(gameObject, 0.5f);
        }

        if (other.CompareTag("Scan") && gm.upgradeLevel[2] == 1) {
            if (gm.upgradeLevel[2] == 2) {
                gm.Currency += 100;
            }

            gm.Currency += 50;
            
            
            Destroy(gameObject, 0.5f);
        }
    }
}
