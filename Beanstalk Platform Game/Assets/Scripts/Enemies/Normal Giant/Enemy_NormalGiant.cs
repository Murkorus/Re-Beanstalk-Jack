using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_NormalGiant : MonoBehaviour
{
    [SerializeField] private int currentState; //1 = Idle / Normal Walking  |  2 = Attacking / Mad
    [SerializeField] private GameObject stompSpawnPosition;
    
    //Attack cooldown
    [SerializeField] private bool attackCooldown;
    [SerializeField] private float attackCooldownTime;
    [SerializeField] private float attackCooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentState = 1;
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
    }

    IEnumerator randomAttackWait()
    {
        yield return new WaitForSeconds(Random.Range(2.00f, 6.00f));
        attack();
    }

    public void attack()
    {
        attackCooldown = true;
        Debug.Log(("attack"));
    }
}
