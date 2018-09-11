using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzahaController : MonoBehaviour {

    private AzahaSkillSystem azahaSkillSystem;
    [SerializeField] private AzahaAnimation azahaAnimation;
    [SerializeField] private PlayerUI playerUI;

    void Start () {
        azahaSkillSystem = GetComponent<AzahaSkillSystem>();
    }

    //====================== Animation ========================================
    public void AnimationCheck(PlayerState m_Animation)   
    {
        azahaAnimation.AnimationCheck(m_Animation);
    }

    public bool AnimationEndCheck()                         
    {
        return azahaAnimation.AnimationEndCheck();
    }

    //========================== WeaponSpawn =================================
    public void BattleWeaponCheck(bool Battle)              
    {
        azahaAnimation.azahaBattle = Battle;
        azahaSkillSystem.WeaponCheck(Battle);
        azahaAnimation.StartCoroutine(azahaAnimation.Battle_IdleCheck());
    }

    //========================== shoot attack =================================================
    public void AttackAzaha()                               //플레이어 공격
    {
        if (!azahaSkillSystem.reLoading) azahaSkillSystem.StartCoroutine(azahaSkillSystem.ShootSystem());
    }

    public void setDelay(bool setDelay)                        //총알 발사 사이 딜레이를 위한 불값
    {
        azahaSkillSystem.shootDelay = setDelay;
    }

    public bool getDelay()                                  //총알 발사 사이 딜레이를 위한 불값
    {
        return azahaSkillSystem.shootDelay;
    }

    public bool getReload()                                 //재장전 여부 화인
    {
        return azahaSkillSystem.reLoading;
    }

    //========================= Fly ======================================
    public void setAzahaFlying()                            //플레이어의 비행 스킬 활성화
    {
        if (azahaSkillSystem.flySkillOnOff)
        {
            azahaSkillSystem.flySkillOnOff = false;
        }
        else
        {
            azahaSkillSystem.flySkillOnOff = (true);
            azahaSkillSystem.flySkillOnOff=true;
        }

        playerUI.SkillButton_1_OnOff();
    }

    public bool getAzahaFlyEnable()                         //비행스킬이 활성화됐는지 확인하는 함수
    {
        return azahaSkillSystem.flySkillOnOff;
    }

    public void AzahaSkillFly()                             //비행 시작 버튼
    {//이미 비행중일떄는 실행하지 않도록 한다
        if (!azahaSkillSystem.curFlying) azahaSkillSystem.StartFlying(); 
    }

    public bool getAzahaFlying()                            //비행이 가능한지 확인하는 불값
    {
        return azahaSkillSystem.enableFly;
    }

    //==================== portal =========================================
    public void AzahaSkillPortal()                          //스킬중 포탈 모드를 켤지 말지 조정
    {
        if (azahaSkillSystem.potalSkillOnOff) azahaSkillSystem.potalSkillOnOff = false;
        else azahaSkillSystem.potalSkillOnOff = true;

        playerUI.SkillButton_2_OnOff();
    }

    public bool getSkillPortal()                            //포탈모드가 켜져있는지 확인하는 값
    {
        return azahaSkillSystem.potalSkillOnOff;
    }

    //--------------------------------------------------------------------------------------------
    public void AzahaSpeialSkill()
    {
        azahaSkillSystem.SpecialSkill();
    }
}
