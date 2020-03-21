using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
public class GameManager : MonoBehaviour
{
    private int health;    

    public static event UnityAction<int> onHealth;
    public int currency;
    public int Health { get => health; set { health = value; if (onHealth != null) { onHealth(health); } } }
    public int enemiesDestroyed;

    private static GameManager instance;
    public static GameManager GetGameManager() => instance;

    public VideoPlayer[] videos = new VideoPlayer[8];

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        Bugs.damage += GetDamage;
    }

    private void Update()
    {
        if (enemiesDestroyed == 10)
        {
            videos[0].Play();
        }
    }

    void Start()
    {
        Health = 100;
        currency = 500; //First Method
    }

    private void GetDamage(int amount) {
        Debug.Log("Ouch");
        Health -= amount;
    }
}
