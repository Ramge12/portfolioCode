using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabAnimation : MonoBehaviour
{
    [SerializeField] private Animator scrabAnimator;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private Transform scarabTransform;
    [SerializeField] private AudioSource attackSound;

  public void AnimationEnd()
    {
        if (scrabAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.90f)
        {
            if (scrabAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                scarabTransform.GetComponent<ScarabController>().damagePlayer.oneDamage = true;
                scrabAnimator.SetBool("Attack", false);
            }
            if (scrabAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                this.transform.gameObject.SetActive(false);
                scarabTransform.position = new Vector3(0, -100, 0);
            }
        }
    }

    public void setAnimation(EnemyStatus animation)
    {
        switch(animation)
        {
            case EnemyStatus.enemy_Idle:
                scrabAnimator.SetBool("Run", false);
                scrabAnimator.SetBool("Attack", false);
                break;
            case EnemyStatus.enemy_Attack:
                if (!attackSound.isPlaying) attackSound.Play();
                scrabAnimator.SetBool("Run", false);
                scrabAnimator.SetBool("Attack", true);
                break;
            case EnemyStatus.enemy_Run:
                scrabAnimator.SetBool("Run", true);
                scrabAnimator.SetBool("Attack", false);
                break;
            case EnemyStatus.enemy_Death:
                scrabAnimator.SetBool("Death", true);
                deathParticle.SetActive(true);
                break;
        }
    }
}
