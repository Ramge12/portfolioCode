using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRange : MonoBehaviour {

    public Transform target;
    public bool tragetInRange;
    private Transform thisTrasnform;

    [SerializeField] private float distance;
    [SerializeField] private float searchRange;

    private void Start()
    {
        thisTrasnform = GetComponent<Transform>();
    }

    private void Update()
    {
        distance = Vector3.Distance(target.position, thisTrasnform.position);

        if (distance < searchRange) tragetInRange = true;
        else tragetInRange = false;
    }
}
