using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_TeleportDoor : MonoBehaviour
{
    public bool canInteract;

    public GameObject OtherDoor;

    public void Start() {
        foreach(Transform child in transform)
                child.gameObject.SetActive(false);
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.E) && canInteract) {
            StartCoroutine(interact());
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            foreach(Transform child in transform)
                child.gameObject.SetActive(true);

            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            foreach(Transform child in transform)
                child.gameObject.SetActive(false);

            canInteract = false;
        }
    }

    IEnumerator interact() {
        Debug.Log("Interacted with door");
        GameObject.Find("GameManager").GetComponent<FadeScreen>().FadeInOut();
        yield return new WaitForSeconds(.65f);
        GameObject.Find("Player").transform.position = OtherDoor.transform.position + new Vector3(0, -0.25f, 0);
    }
}
