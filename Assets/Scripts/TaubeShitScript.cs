using UnityEngine;
using System.Collections;

public class TaubeShitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag.Equals("Ground"))
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag.Equals("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
