using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    private int health;

    

    public static event UnityAction<int> onHealth;

    public int Health { get => health; set { health = value; if (onHealth != null) { onHealth(health); } } }

    private static GameManager instance;
    public static GameManager GetGameManager() => instance;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        Bugs.damage += GetDamage;
    }
    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GetDamage(int amount) {
        Debug.Log("Ouch");
        Health -= amount;
    }
}
