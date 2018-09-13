using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Web : MonoBehaviour {

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform webTarget;
    [SerializeField] private Transform webCurTarget;
    private LineRenderer lineRenderer;
    private bool webOn;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(1, playerTransform.position);
        webCurTarget = webTarget;
    }

    public void StartSkill()
    {
        webOn = true;
        StartCoroutine(WebSkillSystem());
    }

    public void EndSkill()
    {
        webOn = false;
        lineRenderer.enabled = false;
    }

    IEnumerator WebSkillSystem()
    {
        while (webOn)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit)) lineRenderer.enabled = true;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                lineRenderer.enabled = false;
            }

            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPosition(1, playerTransform.position);
            lineRenderer.SetPosition(0, webCurTarget.position);
            yield return new WaitForSeconds(Time.deltaTime * 0.1f);
        }
        yield return null;
    }
}
