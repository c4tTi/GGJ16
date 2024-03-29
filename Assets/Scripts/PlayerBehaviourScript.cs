﻿using System;
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

    public Canvas gameOver;

    private bool isPosing = false;
    private bool facingRight = true;
    private bool grounded = true;
    private float groundRadius = 0.3f;
    private bool isGameOver = false;
    private int gameOverCount = 0;


    private Rigidbody rigidBody;
    private Animator animator;
    private Collider[] colliders;

    public GameObject sprites;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = sprites.GetComponent<Animator>();
        colliders = GetComponents<Collider>();

        menu.OnNoTime += MenuOnNoTime;
        menu.OnToDirty += MenuOnToDirty;

        gameOver.enabled = false;
    }

    private void MenuOnToDirty()
    {
        animator.SetTrigger("GameOver");
        isGameOver = true;
        gameOver.enabled = true;
        rigidBody.velocity = new Vector3();
    }

    private void MenuOnNoTime()
    {
        animator.SetTrigger("GameOver");
        isGameOver = true;
        gameOver.enabled = true;
        rigidBody.velocity = new Vector3();
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
        if (isGameOver)
        {
            gameOverCount++;

            if (gameOverCount >= 60 * 2)
            {
                SceneManager.LoadScene("RunningBrideStartMenu");
            }

            return;
        }

	    grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
        float move = Input.GetAxis("Horizontal");

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(move));

        if (isPosing)
        {
            return;
        }

        if (rigidBody.position.y > 4800) { 
        menu.seconds = 300;
        }
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

        bool nearGrounded = rigidBody.velocity.y < 0.01 &&
                            Physics.CheckSphere(groundCheck.position + groundRadius * Vector3.down, groundRadius,
                                whatIsGround);

        if (!grounded && !nearGrounded)
        {
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }
        else if (nearGrounded)
        {
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
        }

        rigidBody.rotation = Quaternion.identity;
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
        Weapon weapon = menu.getActiveWeapon();
        GameObject weaponInstance = Instantiate(Resources.Load(weapon.text, typeof(GameObject))) as GameObject;
        if (weapon.text == "Brautstrauss")
        {
            BrautstraussMovement script = weaponInstance.GetComponent<BrautstraussMovement>();
            script.player = gameObject;
            script.directionRight = facingRight;
        }
        else if (weapon.text == "Feuerwerk")
        {
            FeuerwerkMovement script = weaponInstance.GetComponent<FeuerwerkMovement>();
            script.player = gameObject;
            script.directionRight = facingRight;
        }
        else
        {
            StandardThrow script = weaponInstance.GetComponent<StandardThrow>();
            script.player = gameObject;
            script.directionRight = facingRight;
        }

        menu.FireWeapon();
    }

    public void StartPosing()
    {
        rigidBody.velocity = new Vector3();
        isPosing = true;
        animator.SetBool("Posing", isPosing);
    }

    public void EndPosing()
    {
        isPosing = false;
        animator.SetBool("Posing", isPosing);
    }
}
