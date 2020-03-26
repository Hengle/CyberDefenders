using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class Bugs : MonoBehaviour
{
    private NavMeshAgent nav;
    [SerializeField]private GameObject target;
    [SerializeField] private GameObject boom;
    [SerializeField] private int attackPower;

    private GameManager gm;
    private FireWall fireWallUnit;
    private WaveManager wm;

    public static event UnityAction<int> damage;
    private void Awake() {
        nav = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        wm = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        StartCoroutine(WaitToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator WaitToStart() {
        YieldInstruction wait = new WaitForSeconds(1f);
        yield return wait;
        nav.SetDestination(target.transform.position);
        Debug.Log("destination set");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Hardrive"))
        {
            Debug.Log("Hardrive hit");
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

        else if (other.CompareTag("FireWall"))
        {
            gm.EnemiesDestroyed += 1;
            gm.Currency += 100;
            Destroy(gameObject);
            fireWallUnit = other.GetComponent<FireWall>();
            fireWallUnit.defense -= 100;
        }
    }
}
