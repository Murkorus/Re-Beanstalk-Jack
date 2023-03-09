using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public string currentProjectile;



    //Projectile amounts
    public int pebbleAmount;

    //special beans
    public int platformBeanAmount;
    public int fireBeanAmount;
    public int mindControlBeanAmount;


    //Bools

    public bool haveProjectile;
    public bool isCharging;
    public bool slingshotMenuIsOpen;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(!slingshotMenuIsOpen) {
            if(Input.GetMouseButton(1)) {
                updateSlingshot();
                if(haveProjectile == true) {
                    isCharging = true;
                } else {
                    isCharging = false;
                }
            } else {
                isCharging = false;
            }
        }


        if(!isCharging) {
            if(Input.GetKey(KeyCode.Tab)) {
                slingshotMenuIsOpen = true;
            } else {
                slingshotMenuIsOpen = false;
            }
        }
    }


    public void changeProjectile(string type) {
        currentProjectile = type;
    }



    public void updateSlingshot() {
        if(currentProjectile == "pebble") {
            if(pebbleAmount > 0) {
                haveProjectile = true;
            } else {
                haveProjectile = false;
            }
        }
        
        if(currentProjectile == "fire") {
            if(pebbleAmount > 0) {
                haveProjectile = true;
            } else {
                haveProjectile = false;
            }
        }

        if(currentProjectile == "ice") {
            if(pebbleAmount > 0) {
                haveProjectile = true;
            } else {
                haveProjectile = false;
            }
        }

        if(currentProjectile == "mind") {
            if(pebbleAmount > 0) {
                haveProjectile = true;
            } else {
                haveProjectile = false;
            }
        }
    }
}
