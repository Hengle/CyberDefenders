using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameObject unit;
    [SerializeField] private Camera cam;

    private GameManager gm;

    private void Awake() {
        DefenseUnit.unitSelected += SetUnit;
    }
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Click" +Input.mousePosition);
            PlaceUnit();
        }
    }
    private void SetUnit(GameObject selectedUnit) {
        unit = selectedUnit;
    }
    private void PlaceUnit() {
        if (unit != null && gm.Currency >= 100) {
            gm.Currency -= 100; //"currency"

            Vector3 position = GetMousePosition();
            //position.z = 10;
            //position =Camera.main.ScreenToWorldPoint(position);
            
            Instantiate(unit,position , Quaternion.identity);
        }
        else {
            Debug.Log("Nothing Selected");
        }
    }
    private Vector3 GetMousePosition() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Debug.DrawLine(transform.position, hit.point);
            return hit.point;
        }
        return new Vector3(0,0,0);
    }
}
