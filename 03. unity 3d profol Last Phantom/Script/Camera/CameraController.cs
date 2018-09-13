using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    [SerializeField] private Transform target;

    [Header("-Rotation Value")]
    [SerializeField] private float rotX;
    [SerializeField] private float yMinRot;
    [SerializeField] private float yMaxRot;
    [SerializeField] private float smoothRotate;

    [Header("-Postion Value")]
    [SerializeField] private float dist;
    [SerializeField] private float height;
    [SerializeField] private float minDist;
    [SerializeField] private float maxDist;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    [Header("-Speed Value")]
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    [SerializeField] private Image bloodUI;

    [System.NonSerialized] public float magnification = 1.0f;  //배율
    [System.NonSerialized] public float x = 0.0f;
    [System.NonSerialized] public float y = 0.0f;

    private bool shakeCamera;
    private float originAlpha;
    private Vector3 cameraNextPosition;
    private Transform cameraTransfrom;

    void Start()
    {
        originAlpha = bloodUI.color.a;
        Vector3 angles = transform.eulerAngles;
        cameraTransfrom = GetComponent<Transform>();
        cameraTransfrom.position = target.position;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target)
        {
            float mag_dist = dist * magnification;
            float mag_height = height * magnification;
            float mag_minDist = minDist * magnification;
            float mag_maxDist = maxDist * magnification;
            float mag_minHeight = minHeight * magnification;
            float mag_maxHeight = maxHeight * magnification;

            if (Input.GetKey(KeyCode.Mouse1))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
                y = ClampRange(y, yMinRot, yMaxRot);
            }

            if (Input.GetKey(KeyCode.Mouse2))
            {
                x = 0;
                y = 0;
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                dist -= 0.5f * Input.mouseScrollDelta.y;
                dist = ClampRange(dist, minDist, maxDist);
                mag_dist = ClampRange(mag_dist, mag_minDist, mag_maxDist);

                height -= 0.5f * Input.mouseScrollDelta.y;
                height = Mathf.Clamp(height, minHeight, maxHeight);
                mag_height = Mathf.Clamp(mag_height, mag_minHeight, mag_maxHeight);
            }

            Quaternion rotation = Quaternion.Euler(y + rotX + target.localEulerAngles.x, x + target.localEulerAngles.y, target.localEulerAngles.z);
            cameraNextPosition = rotation * new Vector3(0, mag_height, -mag_dist) + target.position;

            if(!shakeCamera)cameraTransfrom.position = Vector3.Lerp(cameraTransfrom.position, cameraNextPosition, Time.deltaTime * 1000);
            cameraTransfrom.rotation = rotation;

            if(Input.GetKeyDown(KeyCode.U)) StartCoroutine(ShakeCamera(0.3f,0.3f));
        }
    }

    float ClampRange(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public IEnumerator ShakeCamera(float amount, float time)
    {
        float timer = 0;
        bloodUI.gameObject.SetActive(true);
        bloodUI.color = new Color(bloodUI.color.r, bloodUI.color.g, bloodUI.color.b, originAlpha);
        float boolAlpha = bloodUI.color.a;
        while (true)
        {
            boolAlpha -= Time.deltaTime*2;
            bloodUI.color = new Color(bloodUI.color.r, bloodUI.color.g, bloodUI.color.b, boolAlpha);
            shakeCamera = true;
            transform.localPosition = (Vector3)Random.insideUnitCircle * amount + cameraNextPosition;
            timer += Time.deltaTime;
            if (timer > time) break;
            yield return Time.deltaTime;
        }
        shakeCamera = false;
        bloodUI.gameObject.SetActive(false);
        transform.localPosition = cameraNextPosition;
        yield return null;
    }

    public IEnumerator FootStepCamera(float amount, float time)
    {
        float timer = 0;
        while (true)
        {
            shakeCamera = true;
            transform.localPosition = (Vector3)Random.insideUnitCircle * amount + cameraNextPosition;
            timer += Time.deltaTime;
            if (timer > time) break;
            yield return Time.deltaTime;
        }
        shakeCamera = false;
        transform.localPosition = cameraNextPosition;
        yield return null;
    }
}
