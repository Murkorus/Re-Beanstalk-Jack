using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;
    [SerializeField] private float turnMultiplier;

    public float coyoteTimer;
    public float coyoteTime;
    public bool hasJumped;

    public float maxJumpTime = 0.5f; // Maximum time that the player can hold down the jump button
    public float jumpHeight = 5f; // Height that the player jumps when pressing the jump button

    private float jumpTime; // Time that the player has been holding down the jump button

    [Header("Booleans")]
    public bool isGrounded;




    [Space(25)]

    //Input
    public float Horizontal;
    public float Vertical;

    //If grounded bool

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        if(!isGrounded && !hasJumped)
        {
            coyoteTimer += 1 * Time.deltaTime;
        }


        //Acceleration
        if (Mathf.Sign(rb.velocity.x) == Mathf.Sign(Horizontal)) // If moving in same direction
        {
            rb.velocity = new Vector2(rb.velocity.x + Horizontal * speed * (Time.deltaTime * acceleration), rb.velocity.y);
        }
        else // If moving in opposite direction
        {
            rb.velocity = new Vector2(rb.velocity.x + Horizontal * speed * (Time.deltaTime * acceleration * turnMultiplier), rb.velocity.y);
        }

        //Deacceleration
        if (Horizontal == 0)
            rb.velocity = new Vector2(rb.velocity.x * deacceleration, rb.velocity.y);


        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -5, 5), rb.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded || coyoteTimer <= coyoteTime && !hasJumped)
        {
            jumpTime = 0f;
            hasJumped = true;

            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }
        


    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        collision.GetContacts(contacts);
        for (int i = 0; i < contacts.Length; i++)
        {
            if (contacts[i].normal.y > .75)
            {
                isGrounded = true;
                coyoteTimer = 0;
                jumpTime = 0f;
                hasJumped = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
