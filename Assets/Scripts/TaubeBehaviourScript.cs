using UnityEngine;
using System.Collections;

public class TaubeBehaviourScript : MonoBehaviour
{
    public GameObject birdShit;
    public int spawnRate = 100;
    public int patrolTurnRate = 100;
    public float patrolSpeed = 4;

    private int count = 0;
    private Rigidbody rb;


	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(patrolSpeed, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    count++;
	    if (count % spawnRate == 0)
	    {
	        SpawnBirdShit();
	    }
	    if (count % patrolTurnRate == 0)
	    {
	        rb.velocity = -rb.velocity;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
	}

    private void SpawnBirdShit()
    {
        Instantiate(birdShit, transform.position, transform.rotation);
    }
}