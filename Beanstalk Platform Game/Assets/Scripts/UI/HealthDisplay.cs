using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private int playerHealth;
    public Vector3 healthStartPosition;
    public Vector3 healthOffset;
    public GameObject healthPrefab;

    public List<GameObject> heartGOList;
    

    void Start()
    {
        for (int i = 0; i < GameObject.Find("Player").GetComponent<PlayerStats>().health; i++)
        {
            addHealthToList();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            playHeartDamageAnimation();
        }
    }


    public void addHealthToList()
    {
        GameObject heart = Instantiate(healthPrefab, new Vector3(healthStartPosition.x, healthStartPosition.y, healthStartPosition.z) + new Vector3(healthOffset.x * heartGOList.Count, healthOffset.y * heartGOList.Count, healthOffset.z),
            quaternion.identity);
        heart.transform.SetParent(transform);
        heartGOList.Add(heart);
    }

    public void playHeartDamageAnimation()
    {
        heartGOList[heartGOList.Count - 1].GetComponent<Animator>().Play("Heart_Damage");   
    }
    
    public void removeHealthToList()
    {
        Destroy(heartGOList[heartGOList.Count - 1]);
        heartGOList.RemoveAt(heartGOList.Count - 1);
    }
}
