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

    //Stagger variables
    [Header("Staggered")]
    public bool staggered;
    public float staggerTimeLeft;
    public float staggerTime;

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




    Vector3 calcBallisticVelocityVector(Vector3 source, Vector3 target, float angle)
    {
        Vector3 direction = target - source;
        float h = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float a = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += h / Mathf.Tan(a);

        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * direction.normalized;
    }


    void Start()
    {
        target = GameObject.Find("Player");
        GM = GameObject.Find("GM");
    }

    // Update is called once per frame

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


        //if not staggered
        if (staggered == false)
        {
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
                    boulderGO.GetComponent<Rigidbody2D>().AddForce(calcBallisticVelocityVector(transform.position, target.transform.position, 33) * 41);
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
        else
        {
            if (staggerTimeLeft > 0)
            {
                staggerTimeLeft -= Time.deltaTime;
                animator.Play("StaggerRedGiant");
                isIdle = false;
            }
            else
            {
                //Debug.Log("No Longer staggered");
                staggered = false;
                isIdle = true;
            }
        }
    }

    public void StaggerGiant()
    {
        staggered = true;
        staggerTimeLeft = staggerTime;
    }

    public void Attack()
    {
        isAttacking = true;
        attackTimeLeft = attackTime;
    }

    public void takeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
    }
}
