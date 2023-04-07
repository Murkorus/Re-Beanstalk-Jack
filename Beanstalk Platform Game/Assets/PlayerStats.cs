using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int pebbles;
    public int platformBeans;
    void Start()
    {
        health = GameObject.Find("SaveSystem").GetComponent<SaveSystem>().save.playerHealth;
        pebbles = GameObject.Find("SaveSystem").GetComponent<SaveSystem>().save.pebbles;
        platformBeans = GameObject.Find("SaveSystem").GetComponent<SaveSystem>().save.platform;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            PlayerDeath();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            takeDamage(1);
        }
    }

    #region health
    public void takeDamage(int amount) {
        health -= amount;
        GameObject.Find("Player").GetComponentInChildren<HealthDisplay>().updateHealth();
    }

    public void PlayerDeath() {
        GameObject.Find("SaveSystem").GetComponent<SaveSystem>().Load(GameObject.Find("SaveSystem").GetComponent<SaveSystem>().currentSave);
    }



    #endregion
    //Pebbles
    #region Pebbles
    public void removePebbles(int amount) {
        pebbles -= amount;
    }

    public void addPebbles(int amount) {
        pebbles += amount;
    }
    #endregion

    //Platform beans
    #region Platforms
    public void removePlatform(int amount) {
        platformBeans -= amount;
    }

    public void addPlatform(int amount) {
        platformBeans += amount;
    }
    #endregion




}
