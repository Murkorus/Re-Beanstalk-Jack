using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    //is grounded
    public GameObject groundCheck;
    public bool isGrounded;
    public LayerMask groundLayer;


    //Movement
    public float Horizontal;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] float jumpHeight = 5;

    // Update is called once per frame
    void Update()
    {
        //Ground check
        if(Physics2D.OverlapBox(groundCheck.transform.position, groundCheck.transform.localScale, 0, groundLayer))
        {
            isGrounded = true;
        } else
        {
            isGrounded = false;
            }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);  
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * jumpHeight * .4f, ForceMode2D.Impulse);
        }
    }


    public void FixedUpdate()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (Horizontal != 0)
        {
            // Apply acceleration
            rb.velocity += new Vector2(Horizontal * acceleration * Time.deltaTime, 0);

            // Limit velocity to maximum speed
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
        }

        if (Horizontal == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * (1 - deacceleration * Time.deltaTime), rb.velocity.y);
        }
    }
}
