using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class Enemy_NormalGiant : MonoBehaviour
{
    [Header("Basic settings")]
    public float stompDamage;
    [SerializeField] private float _angryTime;
    private float _angryTimer;


    [Header("Booleans")]
    [SerializeField] private int currentState; //1 = Idle | 2 = Normal Walking
    [SerializeField] private bool isWalking;
    [SerializeField] private bool ïsFacingRight;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isAngry;

    [Header("Stomp")]
    [SerializeField] private GameObject stompSpawnPosition;
    [SerializeField] private GameObject stompPrefab;
    
    //Attack cooldown
    [Header("Attack cooldown")]
    [SerializeField] private bool attackCooldown;
    [SerializeField] private float attackCooldownTime;
    [SerializeField] private float attackCooldownTimer;

    public screenShake screenshake;
    private Animator anim;

    [Header("Detection")]
    [SerializeField] private GameObject WallDetection;
    [SerializeField] private GameObject FloorDetection;
    [SerializeField] private GameObject PlayerDetection;
    private bool playerDetected;
    public LayerMask wallLayer;
    public LayerMask playerLayer;


    // Start is called before the first frame update
    
    public void Start() {
        anim = GetComponent<Animator>();
    }

    public void Update() {
        if(currentState == 1 && !isAttacking) //idle
        {
            //Play idle animation
            anim.Play("Enemy_NormalGiant_Idle");
            isWalking = false;
        }
        if(currentState == 2 && !isAttacking && !isAngry) //Walking
        {
            //Play walking animation
            anim.Play("Enemy_NormalGiant_happy_walking");
            isWalking = true;
        }
        if(currentState == 2 && !isAttacking && isAngry) {
            anim.Play("Enemy_NormalGiant_angry_walking");
            isWalking = true;
        }

        if(Input.GetKeyDown(KeyCode.H))
            attack();
        

        if(Physics2D.OverlapBox(WallDetection.transform.position, WallDetection.transform.localScale, 0, wallLayer)) {
            ïsFacingRight = !ïsFacingRight;
        }
        if(!Physics2D.OverlapBox(FloorDetection.transform.position, WallDetection.transform.localScale, 0, wallLayer)) {
            ïsFacingRight = !ïsFacingRight;
        }

        if(Physics2D.OverlapBox(PlayerDetection.transform.position, PlayerDetection.transform.localScale, 0, playerLayer)) {
            playerDetected = true;
            _angryTimer = 0;
            isAngry = true;
        } else
            playerDetected = false;
        
        if(isWalking) {
            if(ïsFacingRight && !isAttacking) {
                transform.position = new Vector3(transform.position.x + 2 * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(1, 1, 1);

            }
            if(!ïsFacingRight && !isAttacking) {
                transform.position = new Vector3(transform.position.x + -2 * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }


        if(isAngry)
            currentState = 2;

        if (currentState == 2 && !attackCooldown)
        {
            attackCooldownTimer = 0;
            attackCooldown = true;
            StartCoroutine(randomAttackWait());
        }


        if (attackCooldownTime > attackCooldownTimer && attackCooldown == true)
        {
            attackCooldownTimer += Time.deltaTime;
            attackCooldown = true;
        }
        else
        {
            attackCooldown = false;
        }
    }
    


    public void attack()
    {
        attackCooldownTime = Random.Range(3.0f, 6.0f);
        anim.Play("Enemy_NormalGiant_Stomp");   
        isAttacking = true;
        isWalking = false;
        attackCooldown = true;
    }


    public void stomp() {
        GameObject LeftParticles = Instantiate(stompPrefab, stompSpawnPosition.transform.position, Quaternion.identity);    
        LeftParticles.GetComponent<StompController>().isLeft = true;

        GameObject rightParticles = Instantiate(stompPrefab, stompSpawnPosition.transform.position, Quaternion.identity);     
        rightParticles.GetComponent<StompController>().isLeft = false; 
        
        isAttacking = false;
    }
    public void shake() 
    {
        float distanceToPlayer = Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) / 10;

        screenshake.shake(0.25f, 1f / distanceToPlayer, .25f / distanceToPlayer);

    }


    IEnumerator randomAttackWait()
    {
        yield return new WaitForSeconds(Random.Range(0.00f, 3.00f));
        attack();
    }
}
