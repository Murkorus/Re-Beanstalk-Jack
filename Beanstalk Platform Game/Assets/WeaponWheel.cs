using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    void Start()
    {
        weaponWheelText = GameObject.Find("SelectedInWeaponWheel").GetComponent<TextMeshProUGUI>();
        playerCombatController = GameObject.Find("Player").GetComponent<PlayerCombatController>();
    }

    // Update is called once per frame
    void Update()
    {
        weaponWheel();
    }




    public void weaponWheel() {
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
