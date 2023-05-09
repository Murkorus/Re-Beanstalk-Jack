using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponentInParent<Animator>().Play("BossDoorClose");
        }
        gameObject.SetActive(false);
    }
}
