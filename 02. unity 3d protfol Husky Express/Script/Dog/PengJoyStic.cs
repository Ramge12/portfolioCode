using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PengJoyStic : MonoBehaviour {
    
    private Vector3 MoveVector;                                                                     
    private Transform m_Transform;        
    public JoyStick joystick;             
    public Animation M_ani;               
    public AnimationClip M_idle;          
    public AnimationClip M_run;           
    public AnimationClip M_walk;          
    public float turnSpeed = 100.0f;      
    public float MoveSpeed;               
    

    void Start()
    {
        M_ani.clip = M_idle;                
        M_ani.Play();                       
        m_Transform = gameObject.transform; 
        MoveVector = Vector3.zero;            
    }

    void Update(){
    }

    void FixedUpdate()
    {
        HandleInput();
        Move();
    }

    public void HandleInput()
    {
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();
        Vector3 moveDir = new Vector3(h, 0, v).normalized;
        if (Mathf.Abs(h) > 0.5f) transform.Rotate(0f, h * turnSpeed * Time.deltaTime, 0f);
        MoveVector = moveDir;
    }

    void Move()
    {
        float v = joystick.GetVerticalValue();
        if (Mathf.Abs(v) >= 0.1f && v > 0)      //vectical값이 0.1보다 클때만 움직입니다
        {
            m_Transform.Translate(Vector3.forward * -MoveSpeed * v * Time.deltaTime);
            M_ani.clip = M_walk;
            M_ani.Play();
        }
        else if (Mathf.Abs(v) < 0.1f)
        {
            M_ani.clip = M_idle;
            M_ani.Play();
        }
    }
}
