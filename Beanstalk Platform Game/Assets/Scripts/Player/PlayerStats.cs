using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
    {
        public int health;
        public int pebbles;
        public int platformBeans;
        public int fireBean;
        public int iceBean;
        public int mindBean;
        
        public screenShake screenshake;

        
        void Start()
        {
            screenshake = GameObject.Find("CM vcam1").GetComponent<screenShake>();

            health = GameObject.Find("SaveSystem").GetComponent<SaveSystem>().save.playerHealth;
            pebbles = GameObject.Find("SaveSystem").GetComponent<SaveSystem>().save.pebbles;
            platformBeans = GameObject.Find("SaveSystem").GetComponent<SaveSystem>().save.platform;
            fireBean = GameObject.Find("SaveSystem").GetComponent<SaveSystem>().save.fire;
            iceBean = GameObject.Find("SaveSystem").GetComponent<SaveSystem>().save.ice;
            mindBean = GameObject.Find("SaveSystem").GetComponent<SaveSystem>().save.mind;
        }

        // Update is called once per frame
        void Update()
        {
            if(health <= 0) {
                PlayerDeath();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                takeDamage();
            }
        }

        #region health
        public void takeDamage() {
            health -= 1;
            GameObject.Find("Player").GetComponentInChildren<HealthDisplay>().removeHealthToList();
            screenshake.shake(0.25f, 1f, .1f);
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

        public void addSinglePebble() {
            pebbles++;
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

        //Fire beans
        #region Fire
        public void removeFire(int amount) {
            fireBean -= amount;
        }

        public void addFire(int amount) {
            fireBean += amount;
        }
        #endregion
        
        //Ice beans
        #region ice
        public void removeIce(int amount) {
            iceBean -= amount;
        }

        public void addIce(int amount) {
            iceBean += amount;
        }
        #endregion
        
        //Mind beans
        #region Platforms
        public void removeMind(int amount) {
            mindBean -= amount;
        }

        public void addMind(int amount) {
            mindBean += amount;
        }
        #endregion

    }
