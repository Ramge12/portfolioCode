using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrushSkill : MonoBehaviour {

    [Header("CarSkillValue")]
    [SerializeField] private int carCount = 3;
    [SerializeField] private Transform carTransform;
    [SerializeField] private List<GameObject> carManager;

    private static CarCrushSkill carCrushSkill;
    public static CarCrushSkill call() { return carCrushSkill; }

    private void Awake()
    {
        carCrushSkill = this;
    }

    private void OnDestroy()
    {
        MemoryDelete();
        carCrushSkill = null;
    }

    void Start()
    {
        SetObject(carTransform.gameObject, carCount, "CarCrush");
    }

    public void SetObject(GameObject carObject, int carCount, string carName)
    {
        for (int i = 0; i < carCount; i++)
        {
            GameObject obj = Instantiate(carObject) as GameObject;
            obj.transform.name = carName;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);
            obj.transform.parent = this.transform ;
            carManager.Add(obj);
        }
    }

    public GameObject GetObject(string name)
    {
        if (carManager == null)
            return null;

        int Count = carManager.Count;

        for (int i = 0; i < Count; i++)
        {
            if (name != carManager[i].name)
                continue;

            GameObject obj = carManager[i];

            if (obj.activeSelf)
            {
                if (i == Count - 1)
                {
                    SetObject(obj, 1, "CarCursh");
                    return carManager[i + 1];
                }
                continue;
            }
            return carManager[i];
        }
        return null;
    }

    public void Reload(int number)
    {
        carManager[number].SetActive(false);
    }

    public void MemoryDelete()
    {
        if (carManager == null)
            return;

        int Count = carManager.Count;

        for (int i = 0; i < Count; i++)
        {
            GameObject obj = carManager[i];
            GameObject.Destroy(obj);
        }
        carManager = null;
    }
}
