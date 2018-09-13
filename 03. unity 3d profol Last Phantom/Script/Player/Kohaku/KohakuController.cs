using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KohakuController : MonoBehaviour
{
    private KohakuAnimation kohakuAnimation;
    private KohakuSkillSystem kohakuSkillSystem;
    private Transform controllerTransform;

    void Start()
    {
        controllerTransform = this.transform;
        kohakuAnimation = GetComponent<KohakuAnimation>();
        kohakuSkillSystem = GetComponent<KohakuSkillSystem>();
    }

    public void BattleWeaponCheck(bool Battle)
    {
        kohakuAnimation.kohakuBattle = Battle;
        kohakuAnimation.BattleChange();
        kohakuSkillSystem.WeaponCheck(Battle);
        if (Battle)
        {
            kohakuSkillSystem.enabled = (true);
        }
        else
        {
            Transform playerTransfrom = controllerTransform.parent;
            playerTransfrom.parent = null;
            kohakuSkillSystem.enabled = (false);
        }
    }

    public void AnimationCheck(PlayerState m_Animation)
    {
        kohakuAnimation.AnimationCheck(m_Animation);
    }

    public bool AnimationEndCheck()
    {
        return kohakuAnimation.AnimationEndCheck();
    }

    public void KohakuSkillSizeDown(CameraController cameraController)
    {
        kohakuSkillSystem.KohakuSize(cameraController);
    }

    public void KohakuSkillWeb()
    {
        kohakuSkillSystem.WebShootSystemOnOff();
    }

    public void KohakuSpecialSkill()
    {
        kohakuSkillSystem.StartCoroutine(kohakuSkillSystem.WeaponSpecialSkill());
    }

    public bool getSpecialAvailable()
    {
        return kohakuSkillSystem.skill_3_available;
    }
}
