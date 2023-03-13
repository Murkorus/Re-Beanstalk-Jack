using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompController : MonoBehaviour
{
    public int groundMaterial; // 1 = Dirt, 2 = Grass, 3 = Stone

    [Header("Colors")]
    [SerializeField] private Gradient DirtColor;
    [SerializeField] private Gradient GrassColor;
    [SerializeField] private Gradient StoneColor;

    [Header("stomp particles")]
    [SerializeField] private ParticleSystem stompParticles1;
    [SerializeField] private ParticleSystem stompParticles2;


    [Header("Stomp settings")]
    public bool isLeft;
    [SerializeField] private float speed;
    [SerializeField] private float damage;


    [Header("Collision Detection")]
    public GameObject wallDetection;
    public GameObject FloorDetection;

    public LayerMask detectionLayer;

    void Awake()
    {
        Color randColor;
        if(groundMaterial == 1) {
            randColor = DirtColor.Evaluate(Random.Range(0f, 1f));
            stompParticles1.startColor = randColor;
            stompParticles2.startColor = randColor;
        }
        
        if(groundMaterial == 2) {
            randColor = GrassColor.Evaluate(Random.Range(0f, 1f));
            stompParticles1.startColor = randColor;
            stompParticles2.startColor = randColor;
        }

        if(groundMaterial == 3) {
            randColor = StoneColor.Evaluate(Random.Range(0f, 1f));
            stompParticles1.startColor = randColor;
            stompParticles2.startColor = randColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeft) {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }


        //Collision detection
        if(Physics2D.OverlapCircle(wallDetection.transform.position, 0.1f, detectionLayer))
        {
            stompParticles1.Stop();
            stompParticles2.Stop();
            Destroy(this.gameObject, 0.25f);
        }
        if(Physics2D.OverlapCircle(FloorDetection.transform.position, 0.5f, detectionLayer))
        {
            stompParticles1.Stop();
            stompParticles2.Stop();
            Destroy(this.gameObject, 0.25f);
        }
    }
}
