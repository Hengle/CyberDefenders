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
        currency = 300; //First Method
    }

    private void GetDamage(int amount) {
        Debug.Log("Ouch");
        Health -= amount;
    }

    public void UpgradeFirewall()
    {
        if (upgradeLevel[0] < 1 && currency == 500)
        {
            upgradeLevel[0]++;
        }

        else if (upgradeLevel[0] < 2 && currency == 800)
        {
            upgradeLevel[0]++;
        }

        else if (upgradeLevel[0] < 3 && currency == 1000)
        {
            upgradeLevel[0]++;
        }
    }

    public void UpgradePatch()
    {
        if (upgradeLevel[1] < 1 && currency == 500)
        {
            upgradeLevel[1]++;
        }

        else if (upgradeLevel[1] < 2 && currency == 800)
        {
            upgradeLevel[1]++;
        }

        else if (upgradeLevel[1] < 3 && currency == 1000)
        {
            upgradeLevel[1]++;
        }
    }

    public void UpgradeAntiVirus()
    {
        if (upgradeLevel[2] < 1 && currency == 500)
        {
            upgradeLevel[2]++;
        }

        else if (upgradeLevel[2] < 2 && currency == 800)
        {
            upgradeLevel[2]++;
        }

        else if (upgradeLevel[2] < 3 && currency == 1000)
        {
            upgradeLevel[2]++;
        }
    }
}
