using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private float health;
    public GameObject breakPrefab;
    public List<GameObject> drops;
    public int level;

    private GameObject breakSound;
    private Rigidbody2D rb;

    public bool breakOnGroundHit;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void breakObject() {
        Instantiate(breakPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        float distanceToPlayer = Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) / 10;
        GameObject.Find("CM vcam1").GetComponent<screenShake>().shake(0.125f, .15f / distanceToPlayer, .15f / distanceToPlayer);

        //Amount of drops
        for (int i = 0; i < Random.Range(4 ,4 * level); i++) {
            //Drop item based on level
            Vector3 offset = new Vector3(Random.Range(-0.0f, 0.50f), 0, 0);
            GameObject PickupGO = Instantiate(drops[Random.Range(0, level)], transform.position + offset, Quaternion.identity);  
            PickupGO.transform.parent = GameObject.Find("----- Pickup -----").GetComponent<Transform>();  
            offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(breakOnGroundHit)
        {
            if(collision.tag == "Stone")
                breakObject();
        }
    }


    private void Update() {
        if(health <= 0) {
            breakObject();
        }
    }

    public void damage(float amount) {
        health -= amount;
        if(health <= 0) {
            breakObject();
        }
    }
}
