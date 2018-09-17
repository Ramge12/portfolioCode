using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletValue : MonoBehaviour {

    public int weaponDamage;
    public bool passShot;
    public float attackSpeed;
    public Vector3 dir;
    public Transform playerTransform;

    private Rigidbody bulletRigid;
    private Transform bulletTrans;

    private void Start()
    {
        bulletTrans = this.transform;
        bulletRigid = this.transform.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") ||other.CompareTag("Wall"))
        {
            if (!passShot)
            {
                bulletRigid.Sleep();
                bulletTrans.position = new Vector3(0, -200, 0);
            }
        }
        else if(other.CompareTag("Bike"))
        {
            other.transform.GetComponent<BikeSystem>().HurtPlayer(-30);
        }
        else if(other.CompareTag("Obstacle"))
        {
            bulletRigid.Sleep();
            bulletTrans.position = new Vector3(0, -200, 0);
        }
    }
}
