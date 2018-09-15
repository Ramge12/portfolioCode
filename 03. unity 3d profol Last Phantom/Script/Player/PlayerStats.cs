using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour{

    [Header("-Player Mode")]
    public PlayerState playerState = PlayerState.Player_Idle;
    public PlayerCharactor playerCharactor = PlayerCharactor.Kohaku_mode;

    [Header("-Player Script")]
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private PlayerUseItem playerUseItem;
    [SerializeField] private AzahaController azahaControl;
    [SerializeField] private KohakuController kohakuControl;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private SaveLoad saveLoad;
    [SerializeField] private SimpleHealthBar healthBar;

    [Header("-Player Value")]
    [SerializeField] private CharactorStatistics playerStatistics = new CharactorStatistics();

    [Header("-Player Icon")]
    [SerializeField] private GameObject kohakuUI;
    [SerializeField] private GameObject azahaUI;

    [System.NonSerialized] public bool playerBattle = false;
    [System.NonSerialized] public bool playerDeath = false;

    public void Awake()
    {
        playerStatistics.hpMax = playerStatistics.HP;
        playerUI.ChangeCharactorSkillButton(playerCharactor);

        healthBar.transform.GetChild(0).GetComponent<Text>().text = playerStatistics.HP.ToString() + "/" + playerStatistics.hpMax.ToString();
        Load();
    }

    public void CurPlayerStatus(PlayerState playerCurStatus)
    {
        if (playerCharactor == PlayerCharactor.Kohaku_mode)
        {
            PlayerState preStautus = playerState;
            playerState = playerCurStatus;

            switch (playerCurStatus)
            {
                case PlayerState.Player_Attack:
                    StartCoroutine(AnimationEnd());
                    break;

                case PlayerState.Player_SlideStep:
                    StartCoroutine(AnimationEnd());
                    break;

                case PlayerState.Player_SpecialAttack:
                    if (kohakuControl.getSpecialAvailable())
                    {
                        StartCoroutine(AnimationEnd());
                    }
                    else
                    {
                        playerState = PlayerState.Player_Idle;
                    }
                    break;

                case PlayerState.Player_Hurt:
                    if(preStautus!= PlayerState.Player_Hurt)
                        StartCoroutine(AnimationEnd());
                    break;
            }
            kohakuControl.AnimationCheck(playerState);
        }
        else //AzahaMode
        {
            switch (playerCurStatus)
            {
                case PlayerState.Player_Jump:
                    if (azahaControl.getAzahaFlying() && azahaControl.getAzahaFlyEnable())
                    {   //비행이 가능한 상태이고 스킬이 활성화 되어있으때만
                        azahaControl.AzahaSkillFly();
                        CurPlayerStatus(PlayerState.Player_Fly);
                    }
                    else
                    {
                        playerState = PlayerState.Player_Jump;
                        azahaControl.AnimationCheck(playerState);
                    }
                    break;

                case PlayerState.Player_Attack:
                    if (!azahaControl.getSkillPortal() && !azahaControl.getDelay() && !azahaControl.getReload()&& !Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        playerState = PlayerState.Player_Attack;
                        azahaControl.AnimationCheck(playerState);
                        azahaControl.AttackAzaha();
                        StartCoroutine(Waiting());
                    }
                    break;

                case PlayerState.Player_SlideStep:
                    playerState = PlayerState.Player_SlideStep;
                    azahaControl.AnimationCheck(playerState);
                    StartCoroutine(AnimationEnd());
                    break;

                case PlayerState.Player_SpecialAttack:
                    playerState = PlayerState.Player_SpecialAttack;
                    azahaControl.AnimationCheck(playerState);
                    StartCoroutine(AnimationEnd());
                    break;

                default:
                    playerState = playerCurStatus;
                    azahaControl.AnimationCheck(playerState);
                    break;
            }
        }
    }

    IEnumerator Waiting()
    {
        azahaControl.setDelay(true);
        yield return new WaitForSeconds(0.5f);
        CurPlayerStatus(PlayerState.Player_Idle);
        azahaControl.setDelay(false);
        yield return null;
    }

    IEnumerator AnimationEnd()
    {
        while (true)
        {
            if (playerCharactor == PlayerCharactor.Kohaku_mode)
            {
                if (!kohakuControl.AnimationEndCheck())break;
            }
            else
            {
                if (!azahaControl.AnimationEndCheck())break;
            }
            yield return new WaitForSeconds(Time.deltaTime*0.1f);
        }
        CurPlayerStatus(PlayerState.Player_Idle);
        yield return null;
    }

    public void SpecialAttack()
    {
        if (playerCharactor == PlayerCharactor.Kohaku_mode)
        {
            kohakuControl.KohakuSpecialSkill();
        }
        else
        {
            azahaControl.AzahaSpeialSkill();
        }
    }

    public void BattleWeaponCheck()
    {
        switch (playerCharactor)
        {
            case PlayerCharactor.Kohaku_mode:
                kohakuControl.BattleWeaponCheck(playerBattle);
                break;
            case PlayerCharactor.Azaha_mode:
                azahaControl.BattleWeaponCheck(playerBattle);
                break;
        }
    }

    public void ChractorChange()
    {
        switch (playerCharactor)
        {
            case PlayerCharactor.Kohaku_mode:
                kohakuUI.SetActive(true);
                azahaUI.SetActive(false);
                kohakuControl.gameObject.SetActive(true);
                azahaControl.gameObject.SetActive(false);
                break;
            case PlayerCharactor.Azaha_mode:
                kohakuUI.SetActive(false);
                azahaUI.SetActive(true);
                azahaControl.gameObject.SetActive(true);
                kohakuControl.gameObject.SetActive(false);
                break;
        }
        PlayerSound.playSoundManagerCall().PlayAudio("playerChange", true, 0);
        playerUI.ChangeCharactorSkillButton(playerCharactor);
    }

    public void PlayerSkill(int skillNum)
    {
        if (skillNum == 1)
        {
            if (playerCharactor == PlayerCharactor.Kohaku_mode) kohakuControl.KohakuSkillSizeDown(cameraController);
            else azahaControl.setAzahaFlying();
        }
        if (skillNum == 2)
        {
            if (playerCharactor == PlayerCharactor.Kohaku_mode) kohakuControl.KohakuSkillWeb();
            else azahaControl.AzahaSkillPortal();
        }
    }

    public void AddPlayerHealthPoint(int hurtDamage,int caseHeal)
    {
        if (caseHeal == 0)
        {
            cameraController.StopAllCoroutines();
            cameraController.StartCoroutine(cameraController.ShakeCamera(0.1f, 0.05f));
            playerStatistics.AddCharactorHealthPoint(hurtDamage);

            if (playerStatistics.HP > 0)
            {
                CurPlayerStatus(PlayerState.Player_Hurt);
            }
            else if(playerStatistics.HP<=0)
            {
                playerStatistics.HP = 0;
                playerDeath = true;
                CurPlayerStatus(PlayerState.Player_Death);
                StartCoroutine(PlayerDeath());
            }
        }
        else if (caseHeal==1)
        {
            playerStatistics.DrinkPotion(hurtDamage);
        }
        healthBar.UpdateBar(playerStatistics.HP, playerStatistics.hpMax);
        healthBar.transform.GetChild(0).GetComponent<Text>().text = playerStatistics.HP.ToString() + "/" + playerStatistics.hpMax.ToString();
    }

    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(3.5f);
        GameManager.gManagerCall().ThisSceneReLoad();
        yield return null;
    }

    public int PlayerAttackDamage()
    {
        return playerStatistics.Damage;
    }

    public void UseItem(int number)
    {
        playerUseItem.UseItem(number);
    }

    public void Save()
    {
        saveLoad.PlayerSaveData(playerStatistics);
    }

    public void Load()
    {
        if (saveLoad.PlayerLoadData() != null) playerStatistics = saveLoad.PlayerLoadData();
        healthBar.UpdateBar(playerStatistics.HP, playerStatistics.hpMax);
        healthBar.transform.GetChild(0).GetComponent<Text>().text = playerStatistics.HP.ToString() + "/" + playerStatistics.hpMax.ToString();
    }

    public void ResetHP()
    {
        saveLoad.PlayerSaveData(playerStatistics);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Breath"))
        {
            AddPlayerHealthPoint(-15, 0);
        }
    }
}
