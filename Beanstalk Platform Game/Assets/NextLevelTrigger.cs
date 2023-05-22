using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public int sceneIndex;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Saved the game");
        SceneManager.LoadScene(sceneIndex);
    }
}
