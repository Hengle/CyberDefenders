using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class DefenseUnit : MonoBehaviour
{
    [SerializeField] private GameObject unit;

    public static event UnityAction<GameObject> unitSelected;
    // Start is called before the first frame update
    private void Awake() {
        GetComponent<Button>().onClick.AddListener(IconClick);
    }

    private void IconClick() {
        if (unitSelected != null) {
            unitSelected(unit);
        }
    }
}
