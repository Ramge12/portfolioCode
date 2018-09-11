
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSystem : MonoBehaviour{

    [Header("BikeTransform")]
    [SerializeField] private Transform bikeTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform bikeBodyTransform;
    [SerializeField] private Transform forntTireTransform;

    [Header("BikeWheel")]
    [SerializeField] private  WheelCollider fWheel;
    [SerializeField] private  WheelCollider rWheel;

    [Header("BikeValue")]
    [SerializeField] private float bikeSpeed;
    [SerializeField] private float breakForce;
    [SerializeField] private float bikeRotAngle = 45;
    [SerializeField] private float bikeTurenSpeed;

    [Header("BikeScript")]
    [SerializeField] private CameraController cameraController;


    [System.NonSerialized] public bool ride = false;

    private bool outBike = false;
    private float wheelRotX = 0;
    private float wheelRotY = 0;
    private float bikeBodyRotZ = 0;

    void Start()
    { 
        fWheel.steerAngle = 0;
        rWheel.steerAngle = 0;
        bikeTransform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        MoveBike();
    }

    void MoveBike()
    {
        float bikeVertical      = Input.GetAxisRaw("Vertical");
        float bikeHorizontal    = Input.GetAxisRaw("Horizontal");

        Vector3 bikeBodyLocalAngle  = bikeBodyTransform.localEulerAngles;
        Vector3 frontTireLocalAngle = forntTireTransform.localEulerAngles;

        if (ride)
        {
            BreakBike();
            MoveBike(bikeHorizontal, bikeVertical);
            RotTireWheel(bikeVertical, frontTireLocalAngle);
            RotBike(bikeHorizontal, bikeVertical, bikeBodyLocalAngle, frontTireLocalAngle);
            if (Input.GetKeyDown(KeyCode.R) && outBike) EscapeBike();
        }
    }

    void BreakBike()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayerSound.playSoundManagerCall().PlayAudio("bikeBreak", false, 0);
            fWheel.brakeTorque = breakForce;
            rWheel.brakeTorque = breakForce;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            fWheel.brakeTorque = 0;
            rWheel.brakeTorque = 0;
        }
    }

    void MoveBike(float bikeHorizontal, float bikeVertical)
    {
        fWheel.motorTorque = bikeVertical * bikeSpeed;
        rWheel.motorTorque = bikeVertical * bikeSpeed;
        fWheel.steerAngle = bikeHorizontal * bikeRotAngle;
        rWheel.steerAngle = bikeHorizontal * bikeRotAngle;

        if(bikeVertical!=0) PlayerSound.playSoundManagerCall().PlayAudio("bikeDrive", false, 0); 
    }

    void RotTireWheel(float bikeVertical, Vector3 frontTireLocalAngle)
    {
        if (Input.GetKey(KeyCode.W))
        {
            wheelRotX += bikeVertical * 200 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            wheelRotX += bikeVertical * 200 * Time.deltaTime;
        }
        forntTireTransform.localEulerAngles = new Vector3(wheelRotX, frontTireLocalAngle.y, frontTireLocalAngle.z);
    }

    void RotBike(float bikeHorizontal, float bikeVertical, Vector3 bikeBodyLocalAngle, Vector3 frontTireLocalAngle)
    {
        transform.Rotate(0f, bikeHorizontal * bikeRotAngle * Time.deltaTime, 0f);

        if (Input.GetKey(KeyCode.A))
        {
            if (wheelRotY < -30) wheelRotY = -30;
            else wheelRotY += bikeHorizontal * 100 * Time.deltaTime;

            if (bikeBodyRotZ >= 15) bikeBodyRotZ = 15;
            else bikeBodyRotZ -= bikeHorizontal * 100 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (wheelRotY > 30) wheelRotY = 30;
            else wheelRotY += bikeHorizontal * 100 * Time.deltaTime;

            if (bikeBodyRotZ <= -15) bikeBodyRotZ = -15;
            else bikeBodyRotZ -= bikeHorizontal * 50 * Time.deltaTime;
        }
        else
        {
            ResetRotTireWheel(bikeBodyLocalAngle, frontTireLocalAngle);
        }

        forntTireTransform.localEulerAngles = new Vector3(frontTireLocalAngle.x, wheelRotY, frontTireLocalAngle.z);
        bikeBodyTransform.localEulerAngles = new Vector3(bikeBodyLocalAngle.x, bikeBodyLocalAngle.y, bikeBodyRotZ);
    }

    void ResetRotTireWheel(Vector3 bikeBodyLocalAngle, Vector3 frontTireLocalAngle)
    {
        if (Mathf.Abs(wheelRotY) < 1) wheelRotY = 0;
        else if (wheelRotY < 0) wheelRotY += 100 * Time.deltaTime;
        else if (wheelRotY > 0) wheelRotY += -100 * Time.deltaTime;

        if (Mathf.Abs(bikeBodyRotZ) < 0.1f) bikeBodyRotZ = 0;
        else if (bikeBodyRotZ < 0) bikeBodyRotZ += 100 * Time.deltaTime;
        else if (bikeBodyRotZ > 0) bikeBodyRotZ -= 100 * Time.deltaTime;
    }

    public void RideOnPlayer()
    {
        playerTransform.parent = bikeBodyTransform;
        playerTransform.localEulerAngles = new Vector3(0, 0, 0f);
        playerTransform.localPosition = new Vector3(1.7f, 70, -129f);
        playerTransform.GetComponent<PlayerKeyController>().enabled = false;
        playerTransform.GetComponent<CharacterController>().enabled = false;
        playerTransform.GetComponent<PlayerStats>().CurPlayerStatus(PlayerState.Player_Ride);

        ride = true;
        StartCoroutine(StartSizeDown());
        StartCoroutine(WaitingOut());
    }

    IEnumerator StartSizeDown()
    {
        float curSize = bikeTransform.localScale.x * 0.1f;
        float timer = Time.deltaTime * 0.01f;

        while (true)
        {
            bikeTransform.localScale = new Vector3(bikeTransform.localScale.x - timer, bikeTransform.localScale.y - timer, bikeTransform.localScale.z - timer);
            if (bikeTransform.localScale.x < curSize)
            {
                bikeTransform.localScale = new Vector3(curSize, curSize, curSize);
                cameraController.magnification = 0.2f;
                break;
            }
            cameraController.magnification -= Time.deltaTime*2f;
            if (cameraController.magnification < 0.2f) cameraController.magnification = 0.2f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    IEnumerator WaitingOut()
    {
        yield return new WaitForSeconds(1.0f);
        outBike = true;
        yield return null;
    }

    void EscapeBike()
    {
        bikeTransform.localScale = bikeTransform.localScale * 10f;
        playerTransform.parent = null;
        cameraController.magnification = 1f;
        playerTransform.GetComponent<PlayerKeyController>().enabled = true;
        playerTransform.GetComponent<CharacterController>().enabled = true;
        playerTransform.GetComponent<PlayerStats>().CurPlayerStatus(PlayerState.Player_Jump);
        playerTransform.localPosition = playerTransform.localPosition + new Vector3(1, 0, 0);
        ride = false;
        outBike = false;
    }

    public void HurtPlayer(int Damage)
    {
        playerTransform.GetComponent<PlayerStats>().AddPlayerHealthPoint(Damage,0);
    }
}
