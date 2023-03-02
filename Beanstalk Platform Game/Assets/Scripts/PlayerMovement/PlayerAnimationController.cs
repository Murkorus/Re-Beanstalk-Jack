using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator playerAnimator;
    public PlayerMovement movementScript;

    public bool isGrounded;

    public bool isMoving;

    public bool isJumping;

    public bool isFalling;

    public bool usingSlingshot;

    public bool gooseOnBack;

    public bool isFacingRight;

    // Update is called once per frame
    void Update()
    {
        //check if grounded
        isGrounded = movementScript.isGrounded;
        
        //check if moving
        if(movementScript.Horizontal > 0.01 || movementScript.Horizontal < -0.01)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }

        isJumping = movementScript.hasJumped;

        //Is falling or jumping;
        if (movementScript.rb.velocity.y > 0.01)
        {
            isJumping = true;
            isFalling = false;
        }
        else if(movementScript.rb.velocity.y < -5.00)
        {
            isJumping = false;
            isFalling = true;
        } else
        {
            isJumping = false;
            isFalling = false;
        }


        //is player moving left or right
        if(movementScript.Horizontal > 0.01)
        {
            isFacingRight = true;
        } else
        {
            isFacingRight = false;
        }



        //if moving flip the player sprite
        if (isMoving)
        {
            if (isFacingRight)
            {
                this.GetComponentInChildren<Transform>().localScale = new Vector3(1, 1, 1);
            }
            else
            {
                this.GetComponentInChildren<Transform>().localScale = new Vector3(-1, 1, 1);
            }
        }


        if (gooseOnBack)
        {
            if (isGrounded && isMoving && !usingSlingshot)
            {
                playerAnimator.Play("Move_Goose_Normal");
            }
            if (isGrounded && !isMoving && !usingSlingshot)
            {
                playerAnimator.Play("Idle_FacingForwad");
            }
            if (isJumping)
            {
                playerAnimator.Play("Jump_Goose_Normal");
            }
            if (isFalling)
            {
                playerAnimator.Play("PlayerFalling");
            }
        }
        else
        {
            if(isGrounded && isMoving && !usingSlingshot)
            {
                playerAnimator.Play("Move_Normal");
            }
            if (isGrounded && !isMoving && !usingSlingshot)
            {
                playerAnimator.Play("Idle_FacingForwad");
            }
            if(isJumping)
            {
                playerAnimator.Play("Jump_Normal");
            }
            if(isFalling)
            {
                playerAnimator.Play("PlayerFalling");
            }
        }
    }
}
