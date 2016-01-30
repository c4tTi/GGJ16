using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerBehaviourScript : MonoBehaviour
{
    public IngameMenuScript menu;
    public float horizontalSpeed = 10;
    public float jumpForce = 10;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    private bool facingRight = true;
    private bool grounded = true;
    private float groundRadius = 0.2f;


    private Rigidbody rigidBody;
    private Animator animator;

    public GameObject sprites;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = sprites.GetComponent<Animator>();

        menu.OnNoTime += MenuOnNoTime;
        menu.OnToDirty += MenuOnToDirty;
    }

    private void MenuOnToDirty()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void MenuOnNoTime()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Shit"))
        {
            // TODO
            menu.AddDirt(10);
        }
    }

    void FixedUpdate ()
	{
	    grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
        float move = Input.GetAxis("Horizontal");

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(move));

        rigidBody.velocity = new Vector3(move * horizontalSpeed, rigidBody.velocity.y, rigidBody.velocity.z);

	    if ((move > 0 && !facingRight)
            || (move < 0 && facingRight))
	    {
	        Flip();
	    }

	    if (grounded && (Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0))
	    {
	        OnJump();
	    }
	    if (Input.GetButtonDown("Fire1"))
	    {
	        OnFire();
	    }
	    if (Input.GetButtonDown("Fire2"))
	    {
	        OnChangeWeapon();
	    }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = sprites.transform.localScale;
        scale.x *= -1;
        sprites.transform.localScale = scale;
    }

    private void OnJump()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0);
        rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnChangeWeapon()
    {
        menu.ToggleWeapon();
    }

    private void OnFire()
    {
        // TODO
        animator.SetTrigger("Fire");
        menu.FireWeapon();
    }
}
