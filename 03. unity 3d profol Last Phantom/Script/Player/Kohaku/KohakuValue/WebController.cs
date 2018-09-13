using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WebController  {

    [SerializeField] private float drag;
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float maximumSpeed;
    [SerializeField] private Vector3 gravityDirection = new Vector3(0, 1, 0);

    public Vector3 velocity;        //속도
    private Vector3 dampingDirection;       //공기저항력

    public void ApplyGravity()
    {
        velocity -= gravityDirection * gravity * Time.deltaTime;
    }

    public void ApplyDamping()
    {
        dampingDirection = -velocity;
        dampingDirection *= drag;
        velocity += dampingDirection;       //공기저항력을 더해준다
    }

    public void CapMaxSpeed()//최대속도제한
    {
        velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);
    }
}
