using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public string type;
    public Animator anim;
    private bool hasGiven;
    public LayerMask playerLayer;

    public void Start() {
        anim.Play("PebblePickupIdle", -1, Random.Range(0f, 1f));
    }


    public void Update() {
        if(Physics2D.OverlapCircle(transform.position, .25f, playerLayer)) {
            PickupObject(1, type);
        }
    }


    public void PickupObject(int amount, string type) {
        if(type.ToLower() == "pebble" && !hasGiven && GameObject.Find("Player").GetComponent<PlayerStats>().pebbles < 25) {
            hasGiven = true;
            GameObject.Find("Player").GetComponent<PlayerStats>().addPebbles(amount);
            Destroy(transform.parent.gameObject);
        }
        if(type.ToLower() == "platform" && !hasGiven && GameObject.Find("Player").GetComponent<PlayerStats>().platformBeans < 10) {
            hasGiven = true;
            GameObject.Find("Player").GetComponent<PlayerStats>().addPlatform(amount);
            Destroy(transform.parent.gameObject);
        }
        if(type.ToLower() == "fire" && !hasGiven && GameObject.Find("Player").GetComponent<PlayerStats>().fireBean < 10) {
            hasGiven = true;
            GameObject.Find("Player").GetComponent<PlayerStats>().addFire(amount);
            Destroy(transform.parent.gameObject);
        }
        if(type.ToLower() == "ice" && !hasGiven && GameObject.Find("Player").GetComponent<PlayerStats>().iceBean < 10) {
            hasGiven = true;
            GameObject.Find("Player").GetComponent<PlayerStats>().addIce(amount);
            Destroy(transform.parent.gameObject);
        }
        if(type.ToLower() == "mind" && !hasGiven && GameObject.Find("Player").GetComponent<PlayerStats>().mindBean < 25) {
            hasGiven = true;
            GameObject.Find("Player").GetComponent<PlayerStats>().addMind(amount);
            Destroy(transform.parent.gameObject);
        }
        if (type.ToLower() == "health" && !hasGiven && GameObject.Find("Player").GetComponent<PlayerStats>().health < 8)
        {
            hasGiven = true;
            GameObject.Find("Player").GetComponent<PlayerStats>().addHealth();
            Destroy(transform.parent.gameObject);
        }
    }
}
