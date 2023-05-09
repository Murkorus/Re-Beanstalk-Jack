using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponentInParent<GiantBoss>().PlayerDetected();
            gameObject.SetActive(false);
        }
    }
}
