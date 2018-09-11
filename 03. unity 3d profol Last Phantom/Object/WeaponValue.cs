using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponValue : MonoBehaviour {

    public int damage;
    public Transform playerTransform;

    [SerializeField] private CapsuleCollider weaponCollider;
    [SerializeField] private TrailRenderer weaponTrailRender;
    [SerializeField] private CameraController cameraController;

    public void WeaponAttackStart()
    {
        weaponCollider.enabled = true;
        weaponTrailRender.enabled = true;
    }

    public void WeaponAttackEnd()
    {
        weaponCollider.enabled = false;
        weaponTrailRender.enabled = false;
    }
}
