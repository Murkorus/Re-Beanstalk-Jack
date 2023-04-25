using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health;
    public AudioClip audio;
    public GameObject audioPrefab;

    public void start() {
        
    }


    private void Death() {
        Destroy(this.gameObject);
        Instantiate(audioPrefab, transform.position, Quaternion.identity);
    }
    public void damage(float damage) {
        health -= damage;
        if(health <= 0) {

        }
    }
}
