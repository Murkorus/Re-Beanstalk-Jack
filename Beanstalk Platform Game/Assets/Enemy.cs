using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;

    private void Death() {
        Destroy(this.gameObject);
    }

    public void Update() {
        if(health <= 0) {
            Death();
        }
    }
    public void damage(float damage) {
        health -= damage;
        if(health <= 0) {
            Death();
        }
    }
}
