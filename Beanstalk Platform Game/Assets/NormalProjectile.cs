using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : MonoBehaviour
{
    public LayerMask groundLayer;
    public GameObject platformPrefab;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, .45f, groundLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++) {
            if(enemiesToDamage[i].GetComponent<Enemy>()) {
                enemiesToDamage[i].GetComponent<Enemy>().damage(2);
            }
            Destroy(this.gameObject);
            Debug.Log("Hit enemy");
        }
    }
}
