using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Hardrive : MonoBehaviour
{
    private bool dead;
    public static event UnityAction stopEnemies;
    private static Hardrive instance;

    public bool Dead { get => dead; set => dead = value; }

    public static Hardrive GetHardrive() => instance;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.stopEnemies += Defeated;
        UIManager.startGame += StartGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartGame() {
        Dead = false;
    }
    private void Defeated() {
        Dead = true;
        if(stopEnemies!=null)
            stopEnemies();
        StartCoroutine(WaitToKill());
    }
    private IEnumerator WaitToKill() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        Destroy(gameObject);
    }
}
