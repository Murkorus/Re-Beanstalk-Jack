using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    //Options
    public bool optionsIsOpen;
    [SerializeField] private GameObject optionsMenu;

    //Resolution
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    int currentResolutionIndex;
    List<string> options = new List<string>();

    [SerializeField] private GameObject pauseMenu;


    void Start()
    {

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }



    public void updateResolution()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, true);
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
