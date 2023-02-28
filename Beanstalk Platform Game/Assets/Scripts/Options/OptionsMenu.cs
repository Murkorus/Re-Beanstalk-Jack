using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    //Options
    public bool optionsIsOpen;
    private GameObject optionsMenu;

    //Resolution
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    int currentResolutionIndex;
    List<string> options = new List<string>();

    private GameObject pauseMenu;


    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);

        optionsMenu = GameObject.Find("OptionsMenu");
        optionsMenu.SetActive(false);

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();


        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    //Options ui
    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void ExitOptionsMenu()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
