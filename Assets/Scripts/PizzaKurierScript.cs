using UnityEngine;
using System.Collections;

public class PizzaKurierScript : MonoBehaviour
{
    private Rigidbody rb;
    private float kurierSpeed = 10;


	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * -1 * kurierSpeed, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
