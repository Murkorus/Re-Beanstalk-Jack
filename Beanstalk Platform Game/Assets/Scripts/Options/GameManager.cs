using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public UIManager UIM;

    public bool isPaused;
    public bool optionsIsOpened;

    public GameObject UIAudioSource;
    public AudioClip TestAudio;


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !optionsIsOpened)
        {
            if(isPaused)
            {
                UnpauseGame();
            } else
            {
                PauseGame();
            }
        } else if(Input.GetKeyDown(KeyCode.Escape) && optionsIsOpened)
        {
            PauseGame();
        }
    }

    //Pause ui
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;

        optionsIsOpened = false;
        UIM.CloseOptionsMenu();
        UIM.OpenPauseMenu();
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        UIM.ClosePauseMenu();
    }


    public void testAudioFunction()
    {
        GameObject TestGO = Instantiate(UIAudioSource, transform.position, Quaternion.identity);
        TestGO.GetComponent<AudioSource>().pitch = Random.Range(0.90f, 1.10f);
        TestGO.GetComponent<AudioSource>().Play();
        Destroy(TestGO, 3);
    }


    public void OpenOptionsMenu()
    {
        isPaused = false;
        optionsIsOpened = true;
        Time.timeScale = 0;

        UIM.OpenOptionsMenu();
    }

    public void CloseOptionsMenu()
    {
        optionsIsOpened = false;
        PauseGame();
    }


    //Quit game
    public void ExitGame()
    {
        Application.Quit();
    }
}
