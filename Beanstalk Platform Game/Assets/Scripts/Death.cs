using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public SaveSystem respawn;
    // Start is called before the first frame update
    void Start()
    {
        respawn = GameObject.Find("SaveSystem").GetComponent<SaveSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            respawn.Load(respawn.currentSave);
        }
    }
}
