using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobomanController : MonoBehaviour {

    [Header("Roboman Scripts")]
    [SerializeField] private RobomanMove robomanMove;
    [SerializeField] private RobomanBattle robomanBattle;
    [SerializeField] private RobomanAnimation robomanAnimation;

    [Header("Roboman Vlaue")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private EnemyStatus roboEnum;

    private Transform roboTransform;

    void Start () {
        roboTransform = this.transform;
    }
	
	void Update () {
        RobomanMove();
        RobomanShoot();
    }

    void RobomanMove()
    {
        if (robomanBattle.reload)
        {
            roboEnum = EnemyStatus.enemy_RunReload;
            robomanAnimation.ChangeAnimation(roboEnum);
        }
        else
        {
            roboEnum = robomanMove.RobomanMoving();
            robomanAnimation.ChangeAnimation(roboEnum);
        }
    }

    void RobomanShoot()
    {
        if (roboEnum != EnemyStatus.enemy_Idle || roboEnum != EnemyStatus.enemy_Death)
        {
            if (!robomanBattle.roboBattle && robomanMove.playerInRange)
            {
                robomanBattle.StartCoroutine(robomanBattle.ShootStart(playerTransform, roboTransform));
            }
            else if (!robomanMove.playerInRange)
            {
                robomanBattle.StopShooting();
            }
        }
        else
        {
            robomanBattle.StopShooting();
        }
    }
}
