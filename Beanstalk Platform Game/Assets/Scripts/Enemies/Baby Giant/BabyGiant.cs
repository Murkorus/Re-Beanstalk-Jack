using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BabyGiant : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D RB;
    [Space(10)]
    
    
    public int currentState; //1 = Waiting, 2 = Attacking, 3 = avoiding player
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
    
    [Header("Avoid settings")]    
    public bool isAvoiding;
    public float avoidDistanceMax;
    

    [Header("Detection")]
    public LayerMask playerLayer;
    public LayerMask groundLayer;
    public bool isChecking;
    public bool hitPlayer;

    public GameObject FrontDetection;
    public GameObject BackDetection;

    public bool canMoveBack;
    public bool canMoveForward;

    
    void Start()
    {
        currentState = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (xDistance < DistanceToPlayerThreshold && currentState == 1)
        {
            PlayerSide = 0;
            BabyGiantAttack();
        }
        

        //Player position
        PlayerSide = GameObject.Find("Player").transform.position.x - transform.position.x;
        PlayerSide = Mathf.Clamp(PlayerSide, -1, 1);
        
        xDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y));
        yDistance = transform.position.y - GameObject.Find("Player").transform.position.y;
        
        //flip baby giant
        if (PlayerSide > 0 && !isChecking)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if(PlayerSide < 0 && !isChecking)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        //player collsion
        if (isChecking && !hitPlayer && !GameObject.Find("Player").GetComponent<PlayerCombatController>().isDodging)
        {
            if(Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
            {
                GameObject.Find("Player").GetComponent<PlayerStats>().takeDamage();
                hitPlayer = true;
            }
        }
        
        
        //Drop detection
        if (!Physics2D.OverlapCircle(BackDetection.transform.position, .15f, groundLayer))
        {
            Debug.Log("Close to edge in the back");
            canMoveBack = false;
        }
        else
        {
            canMoveBack = true;
        }
        if (!Physics2D.OverlapCircle(FrontDetection.transform.position, .15f, groundLayer))
        {
            Debug.Log("Close to edge in the front");
            canMoveForward = false;
        }
        else
        {
            canMoveForward = true;
        }
    }


    public void FixedUpdate()
    {
        if (xDistance > DistanceToPlayerThreshold && currentState == 1)
        {
            runTowardsPlayer();
        }
        
        if (currentState == 3)
        {
            avoidPlayer();
            if (xDistance < 1)
            {
                Debug.Log("Player is too close");
                StartCoroutine(dashAttack());
            }

            if (xDistance > avoidDistanceMax)
            {
                currentState = 1;
            }
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


    public void avoidPlayer()
    {
        StartCoroutine(avoidPlayerTimer());
        if (PlayerSide > 0 && canMoveBack)
        {
            Debug.Log(("Player is on the right side"));
            float targetSpeed = -PlayerSide * runSpeed;
			
            float accelRate;
        
            float speedDif = targetSpeed - RB.velocity.x;

            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? AccelAmount : DeccelAmount;

            float movement = speedDif * accelRate * 0.75f;
        
            RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }
        
        if (PlayerSide < 0 && canMoveForward)
        {
            Debug.Log("Player is on the left side");
            float targetSpeed = -PlayerSide * runSpeed;
			
            float accelRate;
        
            float speedDif = targetSpeed - RB.velocity.x;

            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? AccelAmount : DeccelAmount;

            float movement = speedDif * accelRate * 0.75f;
        
            RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }
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
        yield return new WaitForSeconds(.1f);
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
        
        //Collision
        isChecking = true;
        yield return new WaitForSeconds(.5f);
        isChecking = false;
        hitPlayer = false;
        currentState = 3;

        isPerformingAttack = false;
    }


    IEnumerator avoidPlayerTimer()
    {
        if (!isAvoiding)
        {
            isAvoiding = true;
            currentState = 3;
            yield return new WaitForSeconds(3f);
            currentState = 1;
            isAvoiding = false;
        }
    }
}
