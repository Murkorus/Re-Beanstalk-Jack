using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class lightFade : MonoBehaviour
{

    //Light
    Light2D light;

    public bool fadeIn;
    public float fadeInSpeed;
    public bool fadeOut;
    public float fadeOutSpeed;


    

    public float brightness;

    void Start() {
        fadeIn = true;
        light = GetComponent<Light2D>();
    }
    void Update()
    {
        if(fadeIn && light.intensity == brightness || fadeOut)
            fadeIn = false;
        if(fadeIn) {
            lightFadeIn();
        }
        if(fadeOut) {
            lightFadeOut();
        }
    }

    public void lightFadeIn() {
        fadeIn = true;
        GetComponent<Light2D>().intensity = Mathf.Lerp(GetComponent<Light2D>().intensity, brightness, fadeInSpeed * Time.deltaTime);
    }

    public void lightFadeOut() {
            fadeOut = true;
            GetComponent<Light2D>().intensity = Mathf.Lerp(GetComponent<Light2D>().intensity, 0, fadeOutSpeed * Time.deltaTime);
    }
}
