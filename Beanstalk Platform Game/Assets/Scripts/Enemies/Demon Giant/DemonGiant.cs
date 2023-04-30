using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class DemonGiant : MonoBehaviour
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
        if(currentState == 1) //idle
        {
            //Play idle animation
            anim.Play("DemonGiant_Idle");
            isWalking = false;
        }
        if(currentState == 2) //Walking
        {
            //Play walking animation
            anim.Play("DemonGiant_Walk");
            isWalking = true;
        }

        if(Input.GetKeyDown(KeyCode.H)) {
            attack();
        }

        if(Physics2D.OverlapBox(WallDetection.transform.position, WallDetection.transform.localScale, 0, wallLayer)) {
            ïsFacingRight = !ïsFacingRight;
        }
        if(!Physics2D.OverlapBox(FloorDetection.transform.position, WallDetection.transform.localScale, 0, wallLayer)) {
            ïsFacingRight = !ïsFacingRight;
        }

        if(Physics2D.OverlapBox(PlayerDetection.transform.position, PlayerDetection.transform.localScale, 0, playerLayer)) {
            playerDetected = true;
            _angryTimer = 0;
        } else {
            playerDetected = false;
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

        if(playerDetected) {
            if(_angryTimer < _angryTime) {
                _angryTimer += Time.time;
                isAngry = true;
            } else  {
                print("giant is no longer angry");
                playerDetected = false;
                isAngry = false;
            }
        }
    }
    


    public void attack()
    {
        isAttacking = true;
        attackCooldown = true;
        anim.Play("DemonGiant_Stomp");
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
