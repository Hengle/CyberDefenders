using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameObject unit;
    [SerializeField] private Camera cam;
    // Start is called before the first frame update
    private void Awake() {
        DefenseUnit.unitSelected += SetUnit;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
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
        if (unit != null) {
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
