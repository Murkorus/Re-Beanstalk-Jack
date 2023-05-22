using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    public TextMeshProUGUI amountText;
    public Image ammoPreviewImage;
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        updateAmmoDisplay();
    }

    public void updateAmmoDisplay()
    {
        if(GameObject.Find("Player").GetComponent<PlayerCombatController>().currentProjectile == "normal")
        {
            ammoPreviewImage.color = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
            amountText.text = GameObject.Find("Player").GetComponent<PlayerStats>().pebbles.ToString();
        }
        if (GameObject.Find("Player").GetComponent<PlayerCombatController>().currentProjectile == "platform")
        {
            ammoPreviewImage.color = new Color(111 / 255.0f, 231 / 255.0f, 62 / 255.0f, 255 / 255.0f);
            amountText.text = GameObject.Find("Player").GetComponent<PlayerStats>().platformBeans.ToString();

        }
        if (GameObject.Find("Player").GetComponent<PlayerCombatController>().currentProjectile == "fire")
        {
            ammoPreviewImage.GetComponent<Image>().color = new Color(255 / 255.0f, 80 / 255.0f, 32 / 255.0f, 255 / 255.0f);
            amountText.text = GameObject.Find("Player").GetComponent<PlayerStats>().fireBean.ToString();

        }
    }
}
