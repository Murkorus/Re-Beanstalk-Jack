using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Camera cam;
    public float parallax_value;

    Vector3 startposition;
    // Start is called before the first frame update
    void Start()
    {
        startposition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relative_pos = cam.transform.position * parallax_value;
        relative_pos.z = startposition.z;
        transform.position = startposition + relative_pos;

    }
}