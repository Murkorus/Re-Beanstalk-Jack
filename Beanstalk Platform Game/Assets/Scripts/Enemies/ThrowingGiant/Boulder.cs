using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boulder : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    private GameObject GM;

    private bool hitPlayer;

    void Start()
    {
        GM = GameObject.Find("GM");
    }


    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 1, playerLayer) && !hitPlayer)
        {
            hitPlayer = true;
            Debug.Log("Hit player");
            GameObject.Find("Player").GetComponent<PlayerStats>().takeDamage();
            Destroy(this.gameObject, 0.15f);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            Destroy(this.gameObject, 0.075f);
        }
    }
}
