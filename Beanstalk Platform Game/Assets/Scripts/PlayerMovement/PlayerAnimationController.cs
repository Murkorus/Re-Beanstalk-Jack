using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Animator playerAnimator;

    public bool isGrounded;

    public bool isMoving;

    public bool isJumping;

    public bool isFalling;

    public bool usingSlingshot;
    public Slingshot slingshot;

    public bool gooseOnBack;

    public bool isFacingRight;

    // Update is called once per frame

    public void Start() {
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void Update() {
        isGrounded = playerMovement.isGrounded;
        usingSlingshot = slingshot.isCharging;
        isFacingRight = playerMovement.isFacingRight;


        if(playerMovement.Horizontal == 0) {
            isMoving = false;
        } else {
            isMoving = true;
        }

        if(playerMovement.rb.velocity.y > 0.01) {
            isJumping = true;
        } else {
            isJumping = false;  
        }
        
        if(playerMovement.rb.velocity.y > 2) {
            if(gooseOnBack) {
                playerAnimator.Play("Jump_Goose_Normal");
            } else {
                playerAnimator.Play("Jump_Normal");
            }
        }
        if(playerMovement.rb.velocity.y < -20) {
            playerAnimator.Play("PlayerFalling");
        }

        //Idle
        if(isGrounded && !isMoving && !usingSlingshot) {
            if(gooseOnBack) {
                playerAnimator.Play("Idle_FacingSide_Goose");
            } else {
                playerAnimator.Play("Idle_FacingSide");
            }
        }
        

        //Moving without charging slingshot
        if(isGrounded && isMoving && !usingSlingshot) {
            if(gooseOnBack) {
                playerAnimator.Play("Move_Goose_Normal");
            } else {
                playerAnimator.Play("Move_Normal");
            }
        }


        //Moving while charging slingshot
        if(isGrounded && isMoving && usingSlingshot) {
            if(gooseOnBack) {
                //playerAnimator.Play("Move_Goose_Normal");
            } else {
                //playerAnimator.Play("Move_Normal");
            }
        }
        
    }

}
