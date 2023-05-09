using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinker : MonoBehaviour
{
    public GameObject stinkCloud;
    public bool isFacingLeft;
    void Start()
    {
        Stink();
        transform.parent = GameObject.Find("StinkClouds").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if(isFacingLeft)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            transform.position += new Vector3(4, 0, 0) * Time.deltaTime;
        } else
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            transform.position += new Vector3(-4, 0, 0) * Time.deltaTime;
        }
    }

    public void Stink()
    {
        GameObject stinkCloudGO = Instantiate(stinkCloud, transform);
        stinkCloudGO.transform.parent = GameObject.Find("StinkClouds").transform;
        stinkCloudGO.GetComponent<StinkCloud>().IsFacingLeft = isFacingLeft;
        Destroy(stinkCloudGO, 10f);
    }
}
