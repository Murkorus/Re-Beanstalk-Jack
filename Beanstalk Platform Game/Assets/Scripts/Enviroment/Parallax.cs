using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Camera Cam;
    public float parallax_value;

    Vector3 startposition;
    // Start is called before the first frame update
    void Start()
    {
        startposition = transform.position;
        Cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relative_pos = Cam.transform.position * parallax_value;
        relative_pos.z = startposition.z;
        relative_pos.y = startposition.y;
        transform.position = relative_pos;

    }
}