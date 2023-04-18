using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public GameObject slingshotPlatformGO;

    public GameObject slingshotFireGO;

    public GameObject slingshotIceGO;

    public GameObject slingshotMindGO;

    [Space(10)]



    [Header("Dodge settings")]
    [SerializeField] private float dodgeForce;
    [SerializeField] private float dodgeTime;
    [SerializeField] private float iFrames;
    public bool isDodging;
    

    [Header("Weapon wheel")]
    [SerializeField] private int weaponWheelselected;
    [SerializeField] private List<string> projectiles;
    [SerializeField] private List<GameObject> weaponWheelSlots;
    [SerializeField] private List<float> weaponWheelSlotsDistance;
    [SerializeField] private GameObject weaponWheelGO;
    private float smallestDistance;
    private bool usingWeaponWheel;
    private TextMeshProUGUI weaponWheelText;




     [Header("Other scripts")]
     public PlayerMovement pm;


    // Start is called before the first frame update
    void Start()
    {
        InitializeSlingshot();
        weaponWheelText = GameObject.Find("SelectedInWeaponWheel").GetComponent<TextMeshProUGUI>();
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
        if(Input.GetKeyDown(KeyCode.LeftControl) && !isDodging && !isPerformingHeavyAttack && !isPerformingLightAttack) {
            dodge();
        }


        Slingshot();
        weaponWheel();
    }


#region Melee
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
#endregion



#region dodge
    public void dodge() {
        isDodging = true;
        StartCoroutine(dodgeRoutine(dodgeTime));
        StartCoroutine(eventTime(isDodging, 1));
    }

    IEnumerator dodgeRoutine(float time)
    {
        pm.isDodging = true;
        pm.RB.velocity = new Vector2(0, 0);
        if(pm.IsFacingRight) {
            pm.RB.AddForce(new Vector2(dodgeForce, 0));
        } else {
            pm.RB.AddForce(new Vector2(-dodgeForce, 0));

        }
        yield return new WaitForSeconds(time);
        pm.isDodging = false;
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


        if (!isPerformingHeavyAttack && !isPerformingLightAttack)
                {
                    if (Input.GetMouseButton(1))
                    {
                        Vector2 slingshotPos = transform.position;
                        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        direction = mousePos - slingshotPos;
                        slingshotPoint.transform.right = direction;
                        
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
                        if (currentProjectile == "fire")
                        {
                            if(GameObject.Find("Player").GetComponent<PlayerStats>().fireBean > 0) {
                                chargeTime += Time.deltaTime * 3;
                                projectileForce = chargeTime * 7.5f + 2;
                                projectileForce = Mathf.Clamp(projectileForce, 2, 10);
                            }
                        }
                        if (currentProjectile == "ice")
                        {
                            if(GameObject.Find("Player").GetComponent<PlayerStats>().iceBean > 0) {
                                chargeTime += Time.deltaTime * 3;
                                projectileForce = chargeTime * 7.5f + 2;
                                projectileForce = Mathf.Clamp(projectileForce, 2, 10);
                            }
                        }
                        if (currentProjectile == "mind")
                        {
                            if(GameObject.Find("Player").GetComponent<PlayerStats>().mindBean > 0) {
                                chargeTime += Time.deltaTime * 3;
                                projectileForce = chargeTime * 7.5f + 2;
                                projectileForce = Mathf.Clamp(projectileForce, 2, 10);
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
            if(GameObject.Find("Player").GetComponent<PlayerStats>().pebbles > 0) {
                GameObject newProjectile = Instantiate(slingshotPebbleGO, projectilePoint.position, projectilePoint.rotation);
                newProjectile.GetComponent<Rigidbody2D>().velocity = slingshotPoint.transform.right * projectileForce;
                GameObject.Find("Player").GetComponent<PlayerStats>().removePebbles(1);
            }
        }
        if(currentProjectile == "platform")
        {
            if(GameObject.Find("Player").GetComponent<PlayerStats>().platformBeans > 0)  {
                GameObject newProjectile = Instantiate(slingshotPlatformGO, projectilePoint.position, projectilePoint.rotation);
                newProjectile.GetComponent<Rigidbody2D>().velocity = slingshotPoint.transform.right * projectileForce;
                GameObject.Find("Player").GetComponent<PlayerStats>().removePlatform(1);
            }
        }
        if(currentProjectile == "fire")
        {
            if(GameObject.Find("Player").GetComponent<PlayerStats>().fireBean > 0)  {
                GameObject newProjectile = Instantiate(slingshotFireGO, projectilePoint.position, projectilePoint.rotation);
                newProjectile.GetComponent<Rigidbody2D>().velocity = slingshotPoint.transform.right * projectileForce;
                GameObject.Find("Player").GetComponent<PlayerStats>().removeFire(1);
            }
        }
        if(currentProjectile == "ice")
        {
            if(GameObject.Find("Player").GetComponent<PlayerStats>().iceBean > 0)  {
                GameObject newProjectile = Instantiate(slingshotIceGO, projectilePoint.position, projectilePoint.rotation);
                newProjectile.GetComponent<Rigidbody2D>().velocity = slingshotPoint.transform.right * projectileForce;
                GameObject.Find("Player").GetComponent<PlayerStats>().removeIce(1);
            }
        }
        if(currentProjectile == "mind")
        {
            if(GameObject.Find("Player").GetComponent<PlayerStats>().mindBean > 0)  {
                GameObject newProjectile = Instantiate(slingshotMindGO, projectilePoint.position, projectilePoint.rotation);
                newProjectile.GetComponent<Rigidbody2D>().velocity = slingshotPoint.transform.right * projectileForce;
                GameObject.Find("Player").GetComponent<PlayerStats>().removeMind(1);
            }
        }
    }



    Vector2 PointPosition(float t)
    {
        Vector2 pos = (Vector2)projectilePoint.transform.position + (direction.normalized * projectileForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return pos;
    }


#endregion

#region weapon wheel

    public void weaponWheel() {
        GetClosestSlot();
        if(Input.GetKey(KeyCode.Q)) {
            weaponWheelGO.SetActive(true);
            usingWeaponWheel = true;
        } else {
            weaponWheelGO.SetActive(false);
            usingWeaponWheel = false;   
        }
    }

    public void changeProjectile(int slot) {
        currentProjectile = projectiles[slot];
    }


    public void GetClosestSlot() {
        if(usingWeaponWheel) {
            smallestDistance = Mathf.Infinity;
            for(int i = 0; i < weaponWheelSlots.Count; i++ ) {
                float distance = Vector2.Distance(Input.mousePosition, weaponWheelSlots[i].GetComponent<RectTransform>().transform.position);
                weaponWheelSlotsDistance[i] = distance;
                if(distance < smallestDistance) {
                    smallestDistance = distance;
                    weaponWheelselected = i;
                }
            }

            if (weaponWheelselected == 0)
                currentProjectile = "normal";
            if (weaponWheelselected == 1)
                currentProjectile = "platform";
            if (weaponWheelselected == 2)
                currentProjectile = "fire";
            if (weaponWheelselected == 3)
                currentProjectile = "ice";
            if (weaponWheelselected == 4)
                currentProjectile = "mind";
            weaponWheelText.text = currentProjectile;
        }
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

        if (value = isDodging)
        {
            isDodging = false;
        }
    }
}