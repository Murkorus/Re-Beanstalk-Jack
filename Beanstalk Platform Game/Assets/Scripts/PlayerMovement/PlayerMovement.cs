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

    [Space(25)]

    //Input
    private float Horizontal;
    private float Vertical;

    //If grounded bool
    private bool isGrounded;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        //Acceleration
        rb.velocity = new Vector2(rb.velocity.x + Horizontal * speed * (Time.deltaTime * acceleration), rb.velocity.y);

        //Deacceleration
        rb.velocity = new Vector2(rb.velocity.x * deacceleration, rb.velocity.y);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -5, 5), rb.velocity.y);
    }

    public void FixedUpdate()
    {
        
    }


    public void Jump()
    {
        rb.velocity = rb.velocity += new Vector2(0, jumpForce);
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
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
