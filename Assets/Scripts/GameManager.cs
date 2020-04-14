using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private int health;
    private int currency;
    private int level;
    public static event UnityAction<int> onHealth;
    public static event UnityAction endGame;
    public static event UnityAction stopEnemies;
    public static event UnityAction stopSpawning;
    public static event UnityAction<int> patchLevel;
    public static event UnityAction<int> firewallLevel;
    public static event UnityAction<int> antivirusLevel;
    public static event UnityAction click;
    public int Health { get => health; set { health = Mathf.Clamp(value, 0, 300); HealthCheck(health); if (onHealth != null) { onHealth(health); } } }

    public int Currency { get => currency; set => currency = value; }
    public int EnemiesDestroyed { get => enemiesDestroyed; set => enemiesDestroyed = value; }

    private int enemiesDestroyed;
    public int[] upgradeLevel = new int[3]; //0 - firewall, 1 - patch, 2 - anti virus

    private static GameManager instance;
    public static GameManager GetGameManager() => instance;

    //PopUp Add
    public GameObject popUpAdd;
    private float timerPopUp;

    //Trojan Horse
    public GameObject trojanHorseAdd;
    private float timerTrojanHorse;
    private bool trojanLeft = false;
    private bool trojanRight = true;
    private bool notClicked = true;
    private float timeToMove;

    #region UI Text
    [SerializeField] private Text currencyText;
    [SerializeField] private Text firewallUpgradeText;
    [SerializeField] private Text patchUpgradeText;
    [SerializeField] private Text antivirusUpgradeText;

    //Level Text
    [SerializeField] private Text firewallLevelText;
    [SerializeField] private Text patchLevelText;
    [SerializeField] private Text antivirusLevelText;
    #endregion

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }

    void Start()
    {
        CyberCriminal.damage += GetDamage;
        Bugs.damage += GetDamage;
        PlayerInput.pause += PauseControl;
        UIManager.startGame += StartGame;
    }

    private void Update()
    {
        if (timerPopUp <= 0)
        {
            popUpAdd.SetActive(true);
        }

        else
        {
            timerPopUp -= Time.deltaTime;
        }

        if (timerTrojanHorse <= 0 && timeToMove >= 0)
        {
            trojanHorseAdd.SetActive(true);

            if (trojanRight)
            {
                trojanHorseAdd.transform.Translate(Vector3.right * Time.deltaTime * 300);
            }

            if (trojanLeft)
            {
                trojanHorseAdd.transform.Translate(Vector3.left * Time.deltaTime * 300);
            }

            timeToMove -= Time.deltaTime;
        }

        else if (timerTrojanHorse <= 0 && timeToMove <= 0)
        {
            if (notClicked)
            {
                currency += 300;
            }

            timerTrojanHorse = Random.Range(30.0f, 60.0f);
            timeToMove = 15f;
            notClicked = true;

            if (trojanRight)
            {
                trojanRight = false;
                trojanLeft = true;
            }

            else if (trojanLeft)
            {
                trojanRight = true;
                trojanLeft = false;
            }
        }

        else
        {
            timerTrojanHorse -= Time.deltaTime;
        }

        currencyText.text = "Currency: " + currency.ToString();

        switch (upgradeLevel[0])
        {
            case 0:
                firewallUpgradeText.text = "Firewall\n Upgrade\n Cost: 500";
                firewallLevelText.text = "Lv. 1";
                break;
            case 1:
                firewallUpgradeText.text = "Firewall\n Upgrade\n Cost: 800";
                firewallLevelText.text = "Lv. 2";
                break;
            case 2:
                firewallUpgradeText.text = "Firewall\n Upgrade\n Cost: 1000";
                firewallLevelText.text = "Lv. 3";
                break;
        }

        switch (upgradeLevel[1])
        {
            case 0:
                patchUpgradeText.text = "Patch\n Upgrade\n Cost: 300";
                patchLevelText.text = "Lv. 1";
                break;
            case 1:
                patchUpgradeText.text = "Patch\n Upgrade\n Cost: 500";
                patchLevelText.text = "Lv. 1";
                break;
            case 2:
                patchUpgradeText.text = "Patch\n Upgrade\n Cost: 700";
                patchLevelText.text = "Lv. 3";
                break;
        }

        switch (upgradeLevel[2])
        {
            case 0:
                antivirusUpgradeText.text = "Antivirus\n Upgrade\n Cost: 800";
                antivirusLevelText.text = "Lv. 1";
                break;
            case 1:
                antivirusUpgradeText.text = "Antivirus\n Upgrade\n Cost: 1000";
                antivirusLevelText.text = "Lv. 2";
                break;
            case 2:
                antivirusUpgradeText.text = "Antivirus\n Upgrade\n Cost: 1200";
                antivirusLevelText.text = "Lv. 3";
                break;
        }
    }

    private void GetDamage(int amount)
    {
        Health -= amount;
    }
    private void PauseControl(bool val) {
        if (val) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }
    private IEnumerator WaitToStopEnemies() {
        YieldInstruction wait = new WaitForSeconds(0.1f);
        yield return wait;
        if (stopEnemies != null) {
            stopEnemies();
        }
    }
    private void HealthCheck(int health) {
        if (health == 0) {
            if (stopSpawning != null) {
                stopSpawning();
            }
            SceneChange(0);
            SceneManager.UnloadSceneAsync(1);
            StartCoroutine(WaitToEnd());
        }
    }
    private IEnumerator WaitToEnd() {
        YieldInstruction wait = new WaitForSeconds(0.5f);
        yield return wait;
        if (endGame != null) {
            endGame();
        }

    }
    private void StartGame() {
        Health = 100;
        Currency = 500;
        timerPopUp = 40f;
        timerTrojanHorse = 50f;
        timeToMove = 20f;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        StartCoroutine(WaitToChange());

    }
    private IEnumerator WaitToChange() {
        YieldInstruction wait = new WaitForSeconds(1f);
        yield return wait;
        SceneChange(1);

    }
    private void SceneChange(int sceneIndex) {
        Scene scene = SceneManager.GetSceneByBuildIndex(sceneIndex);
        SceneManager.SetActiveScene(scene);
    }
    public void popUp()
    {
        popUpAdd.SetActive(false);

        if (currency <= 100)
        {
            currency = 0;
        }

        else
        {
            currency -= 100;
        }

        timerPopUp = Random.Range(30.0f, 60.0f);
    }

    public void closePopUp()
    {
        if (click != null) {
            click();
        }
        if (notClicked)
        {
            currency += 200;
        }

        popUpAdd.SetActive(false);
        timerPopUp = Random.Range(30.0f, 60.0f);
    }

    public void trojanHorse()
    {
        if (currency <= 200)
        {
            currency = 0;
        }

        else
        {
            currency -= 200;
        }

        notClicked = false;
    }

    public void UpgradeFirewall()
    {
        if (click != null) {
            click();
        }
        if (firewallLevel != null) {
            firewallLevel(upgradeLevel[0]);
        }
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
        if (click != null) {
            click();
        }
        if (patchLevel != null) {
            patchLevel(upgradeLevel[1]);
        }
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
        if (click != null) {
            click();
        }
        if (antivirusLevel != null) {
            antivirusLevel(upgradeLevel[2]);
        }
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
