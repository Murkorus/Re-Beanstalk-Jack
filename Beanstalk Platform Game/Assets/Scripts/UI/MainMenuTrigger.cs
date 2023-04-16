using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTrigger : MonoBehaviour
{
    public AudioSource MainMenuTriggerSource;

    public void triggerAudio()
    {
        MainMenuTriggerSource.Play();
    }
}
