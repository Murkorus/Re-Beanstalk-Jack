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

    public bool gooseOnBack;

    public bool isFacingRight;

    // Update is called once per frame

    public void Start() {
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void Update() {
        isGrounded = playerMovement.isGrounded;

        if(playerMovement.Horizontal == 0) {
            isMoving = false;
        } else {
            isMoving = true;
        }
    }

}
