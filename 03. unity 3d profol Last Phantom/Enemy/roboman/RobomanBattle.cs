using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobomanBattle : MonoBehaviour {

    [Header("RoboBattle Value")]
    [SerializeField] private int bulletNumber = 0;
    [SerializeField] private float shootPower = 3;
    [SerializeField] private float reloadDelayTime = 2.0f;
    [SerializeField] private AudioSource gunShotSound;


    [Header("RoboBattle Transform")]
    [SerializeField] private Transform[] bullets;
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private GameObject shootEffect;

    public bool reload = false;
    public bool roboBattle = false;

    public IEnumerator ShootStart(Transform playerTransform, Transform roboTransform)
    {
        roboBattle = true;

        while (true)
        {
            shootEffect.SetActive(true);
            reload = false;

            Vector3 shootDir = (playerTransform.position - weaponPosition.position ).normalized;
            Rigidbody bulletRigid = bullets[bulletNumber].GetComponent<Rigidbody>();

            bulletRigid.Sleep();

            bullets[bulletNumber].position = weaponPosition.position;
            bulletRigid.AddForce(shootDir * shootPower);
            if(!gunShotSound.isPlaying) gunShotSound.Play();
            if (bulletNumber == 5)  //reload
            {
                shootEffect.SetActive(false);
                reload = true;
                bulletNumber = 0;
                yield return new WaitForSeconds(reloadDelayTime);
            }
            else
            {
                bulletNumber++;
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return null;
    }

    public void StopShooting()
    {
        roboBattle = false;
        StopAllCoroutines();
    }
}
