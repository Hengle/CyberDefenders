using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private int health;
    private int currency;

    public static event UnityAction<int> onHealth;
    public int Health { get => health; set { health = Mathf.Clamp(value,0,300); if (onHealth != null) { onHealth(health); } } }

    public int Currency { get => currency; set => currency = value; }
    public int EnemiesDestroyed { get => enemiesDestroyed; set => enemiesDestroyed = value; }

    private int enemiesDestroyed;
    public int[] upgradeLevel = new int[3]; //0 - firewall, 1 - patch, 2 - anti virus

    private static GameManager instance;
    public static GameManager GetGameManager() => instance;

    [SerializeField] private Text currencyText;
    [SerializeField] private Text firewallUpgradeText;
    [SerializeField] private Text patchUpgradeText;
    [SerializeField] private Text antivirusUpgradeText;

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

    private void Update()
    {
        currencyText.text = "Currency: " + currency.ToString();

        switch (upgradeLevel[0])
        {
            case 0:
                firewallUpgradeText.text = "Firewall\n Upgrade\n Cost: 500";
                break;
            case 1:
                firewallUpgradeText.text = "Firewall\n Upgrade\n Cost: 800";
                break;
            case 2:
                firewallUpgradeText.text = "Firewall\n Upgrade\n Cost: 1000";
                break;
        }

        switch (upgradeLevel[1])
        {
            case 0:
                patchUpgradeText.text = "Patch\n Upgrade\n Cost: 300";
                break;
            case 1:
                patchUpgradeText.text = "Patch\n Upgrade\n Cost: 500";
                break;
            case 2:
                patchUpgradeText.text = "Patch\n Upgrade\n Cost: 700";
                break;
        }

        switch (upgradeLevel[2])
        {
            case 0:
                antivirusUpgradeText.text = "Antivirus\n Upgrade\n Cost: 800";
                break;
            case 1:
                antivirusUpgradeText.text = "Antivirus\n Upgrade\n Cost: 1000";
                break;
            case 2:
                antivirusUpgradeText.text = "Antivirus\n Upgrade\n Cost: 1200";
                break;
        }
    }

    private void GetDamage(int amount)
    {        
        Health -= amount;
    }

    public void UpgradeFirewall()
    {
        if (upgradeLevel[0] < 1 && currency >= 500)
        {
            upgradeLevel[0]++;
            currency -= 400;
        }

        else if (upgradeLevel[0] < 2 && currency >= 800)
        {
            upgradeLevel[0]++;
            currency -= 700;
        }

        else if (upgradeLevel[0] < 3 && currency >= 1000)
        {
            upgradeLevel[0]++;
            currency -= 900;
        }
    }

    public void UpgradePatch()
    {
        if (upgradeLevel[1] < 1 && currency >= 300)
        {
            upgradeLevel[1]++;
            currency -= 200;
        }

        else if (upgradeLevel[1] < 2 && currency >= 500)
        {
            upgradeLevel[1]++;
            currency -= 400;
        }

        else if (upgradeLevel[1] < 3 && currency >= 700)
        {
            upgradeLevel[1]++;
            currency -= 600;
        }
    }

    public void UpgradeAntiVirus()
    {
        if (upgradeLevel[2] < 1 && Currency >= 800)
        {
            upgradeLevel[2]++;
            currency -= 700;
        }

        else if (upgradeLevel[2] < 2 && Currency >= 1000)
        {
            upgradeLevel[2]++;
            currency -= 900;
        }

        else if (upgradeLevel[2] < 3 && Currency >= 1200)
        {
            upgradeLevel[2]++;
            currency -= 1100;
        }
    }
}
