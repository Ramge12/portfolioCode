using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodzillaAnimation : MonoBehaviour {

    [SerializeField] private GameObject breathEffect;
    private Animator godzillaAnimator;

    public void Start()
    {
        godzillaAnimator = this.GetComponent<Animator>();
    }

    public bool AnimationEndCheck()
    {
        if (godzillaAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            if (godzillaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                godzillaAnimator.SetBool("Attack", false);
                return false;
            }
            if (godzillaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Breath"))
            {
                breathEffect.SetActive(false);
                godzillaAnimator.SetBool("Breath", false);
                return false;
            }
        }
        return true;
    }

    public void ChangeAnimation(EnemyStatus godzillaStatus)
    {
        switch (godzillaStatus)
        {
            case EnemyStatus.enemy_Idle:
                godzillaAnimator.SetBool("Walking", false);
                godzillaAnimator.SetBool("Breath", false);
                godzillaAnimator.SetBool("Attack", false);
                break;
            case EnemyStatus.enemy_Run:
                godzillaAnimator.SetBool("Walking", true);
                godzillaAnimator.SetBool("Attack", false);
                godzillaAnimator.SetBool("Breath", false);
                break;
            case EnemyStatus.enemy_Attack:
                godzillaAnimator.SetFloat("AttackValue", (int)Random.Range(0, 2));
                godzillaAnimator.SetBool("Walking", false);
                godzillaAnimator.SetBool("Attack", true);
                godzillaAnimator.SetBool("Breath", false);
                break;
            case EnemyStatus.enemy_Death:
                godzillaAnimator.SetBool("Death", true);
                break;
            case EnemyStatus.enemy_Breath:
                breathEffect.SetActive(true);
                godzillaAnimator.SetBool("Breath", true);
                godzillaAnimator.SetBool("Walking", false);
                godzillaAnimator.SetBool("Attack", false);
                break;
        }
    }
}
