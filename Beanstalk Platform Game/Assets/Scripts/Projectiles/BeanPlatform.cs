using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeanPlatform : MonoBehaviour
{
    public float PlatformTime;
    public float PlatformTimer;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        float playerSide = GameObject.Find("Player").transform.position.x - transform.position.x;
        Debug.Log(playerSide);
        if(playerSide < 0) {
            GetComponent<Transform>().transform.localScale = new Vector3(1, 1, 1);
        }
        if(playerSide > 0) {
            GetComponent<Transform>().transform.localScale = new Vector3(-1, 1, 1);

        }
    }

    // Update is called once per frame
    void Update()
    {
        PlatformTimer += Time.deltaTime;
        if (PlatformTimer > PlatformTime)
        {
            anim.Play("PlatformReverseGrow");
            Debug.Log("Platform got destroyed");
        }
    }


    public void DestroyPlatform()
    {
        Destroy(this.gameObject);
    }

    public void playSound() {
        GetComponentInChildren<AudioSource>().Play();
    }
}
