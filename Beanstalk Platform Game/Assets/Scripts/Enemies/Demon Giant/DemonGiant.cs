using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonGiant : MonoBehaviour
{
    [Header("Stomp")]
    public bool hasStomped;
    [SerializeField] float stompDamage;
    [SerializeField] float stompCooldown;
    float stompCooldownTimer;
    private float StompDamage;
    public GameObject stompPrefab;
    public GameObject stompSpawnPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Stomp cooldown
        if(stompCooldown > stompCooldownTimer)
            stompCooldownTimer += Time.deltaTime;
        else {
            hasStomped = false;
        }

        if(Input.GetKeyDown(KeyCode.J)) {
            stomp();
        }
    }



    public void stomp() {
        stompCooldownTimer = 0;
        hasStomped = true;
    }


    public void spawnStomp() {
        GameObject stomp = Instantiate(stompPrefab, stompSpawnPoint.transform.position, Quaternion.identity);
    }
}
