using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KohakuAnimation : MonoBehaviour {

    public bool kohakuBattle;

    private bool playerOneAttack;
    private float idleValue = 0;
    private float attackCount = 0;
    private Animator kohakuAnimation;
    private IEnumerator weaponCoroutine;

    [SerializeField] WeaponValue weaponValue;

    void Start () {
        kohakuAnimation = GetComponent<Animator>();
        weaponCoroutine = WeaponColliderActive();
    }
	
    public void BattleChange() {
        StartCoroutine(Battle_IdleCheck());
    }

    IEnumerator Battle_IdleCheck()
    {
        while (true)
        {
            if (kohakuBattle && idleValue < 1f)
            {
                idleValue += Time.deltaTime;
                if(idleValue>1)
                {
                    idleValue = 1;
                    break;
                }
                kohakuAnimation.SetFloat("IdleValue", idleValue);
            }
            else if (!kohakuBattle && idleValue > 0f)
            {
                idleValue -= Time.deltaTime;
                if (idleValue < 0)
                {
                    idleValue = 0;
                    break;
                }
                kohakuAnimation.SetFloat("IdleValue", idleValue);
            }
            yield return new WaitForSeconds(Time.deltaTime*0.3f);
        }
        yield return null;
    }

    public bool AnimationEndCheck()
    {
        if (kohakuAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            if (kohakuAnimation.GetCurrentAnimatorStateInfo(0).IsName("Attack_1") && attackCount == 0.5)
            {
                kohakuAnimation.SetBool("Attack", false);
                attackCount = 0;
                playerOneAttack = false;
                weaponCoroutine = weaponCoroutine = WeaponColliderActive();
                StopCoroutine(weaponCoroutine);
                return false;
            }
            if (kohakuAnimation.GetCurrentAnimatorStateInfo(0).IsName("Attack_2") && attackCount == 1.0)
            {
                kohakuAnimation.SetBool("Attack", false);
                attackCount = 0;
                playerOneAttack = false;
                weaponCoroutine = weaponCoroutine = WeaponColliderActive();
                StopCoroutine(weaponCoroutine);
                return false;
            }
            if (kohakuAnimation.GetCurrentAnimatorStateInfo(0).IsName("Attack_3") && attackCount == 1.5)
            {
                kohakuAnimation.SetBool("Attack", false);
                attackCount = 0;
                playerOneAttack = false;
                weaponCoroutine = weaponCoroutine = WeaponColliderActive();
                StopCoroutine(weaponCoroutine);
                return false;
            }
            if (kohakuAnimation.GetCurrentAnimatorStateInfo(0).IsName("SlideStep"))
            {
                kohakuAnimation.SetBool("SlideStep", false);
                return false;
            }
            if (kohakuAnimation.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
            {
                kohakuAnimation.SetBool("SpecialAttack", false);
                return false;
            }
            if (kohakuAnimation.GetCurrentAnimatorStateInfo(0).IsName("WallCliming"))
            {
                kohakuAnimation.SetBool("Climing", false);
                return false;
            }
            if (kohakuAnimation.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
            {
                kohakuAnimation.SetBool("Hurt", false);
                return false;
            }
        }
        return true;
    }

    public void AnimationCheck(PlayerState m_Animation)
    {
        switch (m_Animation)
        {
            case PlayerState.Player_Idle:
                kohakuAnimation.SetBool("Run", false);
                kohakuAnimation.SetBool("DoubleJump", false);
                kohakuAnimation.SetBool("Jump", false);
                kohakuAnimation.SetBool("Swing", false);
                kohakuAnimation.SetBool("Fall", false);
                kohakuAnimation.SetBool("Ride", false);
                kohakuAnimation.SetBool("Climing", false);
                kohakuAnimation.SetBool("Hang", false);
                kohakuAnimation.SetBool("JumpAttack", false);
                break;
            case PlayerState.Player_Run:
                kohakuAnimation.SetBool("Jump", false);
                kohakuAnimation.SetBool("DoubleJump", false);
                kohakuAnimation.SetBool("Run", true);
                kohakuAnimation.SetFloat("PosX", Input.GetAxisRaw("Horizontal"));
                kohakuAnimation.SetFloat("PosY", Input.GetAxisRaw("Vertical"));
                kohakuAnimation.SetBool("JumpAttack", false);
                break;
            case PlayerState.Player_Jump:
                kohakuAnimation.SetBool("Jump", true);
                break;
            case PlayerState.Player_DoubleJump:
                kohakuAnimation.SetBool("DoubleJump", true);
                break;
            case PlayerState.Player_Attack:
                if (attackCount <= 1.0f)
                {
                    kohakuAnimation.SetBool("Attack", true);
                    kohakuAnimation.SetFloat("AttackValue", attackCount);
                    attackCount +=0.5f;
                }
                if (!playerOneAttack)
                {
                    playerOneAttack = true;
                    StartCoroutine(StartAnimationSound());
                }
                weaponCoroutine = weaponCoroutine = WeaponColliderActive();
                StartCoroutine(weaponCoroutine);
                break;
            case PlayerState.Player_SlideStep:
                kohakuAnimation.SetBool("SlideStep", true);
                break;
            case PlayerState.Player_SpecialAttack:
                weaponCoroutine = weaponCoroutine = WeaponColliderActive();
                StartCoroutine(weaponCoroutine);
                kohakuAnimation.SetBool("SpecialAttack", true);
                PlayerSound.playSoundManagerCall().PlayAudio("specialAttack", false, 0.7f);
                break;
            case PlayerState.Player_Swing:
                kohakuAnimation.SetBool("Swing", true);
                break;
            case PlayerState.Player_Fall:
                kohakuAnimation.SetBool("Hang", false);
                kohakuAnimation.SetBool("Fall", true);
                kohakuAnimation.SetBool("Swing", false);
                kohakuAnimation.SetBool("Climing", false);
                break;
            case PlayerState.Player_Ride:
                kohakuAnimation.SetBool("Ride", true);
                break;
            case PlayerState.Player_Climing:
                kohakuAnimation.SetBool("Climing", true);
                break;
            case PlayerState.Player_Hang:
                kohakuAnimation.SetBool("Hang", true);
                kohakuAnimation.SetFloat("PosX", Input.GetAxisRaw("Horizontal"));
                break;
            case PlayerState.Player_JumpAttack:
                weaponCoroutine = weaponCoroutine = WeaponColliderActive();
                StartCoroutine(weaponCoroutine);
                kohakuAnimation.SetBool("JumpAttack", true);
                break;
            case PlayerState.Player_Hurt:
                kohakuAnimation.SetBool("Hurt", true);
                break;
            case PlayerState.Player_Death:
                kohakuAnimation.SetBool("Death", true);
                break;
        }
    }

    IEnumerator WeaponColliderActive()
    {
        while (true)
        {
            float animationTime = kohakuAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (animationTime >= 0.1f && animationTime <= 0.7f)
            {
                weaponValue.WeaponAttackStart();
            }
            else if (animationTime >= 0.7f && animationTime<=0.9f)
            {
                weaponValue.WeaponAttackEnd();
            }
            else if (animationTime >= 0.9f)
            {
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime * 0.1f);
        }
        yield return null;
    }

    IEnumerator StartAnimationSound()
    {
        bool m_one=true;
        bool m_two= false;
        bool m_three= false;

        while (playerOneAttack)
        {
            float animationTime = kohakuAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (animationTime >= 0.1f && animationTime <= 0.7f&& m_one && kohakuAnimation.GetCurrentAnimatorStateInfo(0).IsName("Attack_1"))
            {
                m_one = false;
                m_two = true;
                PlayerSound.playSoundManagerCall().PlayAudio("attackSound",false,0);
            }
            if (animationTime >= 0.3f && animationTime <= 0.7f && m_two && kohakuAnimation.GetCurrentAnimatorStateInfo(0).IsName("Attack_2"))
            {
                m_two = false;
                m_three = true;
                PlayerSound.playSoundManagerCall().PlayAudio("attackSound",false,0);
            }
            if (animationTime >= 0.3f && animationTime <= 0.7f && m_three && kohakuAnimation.GetCurrentAnimatorStateInfo(0).IsName("Attack_3"))
            {
                m_three = false;
                PlayerSound.playSoundManagerCall().PlayAudio("attackSound",false,0);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}
