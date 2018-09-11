using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScarabController : MonoBehaviour {

    [Header("ScarabScript")]
    [SerializeField] private ScarabMove scarabMove;
    [SerializeField] private ScarabSatus scarabStatus;
    [SerializeField] private ScarabAnimation scrabAnimation;

    [Header("Scarab Value")]
    [SerializeField] private EnemyStatus scarabEnum;
    [SerializeField] private CharactorStatistics scarabStatistics = new CharactorStatistics();

    [Header("Scarab nav AI")]
    public NavMeshAgent nav;
    public Transform player;

    [Header("Scarab Rest Scripts")]
    public TargetRange targetRange;
    public DamagePlayer damagePlayer;
    public ScarabSpawnPoint sacrabSpawnPoint;

   void Start () {
        nav = this.transform.GetComponent<NavMeshAgent>();
        scarabStatus.chractorStat = scarabStatistics;
        scarabStatus.bodyTransform = this.transform;

        damagePlayer.damage = -scarabStatistics.Damage;
    }

	void Update () {
        StartAttack();
        if (scarabEnum == EnemyStatus.enemy_Attack)
        {
            damagePlayer.battle = true;
        }
        else
        {
            damagePlayer.battle = false;
        }
    }
   

    void StartAttack()
    {
        if (scarabEnum != EnemyStatus.enemy_Death)
        {
            if (targetRange.tragetInRange)
            {
                damagePlayer.transform.GetComponent<CapsuleCollider>().height = 3.0f;
                scarabMove.AttackMove(targetRange.target, scarabEnum);
                
                if (player != null && nav != null )
                {
                    nav.isStopped = true;
                }
            }
            else
            {
                damagePlayer.transform.GetComponent<CapsuleCollider>().height = 1.5f;
       
                if(player!=null && nav!=null)
                {
                    nav.SetDestination(player.position);
                    nav.isStopped = false;
                    SetAnimation(EnemyStatus.enemy_Run);
                }
            }
        }
        scrabAnimation.AnimationEnd();
    }

    public void SetTextCamera(CameraController cameraController)
    {
        scarabStatus.hitText.transform.GetComponent<CameraTalk>().mainCamera = cameraController.gameObject;
    }

    public void SetAnimation(EnemyStatus animation)
    {
        scarabEnum = animation;
        scrabAnimation.setAnimation(scarabEnum);
    }
}
