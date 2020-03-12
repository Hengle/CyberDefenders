using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    private int health;    

    public static event UnityAction<int> onHealth;
    public int currency;
    private float time;

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

    void Start()
    {
        Health = 100;
        // currency = 500; //First Method
        currency = 0;
        time = 3f;///Second Method
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        else
        {
            currency += 50;
            time = 3f;
        }
    }

    private void GetDamage(int amount) {
        Debug.Log("Ouch");
        Health -= amount;
    }
}
