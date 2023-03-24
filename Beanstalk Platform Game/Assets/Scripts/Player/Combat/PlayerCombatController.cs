using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [Header("Dagger settings")]
    [SerializeField] private float heavyAttackDamage = 20f;
    [SerializeField] private float heavyAttackTime = 3f;
    public bool isPerformingHeavyAttack;
    public float heavyAttackThreshold;

    [Space(10)]


    [SerializeField] private float lightAttackDamage = 10f;
    [SerializeField] private float lightAttackTime = 1f;
    public bool isPerformingLightAttack;
    public float attackHoldTime;
    public bool isChargingAttack;


    [Space(5)]


    [Header("Slingshot settings")]
    [SerializeField] private float slingshotDamage;
    [SerializeField] private float slingshotChargeTime;
    public bool isChargingSlingshot;

    [Space(10)]


    [Header("Dodge settings")]
    [SerializeField] private float dodgeForce;
    [SerializeField] private float dodgeTime;
    [SerializeField] private float iFrames;
    public bool isDodging;
    

     [Header("Other scripts")]
     public PlayerMovement pm;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            if(!isPerformingLightAttack && !isPerformingHeavyAttack && !isDodging) {
                if(isChargingSlingshot)
                    isChargingSlingshot = false;
                isChargingAttack = true;
                
            }
        }
        //attack charge check
        if(isChargingAttack)
            attackHoldTime += Time.deltaTime;
            if(Input.GetMouseButtonUp(0)) {
                isChargingAttack = false;
                if(attackHoldTime > heavyAttackThreshold) {
                    heavyAttack();
                } else {
                    lightAttack();
                }
                attackHoldTime = 0;
            }
        if(Input.GetKeyDown(KeyCode.LeftControl) && !isDodging && pm.isGrounded && !isPerformingHeavyAttack && !isPerformingLightAttack) {
            dodge();
        }
    }



    public void heavyAttack() {
        isPerformingHeavyAttack = true;
        StartCoroutine(eventTime(isPerformingHeavyAttack, heavyAttackTime));
        Debug.Log("Heavy attack");
    }


    public void lightAttack() {
        isPerformingLightAttack = true;
        StartCoroutine(eventTime(isPerformingLightAttack, lightAttackTime));
        Debug.Log("Light attack");
    }


    public void slingshot() {
        isChargingSlingshot = true;
    }



#region dodge
    public void dodge() {
        isDodging = true;
        StartCoroutine(dodgeRoutine(dodgeTime));
        StartCoroutine(eventTime(isDodging, 2));
    }

    IEnumerator dodgeRoutine(float time) {
        pm.isDodging = true;
        pm.RB.velocity = new Vector2(0, 0);
        if(pm.IsFacingRight) {
            pm.RB.AddForce(new Vector2(dodgeForce, 0));
        } else {
            pm.RB.AddForce(new Vector2(-dodgeForce, 0));

        }
        yield return new WaitForSeconds(time);
        pm.isDodging = false;
        isDodging = false;

    }
#endregion




    IEnumerator eventTime(bool value, float time) {
        yield return new WaitForSeconds(time);
            
        if(value = isPerformingHeavyAttack) {
            isPerformingHeavyAttack = false;
        }
        if(value = isPerformingLightAttack) {
            isPerformingLightAttack = false;
        }
         yield return null;
    }
}
