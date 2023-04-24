using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float time;
    void Start()
    {
        StartCoroutine(Timer(time));
    }

    IEnumerator Timer(float time) {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
