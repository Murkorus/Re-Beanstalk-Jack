using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkCloud : MonoBehaviour
{
    public bool IsFacingLeft;

    // Update is called once per frame
    void Update()
    {
        if(!IsFacingLeft)
            transform.position += new Vector3(-.25f, 0, 0) * Time.deltaTime;
        else
            transform.position += new Vector3(.25f, 0, 0) * Time.deltaTime;

    }
}
