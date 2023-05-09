using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public List<AudioClip> audioClips;


    public void triggerAudio(int index) {
        GetComponent<AudioSource>().clip = audioClips[index];
        GetComponent<AudioSource>().Play();
    }
}
