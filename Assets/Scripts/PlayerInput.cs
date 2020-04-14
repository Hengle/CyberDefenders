using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour {
    [SerializeField] private GameObject unit;
    [SerializeField] private Camera cam;
    private bool paused;
    private bool canSpawn;
    private GameManager gm;

    public bool Paused {
        get => paused; set {
            paused = value; if (pause != null) {
                pause(paused);
            }
        }
    }

    public static event UnityAction<bool> pause;
    public static event UnityAction unitPlaced;
    private void Awake() {
        DefenseUnit.unitSelected += SetUnit;
    }
    private void Start() {
        gm = GameManager.GetGameManager(); ;

    }

    private void Update() {

        PauseControls();
        if (!Paused) {
            GetInput();
        }
    }
    private void GetInput() {
        if (Input.GetMouseButtonDown(0)) {
            
            PlaceUnit();
        }

    }
    private void PauseControls() {
        if (Input.GetButtonDown("Pause")) {
            if (Paused) {
                Paused = false;
            }
            else {
                Paused = true;
            }
        }

    }
    private void SetUnit(GameObject selectedUnit) {
        unit = selectedUnit;
    }
    private void PlaceUnit() {
        if (unit != null && gm.Currency >= 100) {
             //"currency"

            Vector3 position = GetMousePosition();
            if (canSpawn) {
                Instantiate(unit, position, Quaternion.identity);
                if (unitPlaced != null) {
                    unitPlaced();
                }
                gm.Currency -= 100;
            }
        }
        else {
            Debug.Log("Nothing Selected");
        }
    }
    private Vector3 GetMousePosition() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            
            if (hit.collider.CompareTag("Path")||hit.collider.CompareTag("Malware")) {
                canSpawn = true;
            }
            else {
                canSpawn = false;
            }
            return hit.point;
        }
        return new Vector3(0, 0, 0);
    }
}
