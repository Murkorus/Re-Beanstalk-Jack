using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyGiant : MonoBehaviour
{
    public int currentState; //1 = Waiting, 2 = Attacking
    public bool isAngry;
    public bool isPerformingAttack;
    [SerializeField] private float dashForce;
    
    [SerializeField] private float PlayerSide;
    [SerializeField] private float xDistance;
    [SerializeField] private float yDistance;
    
    [SerializeField] private float DistanceToPlayerThreshold;

    [Header("Movement settings")] 
    public float walkSpeed;
    public float runSpeed;
    public float AccelAmount;
    public float DeccelAmount;

    public Rigidbody2D RB;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (xDistance < DistanceToPlayerThreshold)
        {
            PlayerSide = 0;
        }

        PlayerSide = GameObject.Find("Player").transform.position.x - transform.position.x;
        PlayerSide = Mathf.Clamp(PlayerSide, -1, 1);
        
        xDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y));
        yDistance = transform.position.y - GameObject.Find("Player").transform.position.y;
        
        if (isAngry)
        {
            if (currentState == 1)
            {
                //Wait animation
            }
            if (currentState == 2)
            {
                //Attacking animation
            }
        }
        
        BabyGiantAttack();
    }


    public void FixedUpdate()
    {
        if (xDistance > DistanceToPlayerThreshold)
        {
            runTowardsPlayer();
        }
    }

    public void runTowardsPlayer()
    {

        float targetSpeed = PlayerSide * runSpeed;
			
        float accelRate;
        
        float speedDif = targetSpeed - RB.velocity.x;

        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? AccelAmount : DeccelAmount;

        float movement = speedDif * accelRate;
        
        RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    public void BabyGiantAttack()
    {
        if (!isPerformingAttack)
        {
            yDistance = Mathf.Abs(yDistance);
            if (yDistance < 0.16)
            {
                Debug.Log("is on the same floor: " + yDistance);
            
                //attack
                StartCoroutine(dashAttack());
            }
            else
            {
                Debug.Log(("Isn't on the same floor: " + yDistance));
            }
        }
        
        
        
    }


    IEnumerator dashAttack()
    {
        isPerformingAttack = true;
        currentState = 1;
        yield return new WaitForSeconds(4);
        currentState = 2;
        Debug.Log(xDistance);
        if (PlayerSide > 0 && xDistance < 3)
        {
            RB.AddForce(new Vector2(dashForce, 0));
        }
        else if(PlayerSide < 0 && xDistance < 3)
        {
            RB.AddForce(new Vector2(-dashForce, 0));
        }
        isPerformingAttack = false;
    }
}
