using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GiantThrowing : MonoBehaviour
{
    public float speed;
    public float health;

    public bool isIdle;

    public GameObject boulder;
    public GameObject target;
    public GameObject throwPoint;

    [Header("Attacking")]
    public bool isAttacking;
    public float attackTimeLeft;
    public float attackTime;

    [Header("Attack wave")]
    public bool attackWave;
    public bool attackWaveSound;
    //public GameObject AttackWave;
    //public GameObject WavePosition;
    public Animator animator;

    [Header("Player Detection")]
    public GameObject playerDetection;
    public float playerDetectionRadius;
    public LayerMask playerlayer;

    private GameObject GM;


    void Start()
    {
        target = GameObject.Find("Player");
        GM = GameObject.Find("GM");
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerDetection.transform.position, playerDetectionRadius);
    }
    void Update()
    {
        if (isIdle)
        {
            if (Physics2D.OverlapCircle(playerDetection.transform.position, playerDetectionRadius, playerlayer, Mathf.Infinity, Mathf.Infinity))
            {
                Attack();
            }
        }

        //Animations
        if (!isAttacking) //if not staggered or attakcing
        {
            isIdle = true;
            animator.Play("RedgiantIdle");
        }
        else // if is attacking
        {
            isIdle = false;
            if (attackTimeLeft < 0.55 && !attackWave)
            {
                Debug.Log("Throw");
                GameObject boulderGO = Instantiate(boulder, throwPoint.transform.position, Quaternion.identity);
                attackWave = true;
            }
            if (attackTimeLeft > 0)
            {
                attackTimeLeft -= Time.deltaTime;
                animator.Play("redGiantThrow");
            }
            else
            {
                //Debug.Log("No Longer staggered");
                isAttacking = false;
                attackWave = false;
            }
        }
    }

    public void Attack()
    {
        isAttacking = true;
        attackTimeLeft = attackTime;
    }
}
