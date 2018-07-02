using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : MonoBehaviour      //코뿔소 컨트롤 클래스
{
    public Animator RhinoAni;          
    public GameObject Player;          
    public GameObject Message;         
    public ParticleSystem footstep;    

    Vector3 lookDir;                   
    float moveSpeed = 5.0f;            
    float turnSpeed = 50f;             
    bool Rhino_move = false;                   


    void Start()
    {
        Rhino_move = false;            
        footstep.Stop();                    
    }

    void Update()
    {
        if (Player.GetComponent<PlayerCtr>().Riding)            
        {
            MoveCharactor();                                    
            if (Message.activeSelf) Message.SetActive(false);   
        }
    }

    void playSounding(string snd)       
    {
        if (!GameObject.Find(snd).GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find(snd).GetComponent<AudioSource>().Play();    
        }
    }

    void MoveCharactor()          
    {   
        float xx = Input.GetAxisRaw("Vertical");
        float zz = Input.GetAxisRaw("Horizontal");
        lookDir = Vector3.forward * xx + Vector3.right * zz;
        if (Input.GetKey(KeyCode.W))
        {
            if (!footstep.isPlaying) footstep.Play();  
            playSounding("RhinoSound");
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            RhinoAni.SetBool("Run", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playSounding("RhinoSound");
            transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
            RhinoAni.SetBool("Run", true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))RhinoAni.SetBool("Run", false);
        if (Input.GetKey(KeyCode.A))transform.Rotate(0f, zz * turnSpeed * Time.deltaTime, 0f);
        if (Input.GetKey(KeyCode.D))transform.Rotate(0f, zz * turnSpeed * Time.deltaTime, 0f);
    }
}
