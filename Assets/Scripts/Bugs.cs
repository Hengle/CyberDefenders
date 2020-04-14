using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
[RequireComponent(typeof(NavMeshAgent))]
public class Bugs : MonoBehaviour
{
    private NavMeshAgent nav;
    [SerializeField]private GameObject target;
    [SerializeField] private GameObject boom;
    [SerializeField] private int attackPower;
    [SerializeField] private bool worm;
    private GameManager gm;
    private FireWall fireWallUnit;
    private Patch patchUnit;
    private WaveManager wm;
    private bool dead;

    public static event UnityAction<int> damage;
    private void Awake() {
        nav = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.GetGameManager();
        wm = WaveManager.GetWaveManager();
        Botnet.speedUp += SpeedUp;
        GameManager.stopEnemies+=Stop;
        Hardrive.stopEnemies +=Dead;
        StartCoroutine(WaitToStart());
    }

    private IEnumerator WaitToStart() {
        YieldInstruction wait = new WaitForSeconds(1f);
        yield return wait;
        if (!dead&&!Hardrive.GetHardrive().Dead) {
            nav.SetDestination(target.transform.position);
        }
        Debug.Log("destination set");
    }
    private IEnumerator RecalculatePath() {
        YieldInstruction wait = new WaitForSeconds(4f);
        while (isActiveAndEnabled) { 
        yield return wait;
        nav.SetDestination(target.transform.position);
        }
    }
    private void SpeedUp(Bugs bug) {
        if (!dead) {
            if (bug == this) {nav.speed = 10;
        ; }

        }
        
        
    }
    private void Dead() {

        dead = true;
    }
    private void Stop() {
       StartCoroutine(WaitToStop());
    }
    private IEnumerator WaitToStop() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        yield return wait;
        nav.SetDestination(transform.position);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Hardrive"))
        {
            if (boom != null)
            {
                Instantiate(boom, transform.position, Quaternion.identity);
            }

            if (damage != null)
            {
                damage(attackPower);
            }

            Destroy(gameObject);
        }

        else if (other.CompareTag("FireWall")&&!worm)
        {
            gm.EnemiesDestroyed += 1;
            gm.Currency += 50;
            Destroy(gameObject);
            fireWallUnit = other.GetComponent<FireWall>();
            fireWallUnit.Defense -= 100;
        }

        else if (other.CompareTag("Patch"))
        {
            gm.EnemiesDestroyed += 1;
            gm.Currency += 50;
            Destroy(gameObject);
            patchUnit = other.GetComponent<Patch>();
            patchUnit.Defense -= 1;
        }

        if (other.CompareTag("Scan"))
        {
            if (gm.upgradeLevel[2] == 2)
            {
                gm.Currency += 150;
            }

            else if (gm.upgradeLevel[2] == 1)
            {
                gm.Currency += 100;
            }

            else
            {
                gm.Currency += 50;
            }

            Destroy(gameObject);
        }
    }
}
