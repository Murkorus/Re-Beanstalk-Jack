using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class Enemy_NormalGiant : MonoBehaviour
{
    [Header("Basic settings")]
    public float stompDamage;
    public float angryTime;
    private float angryTimer;


    [Header("Booleans")]
    [SerializeField] private int currentState; //1 = Idle / Normal Walking  |  2 = Attacking / Mad
    [SerializeField] private bool isWalking;
    [SerializeField] private bool ïsFacingRight;
    [SerializeField] private bool isAttacking;

    [Header("Stomp")]
    [SerializeField] private GameObject stompSpawnPosition;
    [SerializeField] private GameObject stompPrefab;
    
    //Attack cooldown
    [Header("Attack cooldown")]
    [SerializeField] private bool attackCooldown;
    [SerializeField] private float attackCooldownTime;
    [SerializeField] private float attackCooldownTimer;

    public screenShake screenshake;

    private Animator enemyanimator;

    [Header("Wall detection")]
    [SerializeField] private GameObject WallDetection;
    [SerializeField] private GameObject PlayerDetection;
    public LayerMask groundLayer;
    public LayerMask playerLayer;


    // Start is called before the first frame update
    void Start()
    {
        currentState = 1;
        enemyanimator = GetComponent<Animator>();

        screenshake = GameObject.Find("CM vcam1").GetComponent<screenShake>();
        StartCoroutine(MoveOrIdle());
    }

    // Update is called once per frame
    void Update()
    {
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

        //State
        if(currentState == 1) {
            if(isWalking) {
                enemyanimator.Play("Enemy_NormalGiant_happy_walking");
            } else {
                enemyanimator.Play("Enemy_NormalGiant_Idle");
            }
        }
        if(currentState == 2 && !isAttacking) {
            enemyanimator.Play("Enemy_NormalGiant_angryy_walking"); 
            isWalking = true;
        }

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

        if(Physics2D.OverlapBox(WallDetection.transform.position, WallDetection.transform.localScale, 0, groundLayer)) {
            ïsFacingRight = !ïsFacingRight;
        }

        if(Physics2D.OverlapBox(PlayerDetection.transform.position, PlayerDetection.transform.localScale, 0, playerLayer)) {
            currentState = 2;
            angryTimer = 0;
        } else {
            angryTimer += 1 * Time.deltaTime;
            if(angryTimer > angryTime)
                currentState = 1;
        }
    }


    IEnumerator MoveOrIdle() {
        if(currentState == 1) {
            int randomNum = Random.Range(1, 3);
            if(randomNum == 1) {
                isWalking = true;
            } else {
                isWalking = false;
            }
        }
        yield return new WaitForSeconds(3);
        
        StartCoroutine(MoveOrIdle());
    }



    IEnumerator randomAttackWait()
    {
        yield return new WaitForSeconds(Random.Range(0.00f, 3.00f));
        attack();
    }

    public void attack()
    {
        isAttacking = true;
        attackCooldown = true;
        Debug.Log("attack");  
        enemyanimator.Play("Enemy_NormalGiant_Stomp");
    }


    public void stomp() {

        float distanceToPlayer = Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) / 4;
        GameObject LeftParticles = Instantiate(stompPrefab, stompSpawnPosition.transform.position, Quaternion.identity);    
        LeftParticles.GetComponent<StompController>().isLeft = true;

        GameObject rightParticles = Instantiate(stompPrefab, stompSpawnPosition.transform.position, Quaternion.identity);     
        rightParticles.GetComponent<StompController>().isLeft = false; 
        screenshake.shake(0.25f, 2f / distanceToPlayer, 5f / distanceToPlayer);
        isAttacking = false;
    }
}
