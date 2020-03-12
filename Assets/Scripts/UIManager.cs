using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Text health;
    private void Awake() {
        GameManager.onHealth += OnHealthchange;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnHealthchange(int amount) {
        health.text = "Health: "+amount.ToString();

    }
}
