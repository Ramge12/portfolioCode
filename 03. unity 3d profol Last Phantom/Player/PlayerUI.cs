using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour {

    [SerializeField] private Text skill_1_Button;
    [SerializeField] private Text skill_2_Button;
    [SerializeField] private Text skill_3_Button;
    [SerializeField] private GameObject Inventory;
    [SerializeField] private GameObject DronView;

    public void ChangeCharactorSkillButton(PlayerCharactor chractor)
    {
        switch(chractor)
        {
            case PlayerCharactor.Kohaku_mode:
                skill_1_Button.text = "Size\nDown";
                skill_2_Button.text = "Spide\nMode";
                skill_3_Button.text = "Special\nSkill";
                break;
            case PlayerCharactor.Azaha_mode:
                skill_1_Button.text = "Air\nWalkig";
                skill_2_Button.text = "Portal";
                skill_3_Button.text = "Magic\nShiled";
                break;
        }
    }

    public void SkillButton_2_OnOff()
    {
        Button skillButton = skill_2_Button.transform.parent.GetComponent<Button>();
        if (skillButton.interactable)
        {
            skillButton.interactable = false;
        }
        else
        {
            skillButton.interactable = true;
        }
    }

    public void SkillButton_1_OnOff()
    {
        Button skillButton = skill_1_Button.transform.parent.GetComponent<Button>();
        if (skillButton.interactable)
        {
            skillButton.interactable = false;
        }
        else
        {
            skillButton.interactable = true;
        }
    }

    public void InventoryOnOff()
    {
        if(Inventory.activeSelf)
        {
            Inventory.SetActive(false);
        }
        else
        {
            Inventory.SetActive(true);
        }
    }

    public void DroneViewOnOff()
    {
        if (DronView.activeSelf)
        {
            DronView.SetActive(false);
        }
        else
        {
            DronView.SetActive(true);
        }
    }
}
