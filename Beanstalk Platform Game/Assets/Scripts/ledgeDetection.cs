using System;
using UnityEngine;

public class ledgeDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private GameObject wallcheck;
    [SerializeField] private bool wallDetected;

    private void Update()
    {
        player.wallDetected = Physics2D.OverlapBox(wallcheck.transform.position,
            wallcheck.transform.localScale, 0, groundMask);
        player.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius, groundMask);
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireCube(wallcheck.transform.position, wallcheck.transform.localScale);
    }
}
