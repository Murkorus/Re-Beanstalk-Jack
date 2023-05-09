    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    bool hasHit;

    private bool sleeping;
    private float fallTime;

    public float force;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if(transform.position.y < -100)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Giant")
        {
            Debug.Log("Hit Giant with " + force);
            if(force > 7)
            {
                //if(collision.GetComponent<GiantEnemy>())
                //{
                    //collision.GetComponent<GiantEnemy>().StaggerGiant();
                //}
                //collision.GetComponent<GiantThrowing>().StaggerGiant();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Giant")
        {
            //Debug.Log("Hit Giant");
        }
        if(collision.transform.tag != "Player" && collision.transform.tag  != "Target")
        {
            //rb.velocity = Vector3.zero;
            //rb.isKinematic = true;
            //Destroy(this.gameObject);
        }
    }
}
