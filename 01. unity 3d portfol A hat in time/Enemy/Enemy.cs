using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_STATE
{
    IDLE,
    WALK,
    Hit,
    ATTACK
}

public class Enemy : MonoBehaviour
{
    public ENEMY_STATE currState = ENEMY_STATE.IDLE;    
    public ENEMY_STATE nextState = ENEMY_STATE.IDLE;    
    public ParticleSystem DeathEffect;                                  

    private Animator anim;                              
    private Vector3  targetPos;                                  
    private Camera   eye;                               //에너미의 시야(실제로 에너미의 눈 위치)

    GameObject player;                                  
    float AttackRange = 2.0f;                           
    float rotationSpeed = 1.0f;                         
    float walkSpeed = 1.0f;                             
    bool AwayEnemy = false;                                        
    //--------------------------------------------------------------------------------------------
    void Start()
    {
        anim = GetComponent<Animator>();                   
        eye = transform.GetComponentInChildren<Camera>();  
        AwayEnemy = false;                                 
        ChangeCoroutine(currState);                        
        DeathEffect.Stop();                                        
    }

    void Update()
    {
        if (!AwayEnemy)                 
        {
            switch (currState)          
            {
                case ENEMY_STATE.IDLE:
                    UpdateIdle();
                    break;
                case ENEMY_STATE.WALK:
                    UpdateWalk();
                    break;
                case ENEMY_STATE.ATTACK:
                    UpdateAttack();
                    break;
                case ENEMY_STATE.Hit:
                    UpdateHit();
                    break;

            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death") & anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
            {   //막약 Death애니메이션이 재생중일 경우 죽음 이펙트를 활성화시키고 코루틴에 onDeath값을 넣어줍니다
                if (!DeathEffect.isPlaying) DeathEffect.Play();
                StartCoroutine("onDeath");
            }
        }
        else 
        {
            transform.position += (transform.forward * -walkSpeed * Time.deltaTime);   
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Running") & anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f)
            {   //달리기 에니메이션이 끝날때쯤 이펙트를 재생하고 코루틴에 onDeath값을 넘겨줍니다
                if (!DeathEffect.isPlaying) DeathEffect.Play();
                StartCoroutine("onDeath");
            }
        }
    }

    public void RunAway()                       
    {
        Vector3 targetDir = targetPos - transform.position;                     
        targetDir.y = 0;                                                        
        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);    
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        AwayEnemy = true;
        anim.SetBool("Runaway", true);                                          
    }

    void UpdateHit()                          
    {
        if(!anim.GetBool("Death"))anim.SetBool("Death", true);  
        anim.SetBool("Hit", true); 
    }

    void MovePatrol()                       
    {
        Vector3 targetDir = targetPos - transform.position;             
        targetDir.y = 0;                                                
        float dist = Vector3.Distance(targetPos, transform.position);   
        if (dist < AttackRange)                                         
        {
            ChangeCoroutine(ENEMY_STATE.ATTACK);                               
            return;
        }
        var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);                                        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);  
        transform.position += (transform.forward * walkSpeed * Time.deltaTime);                                     
    }

    void UpdateIdle()                   
    {
        IsFindEnemy();                  
    }

    void UpdateWalk()                   
    {
        if (IsFindEnemy()) MovePatrol();
        else MovePatrol();              
    }

    bool IsFindEnemy()                  
    {
        bool isFind = false;                                           
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(eye);  
        player = GameObject.Find("Player");                            
        if (player == null) return false;                              
        Bounds bounds = player.GetComponentInChildren<SkinnedMeshRenderer>().bounds;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
        isFind = GeometryUtility.TestPlanesAABB(planes, bounds);        //AABB충돌을 이용하여 bouns(충돌박스)가 시야 안에 들어오는지 파악합니다
        if (isFind)                                                  
        {
            float dist = Vector3.Distance(targetPos, transform.position);
            if (dist > 1.0f) ChangeCoroutine(ENEMY_STATE.WALK);          
            targetPos = player.transform.position;                      
        }
        return isFind;                                                                                        
    }

    void UpdateAttack()                         
    {
        targetPos = player.transform.position;              
        Vector3 targetDir = targetPos - transform.position; 
        targetDir.y = 0;                                                   
        float dist = Vector3.Distance(targetPos, transform.position);  
        anim.SetBool("Attack", true);                      
        if (dist > AttackRange)                            
        {
            ChangeCoroutine(ENEMY_STATE.IDLE);                                        
            return;
        }
    }

    void ChangeCoroutine(ENEMY_STATE nextState) 
    {
        StopAllCoroutines();            
        currState = nextState;          
        anim.SetBool("Idle", false);    
        anim.SetBool("Walk", false);
        anim.SetBool("Damaged", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Hit", false);
        switch (currState)               
        {
            case ENEMY_STATE.IDLE: StartCoroutine("CoroutineIdle"); break;  
            case ENEMY_STATE.WALK: StartCoroutine("CoroutineWalk"); break;
        }
    }

    IEnumerator onDeath()                       
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);  
            Destroy(this.gameObject);
        }
    }

    IEnumerator CoroutineIdle()         
    {
        anim.SetBool("Idle", true);     
        nextState = ENEMY_STATE.IDLE;         
        while (true)
        {
            yield return new WaitForSeconds(1.0f);  
            ChangeCoroutine(nextState); //1초뒤 ChangeCoroutine함수에 Idle을 넣고 실행합니다
        }
    }

    IEnumerator CoroutineWalk()           
    {
        anim.SetBool("Walk", true);             
        targetPos = transform.position + new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f));
        int rand = Random.Range(0, 2);     
        switch (rand)                 
        {
            case 0: nextState = ENEMY_STATE.IDLE; break;
            case 1: nextState = ENEMY_STATE.WALK; break;
        }
        while (true)
        {
            yield return new WaitForSeconds(1.0f); 
            ChangeCoroutine(nextState);
        }
    }
}