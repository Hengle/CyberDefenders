using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVirus : MonoBehaviour
{
    private GameManager gm;

    private float timerScan;
    private bool antivirusLeft = true;
    private bool antivirusRight = false;
    private float timeToMove = 10f;
    private bool start = false;


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {

        if (start && timeToMove > 0)
        {
            if (antivirusRight)
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * 50);
            }

            else if (antivirusLeft)
            {
                this.transform.Translate(Vector3.left * Time.deltaTime * 50);
            }

            timeToMove -= Time.deltaTime;
        }

        else
        {
            timeToMove = 2f;

            if (antivirusRight && start)
            {
                antivirusLeft = true;
                antivirusRight = false;
            }

            else if (antivirusLeft && start)
            {
                antivirusLeft = false;
                antivirusRight = true;
            }

            start = false;
        }
    }

    public void Scan()
    {
        if (gm.Currency >= 1000 && !start)
        {
            gm.Currency -= 1000;
            start = true;
        }

        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
