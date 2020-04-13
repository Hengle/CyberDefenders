using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Botnet : MonoBehaviour
{
    public static event UnityAction<Bugs> speedUp;
    private bool dead;
    [SerializeField]private GameObject spawner;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        GameManager.stopSpawning += KillTheBot;
        UIManager.startGame += StartGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartGame() {
        dead = false;
    }
    private void KillTheBot() {
        dead = true;
        //gameObject.SetActive(false);
       // StartCoroutine(WaitToKill());
    }
    private IEnumerator WaitToKill() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Malware")&&!dead) {
            Debug.Log("Malware detected");
            if (speedUp != null) {
                speedUp(other.GetComponent<Bugs>());
            }
        }
        if (other.CompareTag("Patch"))
        {
            gm.Currency += 100;
            Instantiate(spawner,transform.position,Quaternion.identity);
            dead = true;
            Destroy(gameObject,0.5f);
        }

        if (other.CompareTag("Scan") && gm.upgradeLevel[2] == 1)
        {
            if (gm.upgradeLevel[2] == 2)
            {
                gm.Currency += 100;
            }

            gm.Currency += 50;
            Instantiate(spawner, transform.position, Quaternion.identity);
            dead = true;
            Destroy(gameObject, 0.5f);
        }
    }
    
}
