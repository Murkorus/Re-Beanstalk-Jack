using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Player ui


    //Menues



    //Main Menu

    //Level Mnu


    //Pause menu
    #region Pause menu
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool pauseMenuOpened;

    public bool OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        return true;
    }

    public bool ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        return false;
    }
    #endregion


    //Options Menu
    #region Options menu
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private bool optionsMenuOpened;


    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        getResolution();
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }


    [SerializeField] private TMPro.TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    public void getResolution()
    {

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentRefreshRate = Screen.currentResolution.refreshRate;

        foreach (Resolution resolution in Screen.resolutions)
        {
            if (resolution.refreshRate == currentRefreshRate)
            {
                string option = resolution.width + " x " + resolution.height;
                options.Add(option);
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

    }

    public void updateResolution(int index)
    {
        List<Resolution> filteredResolutions = new List<Resolution>();
        int currentRefreshRate = Screen.currentResolution.refreshRate;

        foreach (Resolution resolution in Screen.resolutions)
        {
            if (resolution.refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolution);
            }
        }

        if (index >= 0 && index < filteredResolutions.Count)
        {
            Resolution resolution = filteredResolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
        }
    }
    #endregion
    //Save Menu



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (uiHoverPitch > 1)
        {
            uiHoverPitch -= 1 * Time.deltaTime;
            uiHoverPitch = Mathf.Clamp(uiHoverPitch, 1, 1.25f);
        }
    }


    #region uiHoverSound
    public GameObject HoverSound;
    public float uiHoverPitch = 1;
    public void playUiHover()
    {
        uiHoverPitch += 0.1f;
        GameObject hoverSoundGO = Instantiate(HoverSound);
        hoverSoundGO.GetComponent<AudioSource>().pitch = uiHoverPitch;
        hoverSoundGO.GetComponent<AudioSource>().Play();
        Destroy(hoverSoundGO, 1);
    }
    #endregion

    #region uiLoadSound
    public GameObject LoadSound;
    public void playUiLoad()
    {
        uiHoverPitch += 0.1f;
        GameObject hoverSoundGO = Instantiate(LoadSound);
        hoverSoundGO.GetComponent<AudioSource>().Play();
        Destroy(hoverSoundGO, 1);
    }
    #endregion
    public void updatePlayerUI()
    {

    }
}
