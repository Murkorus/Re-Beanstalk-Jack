using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponWheel : MonoBehaviour
{
    [Header("Weapon wheel")]
    [SerializeField] private int weaponWheelselected;
    [SerializeField] private List<string> projectiles;
    [SerializeField] private List<GameObject> weaponWheelSlots;
    [SerializeField] private List<float> weaponWheelSlotsDistance;
    [SerializeField] private GameObject weaponWheelGO;
    private float smallestDistance;
    private bool usingWeaponWheel;
    private TextMeshProUGUI weaponWheelText;
    private PlayerCombatController playerCombatController;
    private PlayerStats playerStats;


    void Start()
    {
        weaponWheelText = GameObject.Find("SelectedInWeaponWheel").GetComponent<TextMeshProUGUI>();
        playerCombatController = GameObject.Find("Player").GetComponent<PlayerCombatController>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        weaponWheel();
    }




    public void weaponWheel() {

        //Pebble
        if(playerStats.pebbles > 0)
        {
            weaponWheelSlots[0].GetComponent<Image>().color = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 255 / 255.0f); ;
        }
        else
        {
            weaponWheelSlots[0].GetComponent<Image>().color = new Color(75 / 255.0f, 75 / 255.0f, 75 / 255.0f, 255 / 255.0f); ;
        }
        //Platform bean
        if (playerStats.platformBeans > 0)
        {
            weaponWheelSlots[1].GetComponent<Image>().color = new Color(111 / 255.0f, 231 / 255.0f, 62 / 255.0f, 255 / 255.0f);
        }
        else
        {
            weaponWheelSlots[1].GetComponent<Image>().color = new Color(63 / 255.0f, 101 / 255.0f, 46 / 255.0f, 255 / 255.0f);
        }

        //Fire
        if (playerStats.fireBean > 0)
        {
            weaponWheelSlots[2].GetComponent<Image>().color = new Color(255 / 255.0f, 80 / 255.0f, 32 / 255.0f, 255 / 255.0f); ;
        }
        else
        {
            weaponWheelSlots[2].GetComponent<Image>().color = new Color(106 / 255.0f, 51 / 255.0f, 36 / 255.0f, 255 / 255.0f); ;
        }
        GetClosestSlot();
        if(Input.GetKey(KeyCode.Q)) {
            weaponWheelGO.SetActive(true);
            usingWeaponWheel = true;
        } else {
            weaponWheelGO.SetActive(false);
            usingWeaponWheel = false;   
        }
    }

    public void changeProjectile(int slot) {
        playerCombatController.currentProjectile = projectiles[slot];
    }


    public void GetClosestSlot() {
        if(usingWeaponWheel) {
            smallestDistance = Mathf.Infinity;
            for(int i = 0; i < weaponWheelSlots.Count; i++ ) {
                float distance = Vector2.Distance(Input.mousePosition, weaponWheelSlots[i].GetComponent<RectTransform>().transform.position);
                weaponWheelSlotsDistance[i] = distance;
                if(distance < smallestDistance) {
                    smallestDistance = distance;
                    weaponWheelselected = i;
                }
            }

            if (weaponWheelselected == 0)
                playerCombatController.currentProjectile = "normal";
            if (weaponWheelselected == 1)
                playerCombatController.currentProjectile = "platform";
            if (weaponWheelselected == 2)
                playerCombatController.currentProjectile = "fire";
            if (weaponWheelselected == 3)
                playerCombatController.currentProjectile = "ice";
            if (weaponWheelselected == 4)
                playerCombatController.currentProjectile = "mind";
            weaponWheelText.text = playerCombatController.currentProjectile;
        }
    }
}
