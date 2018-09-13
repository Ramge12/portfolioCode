using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzahaAnimation : MonoBehaviour {

    [SerializeField] private Animator azahaAnimation;
    public bool azahaBattle;
    private float idleValue = 0.0f;

    public void Run(bool Run, float PosX, float PosY)
    {
        azahaAnimation.SetBool("Run", Run);
        azahaAnimation.SetFloat("PosX", PosX);
        azahaAnimation.SetFloat("PosY", PosY);
    }

    public IEnumerator Battle_IdleCheck()
    {
        while (true)
        {
            if (azahaBattle && idleValue < 1f)
            {
                idleValue += Time.deltaTime;
                if (idleValue > 1)
                {
                    idleValue = 1;
                    break;
                }
                azahaAnimation.SetFloat("IdleValue", idleValue);
            }
            else if (!azahaBattle && idleValue > 0f)
            {
                idleValue -= Time.deltaTime;
                if (idleValue < 0)
                {
                    idleValue = 0;
                    break;
                }
                azahaAnimation.SetFloat("IdleValue", idleValue);
            }
            yield return new WaitForSeconds(Time.deltaTime * 0.3f);
        }
        yield return null;
    }

    public bool AnimationEndCheck()
    {
        if (azahaAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            if (azahaAnimation.GetCurrentAnimatorStateInfo(0).IsName("SlideStep"))
            {
                azahaAnimation.SetBool("SlideStep", false);
                return false;
            }
            if (azahaAnimation.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
            {
                azahaAnimation.SetBool("SpecialAttack", false);
                return false;
            }
            if (azahaAnimation.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                azahaAnimation.SetBool("Attack", false);
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
                azahaAnimation.SetBool("Run", false);
                azahaAnimation.SetBool("DoubleJump", false);
                azahaAnimation.SetBool("Jump", false);
                azahaAnimation.SetBool("Fly", false);
                azahaAnimation.SetBool("Fall", false);
                azahaAnimation.SetBool("Attack", false);
                azahaAnimation.SetBool("Ride", false);
                azahaAnimation.SetBool("Climing", false);
                azahaAnimation.SetBool("Hang", false);
                break;
            case PlayerState.Player_Run:
                azahaAnimation.SetBool("Jump", false);
                azahaAnimation.SetBool("DoubleJump", false);
                azahaAnimation.SetBool("Run", true);
                azahaAnimation.SetBool("Fly", false);
                azahaAnimation.SetBool("Fall", false);
                azahaAnimation.SetFloat("PosX", Input.GetAxisRaw("Horizontal"));
                azahaAnimation.SetFloat("PosY", Input.GetAxisRaw("Vertical"));
                break;
            case PlayerState.Player_Jump:
                azahaAnimation.SetBool("Jump", true);
                break;
            case PlayerState.Player_DoubleJump:
                azahaAnimation.SetBool("DoubleJump", true);
                break;
            case PlayerState.Player_Attack:
                azahaAnimation.SetBool("Attack", true);
                azahaAnimation.Play(0);
                break;
            case PlayerState.Player_SlideStep:
                azahaAnimation.SetBool("SlideStep", true);
                break;
            case PlayerState.Player_SpecialAttack:
                azahaAnimation.SetBool("SpecialAttack", true);
                break;
            case PlayerState.Player_Fly:
                azahaAnimation.SetBool("Fly", true);
                break;
            case PlayerState.Player_Fall:
                azahaAnimation.SetBool("Fly", false);
                azahaAnimation.SetBool("Hang", false);
                azahaAnimation.SetBool("Fall", true);
                azahaAnimation.SetBool("Climing", false);
                break;
            case PlayerState.Player_Ride:
                azahaAnimation.SetBool("Ride", true);
                break;
            case PlayerState.Player_Climing:
                azahaAnimation.SetBool("Climing", true);
                break;
            case PlayerState.Player_Hang:
                azahaAnimation.SetBool("Hang", true);
                azahaAnimation.SetFloat("PosX", Input.GetAxisRaw("Horizontal"));
                break;
            case PlayerState.Player_Death:
                azahaAnimation.SetBool("Death", true);
                break;
        }
    }
}
