using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StepThroughPortal : MonoBehaviour {

    [SerializeField] private Transform otherPortal;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = otherPortal.position + otherPortal.forward*2;
            other.transform.LookAt(otherPortal.position + otherPortal.forward * 20);
            other.transform.localEulerAngles = new Vector3(0, other.transform.localEulerAngles.y, 0);
        }
        else if (other.CompareTag("Weapon"))
        {
            other.GetComponent<Rigidbody>().Sleep();
        
            RaycastHit hit;
            if (Physics.Raycast(other.transform.position, this.transform.position, out hit))
            {
                Vector3 incomingVec = other.GetComponent<BulletValue>().dir;
                Vector3 nomalVec = hit.normal;
                Vector3 reflectVec = Vector3.Reflect(incomingVec, nomalVec);
                other.GetComponent<Rigidbody>().AddForce(other.GetComponent<BulletValue>().attackSpeed * reflectVec);
            }
            other.transform.position = otherPortal.position + otherPortal.forward * 2;
        }
    }

}
