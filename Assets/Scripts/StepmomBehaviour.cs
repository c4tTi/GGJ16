using UnityEngine;
using System;

public class StepmomBehaviour : MonoBehaviour
{
    private DateTime nextAction;

    void Start()
    {
        CalculateNextAction();
    }

    void FixedUpdate()
    {
        if (DateTime.Compare(nextAction, System.DateTime.Now) <= 0)
        {
            CalculateNextAction();
            DoAction();
        }
    }

    private void CalculateNextAction()
    {
        nextAction = System.DateTime.Now.AddSeconds(UnityEngine.Random.Range(1, 3));
    }

    private void DoAction()
    {
        GameObject weaponInstance = Instantiate(Resources.Load("Reis", typeof(GameObject))) as GameObject;

        StandardThrow script = weaponInstance.GetComponent<StandardThrow>();
        script.player = gameObject;
        script.directionRight = false;
    }
}
