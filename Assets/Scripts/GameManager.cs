using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
public class GameManager : MonoBehaviour
{
    private int health;    

    public static event UnityAction<int> onHealth;
    private int currency;
    public int Health { get => health; set { health = Mathf.Clamp(value,0,300); if (onHealth != null) { onHealth(health); } } }

    public int Currency { get => currency; set => currency = value; }
    public int EnemiesDestroyed { get => enemiesDestroyed; set => enemiesDestroyed = value; }

    private int enemiesDestroyed;
    private int[] upgradeLevel = new int[3]; //first - firewall, second - patch, third - anti virus

    private static GameManager instance;
    public static GameManager GetGameManager() => instance;

    private void Awake() {
        if (instance != null && instance != this){
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
        Currency = 300; //First Method
    }

    private void GetDamage(int amount) {
        
        Health -= amount;
    }

    public void UpgradeFirewall()
    {
        if (upgradeLevel[0] < 1 && Currency == 500)
        {
            upgradeLevel[0]++;
        }

        else if (upgradeLevel[0] < 2 && Currency == 800)
        {
            upgradeLevel[0]++;
        }

        else if (upgradeLevel[0] < 3 && Currency == 1000)
        {
            upgradeLevel[0]++;
        }
    }

    public void UpgradePatch()
    {
        if (upgradeLevel[1] < 1 && Currency == 500)
        {
            upgradeLevel[1]++;
        }

        else if (upgradeLevel[1] < 2 && Currency == 800)
        {
            upgradeLevel[1]++;
        }

        else if (upgradeLevel[1] < 3 && Currency == 1000)
        {
            upgradeLevel[1]++;
        }
    }

    public void UpgradeAntiVirus()
    {
        if (upgradeLevel[2] < 1 && Currency == 500)
        {
            upgradeLevel[2]++;
        }

        else if (upgradeLevel[2] < 2 && Currency == 800)
        {
            upgradeLevel[2]++;
        }

        else if (upgradeLevel[2] < 3 && Currency == 1000)
        {
            upgradeLevel[2]++;
        }
    }
}
