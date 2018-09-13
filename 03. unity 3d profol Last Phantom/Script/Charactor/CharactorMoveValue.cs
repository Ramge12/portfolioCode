using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharactorMoveValue {

    [Header ("-Charactor Move Value")]
    public float moveSpeed;
    public float turnSpeed;
    public float gravity;
    public float jumpPower;
    public float charactorHeight;

    public Vector3 InputVector()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveVector = new Vector3(horizontal, 0, vertical);
        moveVector = moveVector.normalized;
        return moveVector;
    }

    public bool InputPressCheck()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float InputRotationY()
    {
        float rotationY = Input.GetAxisRaw("Horizontal") * turnSpeed * Time.deltaTime;
        return rotationY;
    }
}
