using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private int playerHealth;
    public Vector3 healthStartPosition;
    public Vector3 healthOffset;
    public GameObject healthPrefab;

    void Start()
    {
        updateHealth();
    }

    public void updateHealth()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        playerHealth = GameObject.Find("Player").GetComponent<PlayerStats>().health;
        for (int i = 0; i < playerHealth; i++)
        {
            GameObject health = Instantiate(healthPrefab, new Vector3(healthStartPosition.x, healthStartPosition.y, healthStartPosition.z) + new Vector3(healthOffset.x * i, healthOffset.y * i, healthOffset.z),
                quaternion.identity);
            health.transform.parent = this.transform;
        }
    }
}
