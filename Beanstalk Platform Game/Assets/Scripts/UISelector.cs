using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelector : MonoBehaviour
{
    [SerializeField]    
    private int currentSelected;

    [SerializeField] private List<GameObject> ButtonsList;
    [SerializeField] private GameObject selectedUIAnimation;

    private bool hasSwitched;

    void Start()
    {
        currentSelected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") < 0 && !hasSwitched)
        {
            hasSwitched = true;
            if(currentSelected != ButtonsList.Count - 1)
            {
                currentSelected++;
            }
        }
        if (Input.GetAxisRaw("Vertical") > 0 && !hasSwitched)
        {
            hasSwitched = true;
            if (currentSelected != 0)
            {
                currentSelected--;
            }
        }
        if (Input.GetAxisRaw("Vertical") == 0 && hasSwitched)
        {
            hasSwitched = false;
        }


        for (int i = 0; i < ButtonsList.Count; i++)
        {
            if (i == currentSelected)
            {
                ButtonsList[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                ButtonsList[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color(255, 255, 255, 255);
                selectedUIAnimation.transform.position = ButtonsList[i].transform.position;
            } else
            {
                ButtonsList[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                ButtonsList[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 255);
            }
        }
    }
}
