using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobomanMove : MonoBehaviour {

    [Header("Roboman Move Value")]
    [SerializeField] private float backRunRange;
    [SerializeField] private float maxAttackRange;
    [SerializeField] private float minAttackRange;

    [Header("Roboman Move Scripts")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private CharactorMoveValue robomanMoveValue;
    [SerializeField] private CharacterController robomanCharactor;

    public bool playerInRange;

    private float distance;
    private Vector3 moveVector;
    private Transform robomanTransform;

    private void Start()
    {
        robomanTransform = this.transform;
    }

    public EnemyStatus RobomanMoving()
    {
        distance = Vector3.Distance(playerTransform.position, robomanTransform.position);

        moveVector = (playerTransform.position - robomanTransform.position).normalized;
        moveVector = new Vector3(moveVector.x, 0, moveVector.z);
        var targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);

        if (distance < maxAttackRange && distance > minAttackRange)
        {   //최대 사정거리 안에 들어왔으며  최소 사정거리보다 멀다
            playerInRange = true;

            if (distance < backRunRange)
            {   //매우 가까운 상태 이동하지 않는다
                return EnemyStatus.enemy_Attack;
            }
            else
            {
                robomanCharactor.Move(moveVector * robomanMoveValue.moveSpeed * Time.deltaTime);
                robomanTransform.rotation = Quaternion.Slerp(robomanTransform.rotation, targetRotation, robomanMoveValue.turnSpeed * Time.deltaTime);
                robomanTransform.eulerAngles += new Vector3(0,60f, 0);
                //적절하게 거리가 벌려져있는 상태 이동한다
            }
            return EnemyStatus.enemy_BattleRun;
        }
        else if (distance < minAttackRange)
        {
            robomanCharactor.Move(-moveVector * robomanMoveValue.moveSpeed * Time.deltaTime);
            robomanTransform.rotation = Quaternion.Slerp(robomanTransform.rotation, targetRotation, robomanMoveValue.turnSpeed * Time.deltaTime);
            robomanTransform.eulerAngles += new Vector3(0, 60f, 0);
            //뒤로 이동한다
            return EnemyStatus.enemy_BackMove;
        }
        else
        {
            playerInRange = false;
        }

        return EnemyStatus.enemy_Idle;
    }
}
