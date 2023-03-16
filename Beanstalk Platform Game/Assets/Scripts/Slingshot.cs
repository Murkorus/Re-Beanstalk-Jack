using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Slingshot : MonoBehaviour
{
    public int Stones;
    public int maxStones;

    public GameObject Player;
    public GameObject Pebble;
    public GameObject PlatformBean;
    public float projectileForce;
    public Transform projectilePoint;

    public string currentProjectile;

    Vector2 direction;


    private bool isAttacking;
    public bool isCharging;
    public bool isWalking;
    public bool carryingGoose;

    public bool canShoot;

    [SerializeField] private float chargeTime;
    private float totalTime;

    private GameObject AM;
    private GameObject GM;


    //UI


    //Trajectory
    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;


    void Start()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, projectilePoint.position, Quaternion.identity);
            points[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //isAttacking = GameObject.FindGameObjectWithTag("Player").GetComponent<playerAttack>().isAttacking;

        //UI
        //stoneText.text = "Stones: " + Stones;


        transform.position = Player.transform.position;
        //Slingshot rotation
        Vector2 slingshotPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - slingshotPos;
        transform.right = direction;



        //Slingshot charge
        if (Stones > 0)
        {
            if (!isAttacking)
                {
                    if (Input.GetMouseButton(1))
                    {
                        isCharging = true;
                        //AM.GetComponent<AnimationManager>().isCharging = true;
                        if (currentProjectile == "normal")
                        {
                            chargeTime += Time.deltaTime;
                            projectileForce = chargeTime * 5f + 2;
                            projectileForce = Mathf.Clamp(projectileForce, 2, 10);
                        }
                        if(currentProjectile == "platform")
                        {
                            chargeTime += Time.deltaTime;   
                            projectileForce = chargeTime * 1.5f + 2;
                            projectileForce = Mathf.Clamp(projectileForce, 2, 4);
                        }
                    }
                    if (Input.GetMouseButtonUp(1))
                    {
                        isCharging = false;
                        Shoot();
                        chargeTime = 0.0f;
                        projectileForce = chargeTime;
                    }
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

            if(!isCharging) {
                points[i].gameObject.SetActive(false);
                projectileForce = 0;
                chargeTime = 0;
            }
        }
    }

    public void Shoot()
    {
        if (currentProjectile == "normal")
        {
            GameObject newProjectile = Instantiate(Pebble, projectilePoint.position, projectilePoint.rotation);
            newProjectile.GetComponent<Rigidbody2D>().velocity = transform.right * projectileForce;
            //newProjectile.GetComponent<Projectile>().force = projectileForce;
            Stones--;
        }
        if(currentProjectile == "platform")
        {
            GameObject newProjectile = Instantiate(PlatformBean, projectilePoint.position, projectilePoint.rotation);
            newProjectile.GetComponent<Rigidbody2D>().velocity = transform.right * projectileForce;
            Stones--;
        }
    }



    Vector2 PointPosition(float t)
    {
        Vector2 pos = (Vector2)projectilePoint.transform.position + (direction.normalized * projectileForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return pos;
    }
}