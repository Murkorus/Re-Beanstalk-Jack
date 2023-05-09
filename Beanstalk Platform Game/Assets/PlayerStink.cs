using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PlayerStink : MonoBehaviour
{

    public bool isInStinkCloud;
    public LayerMask stinkLayer;
    public float stinkLevel;

    private Volume volume;
    private ColorAdjustments colorAdjustments;
    public Color startColor = Color.white;
    public Color endColor = Color.green;
    private Color lerpedColor;

    public void Start()
    {
        //Post processing
        VolumeProfile profile = GameObject.Find("Global Volume").GetComponent<Volume>().profile;
        volume = GameObject.Find("Global Volume").GetComponent<Volume>();

        if (volume.profile.TryGet<ColorAdjustments>(out ColorAdjustments ca))
        {
            colorAdjustments = ca;
        }
        lerpedColor = Color.white;
    }

    public void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.25f, stinkLayer))
        {
            isInStinkCloud = true;
        } else
        {
            isInStinkCloud = false;
        }


        if(isInStinkCloud)
        {
            stinkLevel += 1 * Time.deltaTime;
            lerpedColor = Color.Lerp(lerpedColor, endColor, .25f * Time.deltaTime);

        }
        else if(!isInStinkCloud && stinkLevel > 0)
        {
            stinkLevel -= 1 * Time.deltaTime;
        }
        if(!isInStinkCloud && lerpedColor != Color.white)
            lerpedColor = Color.Lerp(lerpedColor, startColor, .4f * Time.deltaTime);

        if (stinkLevel < 0)
        {
            stinkLevel = 0;
        }


        if (stinkLevel > 5)
        {
            stinkLevel = 0;
            GetComponent<PlayerStats>().takeDamage();
        }
        StinkLevelOverlay();
    }


    public void StinkLevelOverlay()
    {
        colorAdjustments.colorFilter.value = lerpedColor;
    }
}
