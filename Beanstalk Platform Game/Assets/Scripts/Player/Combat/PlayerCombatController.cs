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
    private float chargeTime;
    private float totalTime;
    public Transform projectilePoint;

    //Trajectory
    public GameObject slingshotPoint;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    Vector2 direction;


    [Header("Slingshot projectiles")]
    [SerializeField] private string currentProjectile;
    public float projectileForce;

    public GameObject slingshotPebbleGO;
    public int slingshotPebblesCount;

    public GameObject slingshotPlatformGO;
    public int slingshotPlatformsCount;

    public GameObject slingshotFireGO;
    public int slingshotFireCount;

    public GameObject slingshotIceGO;
    public int slingshotIceCount;

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
        InitializeSlingshot();
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


        Slingshot();
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


#region slingshot

    public void InitializeSlingshot() {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(slingshotPoint, projectilePoint.position, Quaternion.identity);
            points[i].SetActive(false);
        }
    }

    public void Slingshot() {
        Vector2 slingshotPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - slingshotPos;
        slingshotPoint.transform.right = direction;


        if (!isPerformingHeavyAttack && !isPerformingLightAttack)
                {
                    if (Input.GetMouseButton(1))
                    {
                        isChargingSlingshot = true;
                        //AM.GetComponent<AnimationManager>().isCharging = true;
                        if (currentProjectile == "normal")
                        {
                            if(GameObject.Find("Player").GetComponent<PlayerStats>().pebbles > 0) {
                                chargeTime += Time.deltaTime * 3;
                                projectileForce = chargeTime * 7.5f + 2;
                                projectileForce = Mathf.Clamp(projectileForce, 2, 10);
                            }
                        }
                        if(currentProjectile == "platform")
                        {
                            if(GameObject.Find("Player").GetComponent<PlayerStats>().platformBeans > 0) {
                                chargeTime += Time.deltaTime;   
                                projectileForce = chargeTime * 1.5f + 2;
                                projectileForce = Mathf.Clamp(projectileForce, 2, 4);
                            }
                        }
                    }
                    if (Input.GetMouseButtonUp(1))
                    {
                        isChargingSlingshot = false;
                        shootSlingshot();
                        chargeTime = 0.0f;
                        projectileForce = chargeTime;
                    }
                }


        //Trajectory
        for (int i = 0; i < numberOfPoints; i++)
        {
            if (projectileForce > 0)
            {
                points[i].gameObject.SetActive(true);
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
            else
            {
                points[i].gameObject.SetActive(false);
            }

            if(!isChargingSlingshot) {
                points[i].gameObject.SetActive(false);
                projectileForce = 0;
                chargeTime = 0;
            }
        }
    }



    public void shootSlingshot() {
        if (currentProjectile == "normal")
        {
            GameObject newProjectile = Instantiate(slingshotPebbleGO, projectilePoint.position, projectilePoint.rotation);
            newProjectile.GetComponent<Rigidbody2D>().velocity = slingshotPoint.transform.right * projectileForce;
            //newProjectile.GetComponent<Projectile>().force = projectileForce;
            GameObject.Find("Player").GetComponent<PlayerStats>().removePebbles(1);
        }
        if(currentProjectile == "platform")
        {
            GameObject newProjectile = Instantiate(slingshotPlatformGO, projectilePoint.position, projectilePoint.rotation);
            newProjectile.GetComponent<Rigidbody2D>().velocity = slingshotPoint.transform.right * projectileForce;
            GameObject.Find("Player").GetComponent<PlayerStats>().removePlatform(1);
        }
    }



    Vector2 PointPosition(float t)
    {
        Vector2 pos = (Vector2)projectilePoint.transform.position + (direction.normalized * projectileForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return pos;
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
