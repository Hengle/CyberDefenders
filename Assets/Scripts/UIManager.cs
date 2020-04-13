using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Text health;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject deadScreen;
    [SerializeField] private Text fireWallLevel;
    [SerializeField] private Text patchLevel;
    [SerializeField] private Text antivirusLevel;
    
    public static event UnityAction startGame;
    public static event UnityAction click;
    private void Awake() {
        titleScreen.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        GameManager.onHealth += OnHealthchange;
        PlayerInput.pause += PauseScreenControl;
        GameManager.endGame += DeadScreenControl;
        GameManager.patchLevel += OnPatchLevelUp;
        GameManager.firewallLevel += OnFirewallLevelUp;
        GameManager.antivirusLevel += OnAntivirusLevelUp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnHealthchange(int amount) {
        health.text = "Health: " + amount.ToString();
    }
    private void OnPatchLevelUp(int level) {
        patchLevel.text = "Lv: " + level.ToString();
    }
    private void OnFirewallLevelUp(int level) {
        fireWallLevel.text = "Lv: " + level.ToString();
    }
    private void OnAntivirusLevelUp(int level) {
        antivirusLevel.text = "Lv: " + level.ToString();
    }
    private void PauseScreenControl(bool val) {
        Debug.Log("pause screen up");
        
        pauseScreen.SetActive(val);
    }
    private void DeadScreenControl() {
        deadScreen.SetActive(true);
        StartCoroutine(WaitToReset());
    }
    private IEnumerator WaitToReset() {
        YieldInstruction wait = new WaitForSeconds(5);
        yield return wait;
        deadScreen.SetActive(false);
        titleScreen.SetActive(true);
    }
    public void StartGame() {
        if (click != null) {
            click();
        }
        titleScreen.SetActive(false);
        if (startGame != null) {
            startGame();
        }
    }
}
