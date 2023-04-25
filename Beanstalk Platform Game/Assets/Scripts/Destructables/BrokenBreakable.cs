using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBreakable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyWait());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destroyWait() {
        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        Destroy(this.gameObject);
    }
}
