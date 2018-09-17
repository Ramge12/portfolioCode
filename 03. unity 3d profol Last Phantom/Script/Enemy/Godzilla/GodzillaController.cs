using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GodzillaController : MonoBehaviour {

    [Header("GodzillaScripts")]
    [SerializeField] private GodzillaAnimation godzillaAnimation;
    [SerializeField] private GodzillaStatus godzillaStat;
    [SerializeField] private Stage3Quest boosUI;
    [SerializeField] private CameraController cameraController;

    [Header("GodzillaTransform")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform godzillaTransform;
    [SerializeField] private DamagePlayer[] damagePlayer = new DamagePlayer[5];

    [Header("GodzillaAudio")]
    [SerializeField] private AudioSource screamAudio;
    [SerializeField] private AudioSource moveAudio;

    [Header("GodzillaValue")]
    [SerializeField] private float rangeDistance;
    [SerializeField] private EnemyStatus godzillaStatus;

    [Header("Godzilla nav Nav")]
    public NavMeshAgent nav;

    [System.NonSerialized] public bool godzillaDeath;

    private int attackCounter = 1;
    private bool moveGodzilla = true;
    private float walkTimer;
    private float attackTimer;

    void Start () {
        godzillaStatus = EnemyStatus.enemy_Idle;

        for (int i = 0; i < damagePlayer.Length; i++){
            damagePlayer[i].damage = godzillaStat.geDamage();
        }
    }
	
	void Update () {
        if (!godzillaDeath)
        {
            godzillaAnimation.AnimationEndCheck();
            GozillaMove();

            for (int i = 0; i < damagePlayer.Length; i++)
            {
                damagePlayer[i].oneDamage = true;
            }
        }
        else
        {

            nav.isStopped = true;
            nav.speed = 0;
            boosUI.QuestClearUI();
            godzillaStatus = EnemyStatus.enemy_Death;
            godzillaAnimation.ChangeAnimation(EnemyStatus.enemy_Death);
        }
    }

    void GozillaMove()
    {
        if (DistanceCheck() && !godzillaDeath)
        {
            if (!boosUI.gameObject.activeSelf)
            {
                boosUI.gameObject.SetActive(true);
            }
            nav.SetDestination(playerTransform.position);
          
            MoveToPlayer();
            AttackGodzilla();
        }
        else
        {
            nav.speed = 0;
            godzillaStatus = EnemyStatus.enemy_Idle;
            godzillaAnimation.ChangeAnimation(EnemyStatus.enemy_Idle);
        }
    }

    bool DistanceCheck()
    {
        float distance = Vector3.Distance(playerTransform.position, godzillaTransform.position);
        
        if(distance<rangeDistance)
        {
           return true;
        }
        else
        {
            return false;
        }
    }

    void MoveToPlayer()
    {
        if (moveGodzilla && !godzillaDeath)
        {
            walkTimer += Time.deltaTime;
            if(walkTimer%1.4f > 1.0f)
            {
                cameraController.StartCoroutine(cameraController.FootStepCamera(0.2f, 0.1f));
                if (!moveAudio.isPlaying) moveAudio.Play();
            }
            godzillaStatus = EnemyStatus.enemy_Run;
            godzillaAnimation.ChangeAnimation(EnemyStatus.enemy_Run);
            nav.speed =2;
        }
        else
        {
            nav.speed = 0;
        }
    }

    void AttackGodzilla()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer > attackCounter * 10)
        {
            if (attackCounter % 3 == 0)
            {
                screamAudio.Play();
                godzillaStatus = EnemyStatus.enemy_Breath;
                godzillaAnimation.ChangeAnimation(EnemyStatus.enemy_Breath);
            }
            else
            {
                screamAudio.Play();
                godzillaStatus = EnemyStatus.enemy_Attack;
                godzillaAnimation.ChangeAnimation(EnemyStatus.enemy_Attack);
            }

            moveGodzilla = false;
            attackCounter++;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(4.5f);
        moveGodzilla = true;
        yield return null;
    }
}
