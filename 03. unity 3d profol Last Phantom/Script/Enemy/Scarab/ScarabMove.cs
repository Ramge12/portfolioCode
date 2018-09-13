using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarabMove : MonoBehaviour {

    [SerializeField] private float attackRange;
    [SerializeField] private CharactorMoveValue charactorMoveValue = new CharactorMoveValue();
    [SerializeField] private AudioSource footStep;
    private Vector3 moveVector;
    private Transform scarabTransform;

    void Start () {
        scarabTransform = this.transform;
    }

    public void AttackMove(Transform target, EnemyStatus scarabStatus)
    {
        moveVector = target.position - scarabTransform.position;
        moveVector = new Vector3(moveVector.x, 0, moveVector.z);
        float dist = Vector3.Distance(target.position, scarabTransform.position);

        if (dist > attackRange)
        {
            if (scarabStatus == EnemyStatus.enemy_Idle || scarabStatus == EnemyStatus.enemy_Run || scarabStatus == EnemyStatus.enemy_Attack)
            {
                if (!footStep.isPlaying) footStep.Play();
                scarabTransform.GetComponent<ScarabController>().SetAnimation(EnemyStatus.enemy_Run);
                scarabTransform.position += (moveVector * charactorMoveValue.moveSpeed * Time.deltaTime);
                var targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
                scarabTransform.rotation = Quaternion.Slerp(scarabTransform.rotation, targetRotation, charactorMoveValue.turnSpeed * Time.deltaTime);
            }
        }
        else
        {
            scarabTransform.GetComponent<ScarabController>().SetAnimation(EnemyStatus.enemy_Attack);
        }
    }
}
