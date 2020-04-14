using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
public class CyberCriminal : MonoBehaviour {
    [SerializeField] private GameObject viruses;
    [SerializeField] private int attackPower;
    [SerializeField] private GameObject target;

    private int health;
    private bool dead;

    private FireWall fireWallUnit;
    private Patch patchUnit;
    private GameManager gm;
    private NavMeshAgent nav;

    public static event UnityAction<int> damage;
    public int Health { get => health; set { health = value;Debug.Log(health); if (health <= 0) { Destroy(gameObject); } } }

    private void Awake() {
        Health = 10;
        nav = GetComponent<NavMeshAgent>();
        gm = GameManager.GetGameManager();
    }
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnViruses());
        StartCoroutine(WaitToStart());
        Hardrive.stopEnemies += Dead;
        GameManager.stopEnemies += Stop;
        StartCoroutine(WaitToStart());
    }

    // Update is called once per frame
    void Update() {

    }
    
    private void Dead() {

        dead = true;
    }
    private void Stop() {
        StartCoroutine(WaitToStop());
    }

    private IEnumerator WaitToStart() {
        YieldInstruction wait = new WaitForSeconds(1f);
        yield return wait;
        if (!dead && !Hardrive.GetHardrive().Dead) {
            nav.SetDestination(target.transform.position);
        }
        Debug.Log("destination set");
    }
    private IEnumerator SpawnViruses() {
        YieldInstruction wait = new WaitForSeconds(3);
        while (isActiveAndEnabled) { 
        yield return wait;
        Instantiate(viruses, transform.position, Quaternion.identity);
        }
    }
    
    private IEnumerator WaitToStop() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        yield return wait;
        nav.SetDestination(transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Hardrive")) {
            if (damage != null) {
                damage(attackPower);
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("FireWall")) {
            fireWallUnit = other.GetComponent<FireWall>();
            fireWallUnit.Defense -= 100;
            nav.speed = 0;
        }
        else {
            nav.speed = 1;
        }

        if (other.CompareTag("Patch")) {
            switch (gm.upgradeLevel[1]) {
                case 1:
                    Health -= 2;
                    break;
                case 2:
                    Health -= 3;
                    break;
                case 3:
                    Health -= 4;
                    break;
            }
            patchUnit = other.GetComponent<Patch>();
            patchUnit.Defense -= 1;
        }

        if (other.CompareTag("Scan")) {
            switch (gm.upgradeLevel[2]) {
                case 1:
                    Health -= 2;
                    break;
                case 2:
                    Health -= 3;
                    break;
                case 3:
                    Health -= 4;
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("FireWall") && !dead) {
            nav.speed = 1;
        }
        }
}