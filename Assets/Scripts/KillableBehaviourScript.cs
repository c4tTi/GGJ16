using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Lifetime;

public class KillableBehaviourScript : MonoBehaviour
{
    public int life = 1;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            life--;
            Destroy(other.gameObject);

            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
