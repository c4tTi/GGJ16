using UnityEngine;
using System.Collections;

public class PaparazziBehaviourScript : MonoBehaviour
{
    public int posingTime = 20;
    public int posingTimeOut = 20;
    public GameObject sprites;

    private int curPosingTime = 0;
    private bool isPosing = false;
    private Animator ownAnimator;
    private PlayerBehaviourScript player;

    // Use this for initialization
    void Start ()
    {
        ownAnimator = sprites.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        curPosingTime++;

        if (isPosing)
	    {
	        if (curPosingTime >= posingTime)
	        {
	            curPosingTime = 0;
	            isPosing = false;
                player.EndPosing();
                ownAnimator.SetBool("IsTakingFoto", false);
            }
	    }
	}

    public void OnTriggerEnter(Collider other)
    {
        if (curPosingTime >= posingTimeOut &&  other.CompareTag("Player"))
        {
            isPosing = true;
            curPosingTime = 0;
            player = other.GetComponent<PlayerBehaviourScript>();
            player.StartPosing();
            ownAnimator.SetBool("IsTakingFoto", true);
        }
    }
}
