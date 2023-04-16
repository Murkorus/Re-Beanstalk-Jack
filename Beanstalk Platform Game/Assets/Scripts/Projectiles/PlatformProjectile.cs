using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformProjectile : MonoBehaviour
{
    public LayerMask groundLayer;
    public GameObject platformPrefab;

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, .05f, groundLayer)) {
            Instantiate(platformPrefab, transform.position - new Vector3(0, 0.15f, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
