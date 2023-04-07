using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelector : MonoBehaviour
{
    [SerializeField]    
    private int currentSelected;

    [SerializeField] private List<GameObject> ButtonsList;
    [SerializeField] private GameObject selectedUIAnimation;

    [SerializeField] private Color selectedColor;
    [SerializeField] private Color normalColor;

    private bool hasSwitched;
    [SerializeField] private bool isUsingMouse;

    [SerializeField] UIManager uimanager;

    void Start()
    {
        currentSelected = 0;
        updateUi();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isUsingMouse)
        {
            if (Input.GetAxisRaw("Vertical") < 0 && !hasSwitched)
            {
                hasSwitched = true;
                if (currentSelected != ButtonsList.Count - 1)
                {
                    currentSelected++;
                    updateUi();
                }
            }
            if (Input.GetAxisRaw("Vertical") > 0 && !hasSwitched)
            {
                hasSwitched = true;
                if (currentSelected != 0)
                {
                    currentSelected--;
                    updateUi();
                }
            }
            if (Input.GetAxisRaw("Vertical") == 0 && hasSwitched)
            {
                hasSwitched = false;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                ButtonsList[currentSelected].GetComponent<Button>().onClick.Invoke();
                Debug.Log("Clicked button");
            }
        }
    }



    public void SwitchToMouse(int index)
    {
        isUsingMouse = true;
        currentSelected = index;
        updateUi();
        Debug.Log("Hovering mouse");
    }

    public void switchToKeyboard()
    {
        isUsingMouse = false;
    }

    public void updateUi()
    {

        uimanager.playUiHover();

        for (int i = 0; i < ButtonsList.Count; i++)
        {
            if (i == currentSelected)
            {
                ButtonsList[i].GetComponent<Image>().color = selectedColor;
                selectedUIAnimation.transform.position = ButtonsList[i].transform.position;
            }
            else
            {
                ButtonsList[i].GetComponent<Image>().color = normalColor;
            }
        }
    }
}
