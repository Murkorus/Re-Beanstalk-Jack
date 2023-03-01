using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject fadeInGO;
    public Color transparentColor;

    public GameObject CanvasGO;

    public Animator canvasAnim;
    public bool hasPlayedOpenAnimation;

    //Audio
    public AudioSource windSound;
    public float windVolume;

    public AudioSource GeeseSound;
    public float GeeseVolume;

    public bool playAmbience;


    #region FIELDS
    public Image fadeOutUIImage;
    public float fadeSpeed = 0.8f;




    public void Start()
    {
        CanvasGO.SetActive(false);

        windVolume = 0;
        windSound.volume = windVolume;

        GeeseVolume = 0;
        GeeseSound.volume = GeeseVolume;

        playAmbience = true;
    }
    public enum FadeDirection
    {
        In, //Alpha = 1
        Out // Alpha = 0
    }
    #endregion

    #region MONOBHEAVIOR
    void OnEnable()
    {
        StartCoroutine(Fade(FadeDirection.Out));
    }
    #endregion


    public void Update()
    {
        if(playAmbience)
        {
            windVolume = Mathf.Lerp(windVolume, 0.4f, .5f * Time.deltaTime);
            windSound.volume= windVolume;

            GeeseVolume = Mathf.Lerp(GeeseVolume, 0.025f, .5f * Time.deltaTime);
            GeeseSound.volume= GeeseVolume;
        }
    }


    #region FADE
    private IEnumerator Fade(FadeDirection fadeDirection)
    {
        float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;
        if (fadeDirection == FadeDirection.Out)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
            fadeOutUIImage.enabled = false;
        }
        else
        {
            fadeOutUIImage.enabled = true;
            while (alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
        }
        CanvasGO.SetActive(true);
    }
    #endregion

    #region HELPERS
    public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad)
    {
        yield return Fade(fadeDirection);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
    }
    #endregion
}
