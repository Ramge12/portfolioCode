using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobomanAnimation : MonoBehaviour
{
    private Animator robomanAnimator;

    private void Start()
    {
        robomanAnimator = this.transform.GetComponent<Animator>();
    }

    public void ChangeAnimation(EnemyStatus roboEnum)
    {
        switch(roboEnum)
        {
            case EnemyStatus.enemy_Idle:
                robomanAnimator.SetBool("Run", false);
                robomanAnimator.SetBool("Shot", false);
                robomanAnimator.SetBool("Reload", false);
                break;
            case EnemyStatus.enemy_Run:
                robomanAnimator.SetBool("Run", true);
                robomanAnimator.SetBool("Shot", false);
                robomanAnimator.SetFloat("RunValue", 0);
                robomanAnimator.SetBool("Reload", false);
                break;
            case EnemyStatus.enemy_BackMove:
                robomanAnimator.SetBool("Run", true);
                robomanAnimator.SetBool("Shot", false);
                robomanAnimator.SetFloat("RunValue", 1);
                robomanAnimator.SetBool("Reload", false);
                break;
            case EnemyStatus.enemy_Attack:
                robomanAnimator.SetBool("Run", false);
                robomanAnimator.SetBool("Shot", true);
                robomanAnimator.SetBool("Reload", false);
                break;
            case EnemyStatus.enemy_BattleRun:
                robomanAnimator.SetBool("Run", true);
                robomanAnimator.SetBool("Shot", true);
                robomanAnimator.SetBool("Reload", false);
                break;
            case EnemyStatus.enemy_Death:
                robomanAnimator.SetBool("Death", true);
                robomanAnimator.SetBool("Reload", false);
                break;
            case EnemyStatus.enemy_Reload:
                robomanAnimator.SetBool("Reload", true);
                break;
            case EnemyStatus.enemy_RunReload:
                robomanAnimator.SetBool("Reload", true);
                break;
        }
    }
}
