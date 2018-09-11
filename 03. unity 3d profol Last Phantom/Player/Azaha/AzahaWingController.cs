using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzahaWingController : MonoBehaviour {

    [Header("-Wing parts")]
    [SerializeField] private GameObject wingRoot;
    [SerializeField] private GameObject[] wingsFirstJoint;
    [SerializeField] private GameObject[] wingsSecondJoint;

    [Header("-Wing Controll Value")]
    [SerializeField] private float wingCheckTime = 0f;
    [SerializeField] private float wingAngle = 30;
    [SerializeField] private float randWingTime = 1.0f;
    [SerializeField] private float wingRootRotSpeed = 220f;
    
    private bool randFoldWing = false;

    void Start () {
    }

    public void SelectWingFold(int number)
    {
        StartCoroutine(WingControll(number, wingCheckTime,  -wingAngle));
    }

    public void AllWingFold()
    {
        for(int i=0;i<6;i++) StartCoroutine(WingControll(i, wingCheckTime, wingAngle));
    }

    public void StartWingRoation()
    {
        StartCoroutine(WingRotation(true));
    }

    public IEnumerator WingRotation(bool left)
    {
        float rotZ=0;
        Transform rootTransfrom = wingRoot.transform;
        float zAngle = rootTransfrom.localEulerAngles.z;

        while (true)
        {
            if (left) rotZ += -wingRootRotSpeed * Time.deltaTime;
            else rotZ += wingRootRotSpeed * Time.deltaTime;

            if (Mathf.Abs(rotZ) > 180)
            {
                if (left) rotZ = -180;
                else rotZ = 180;
            }

            rootTransfrom.localEulerAngles =new Vector3(rootTransfrom.localEulerAngles.x,rootTransfrom.localEulerAngles.y,zAngle + rotZ);

            if (Mathf.Abs(rotZ) == 180) break;

            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    IEnumerator WingControll(int wingNum,float time,float angle)
    {
        float timer = 0.0f;
        float count = 0.0f;

        Transform fisrtJoint = wingsFirstJoint[wingNum].transform;
        Transform secondJoint = wingsSecondJoint[wingNum].transform;

        while (time > timer)
        {
            timer += Time.deltaTime;
            if (timer >= count)
            {
                count += 0.025f;
                fisrtJoint.Rotate(0, angle * Time.deltaTime, 0);
                secondJoint.Rotate(0, angle * Time.deltaTime, 0);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}
