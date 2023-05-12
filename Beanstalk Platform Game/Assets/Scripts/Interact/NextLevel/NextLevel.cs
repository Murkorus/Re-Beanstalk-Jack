using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public bool canInteract;
    public int sceneIndex;

    public void Start()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            interact();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(true);

            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);

            canInteract = false;
        }
    }

    public void interact()
    {
        Debug.Log("Saved the game");
        SceneManager.LoadScene(sceneIndex);
    }
}
