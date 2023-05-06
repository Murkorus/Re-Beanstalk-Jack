using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    public GameObject controller;
    public AudioSource walkingSound;
    public AudioClip grass, stone, corruption, cloud;

    private PlayerAnimationController playerAnimationController;
    private int layerMask;


    private void Awake()
    {
        layerMask = LayerMask.GetMask("Ground");
        playerAnimationController = controller.GetComponent<PlayerAnimationController>();
    }

    void Update()
    {
        if (playerAnimationController.isGrounded == true && playerAnimationController.isMoving == true)  
        {
            RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, Vector2.down,Mathf.Infinity,layerMask);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Grass"))
                {
                    walkingSound.clip = grass;
                    walkingSound.enabled = true;
                }
                else if (hit.collider.gameObject.CompareTag("Stone"))
                {
                    walkingSound.clip = stone;
                    walkingSound.enabled = true;
                }
                else if (hit.collider.gameObject.CompareTag("Corruption"))
                {
                    walkingSound.clip = corruption;
                    walkingSound.enabled = true;
                }
                else if (hit.collider.gameObject.CompareTag("Cloud"))
                {
                    walkingSound.clip = cloud;
                    walkingSound.enabled = true;
                }
            }
        }
        else
        {
            walkingSound.enabled = false;
        }
    }
}
