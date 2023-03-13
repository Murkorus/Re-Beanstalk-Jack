using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class Enemy_NormalGiant : MonoBehaviour
{
    [SerializeField] private int currentState; //1 = Idle / Normal Walking  |  2 = Attacking / Mad
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isAttacking;
    [SerializeField] private GameObject stompSpawnPosition;
    [SerializeField] private GameObject stompPrefab;
    
    //Attack cooldown
    [SerializeField] private bool attackCooldown;
    [SerializeField] private float attackCooldownTime;
    [SerializeField] private float attackCooldownTimer;



    private Animator enemyanimator;
    public CinemachineVirtualCamera vcam;
    public CinemachineBasicMultiChannelPerlin noise;



    // Start is called before the first frame update
    void Start()
    {
        currentState = 1;
        enemyanimator = GetComponent<Animator>();

        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera> ();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin> ();
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
        }


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
        GameObject LeftParticles = Instantiate(stompPrefab, stompSpawnPosition.transform.position, Quaternion.identity);    
        LeftParticles.GetComponent<StompController>().isLeft = true;

        GameObject rightParticles = Instantiate(stompPrefab, stompSpawnPosition.transform.position, Quaternion.identity);     
        rightParticles.GetComponent<StompController>().isLeft = false; 
        StartCoroutine(shakeScreen());
    }


    public void Noise(float amplitudeGain, float frequencyGain) {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;    
    }


    IEnumerator shakeScreen() {
        Noise(2, 5);
        yield return new WaitForSeconds(0.25f);
        Noise(0, 0);
        isAttacking = false;
    }
}
