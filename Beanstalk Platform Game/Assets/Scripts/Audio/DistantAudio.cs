using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantAudio : MonoBehaviour
{
    float distanceToPlayer;
    public float Loudness;
    public float audioDistanceThreshold;

    public bool fadeIn;
    public bool fadeOut;
    void Start()
    {
        audioFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
        if(!fadeIn || !fadeOut) {
            if(distanceToPlayer < audioDistanceThreshold)
                GetComponent<AudioSource>().volume = 0;
            else
                GetComponent<AudioSource>().volume = 1 / (distanceToPlayer / Loudness);
        }
        if(fadeOut) {
            audioFadeOut();
        }
    }


    public void audioFadeIn() {
        fadeIn = true;
        GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 1 / (distanceToPlayer / Loudness), 1 * Time.deltaTime);
    }

    public void audioFadeOut() {
            fadeOut = true;
            GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0, 1 * Time.deltaTime);
    }
}
