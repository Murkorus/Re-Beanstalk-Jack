using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Food : MonoBehaviour
{
    public bool canInteract;
    public bool hasEaten;

    public Sprite foodSprite;
    public Sprite EmptySprite;

    public void Start()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract && !hasEaten)
        {
            interact();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hasEaten)
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
        hasEaten = true;
        Debug.Log("Eaten food");
        GetComponent<SpriteRenderer>().sprite = EmptySprite;
        GameObject.Find("Player").GetComponent<PlayerStats>().addHealth();
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
}
