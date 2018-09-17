using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {

    [Header("BulletManagerValue")]
    public int bulletCount = 6;

    [Header("BulletTransform")]
    public GameObject[] originalPrefab;
    public List<GameObject> objectManager;
    public Transform bullets;

    private static BulletManager bulletManager;
    public static BulletManager bulletManagerCall() { return bulletManager; }

    private void Awake()
    {
        bulletManager = this;
    }

    private void OnDestroy()
    {
        MemoryDelete();
        bulletManager = null;
    }

    void Start()
    {
        SetObject(originalPrefab[0], bulletCount, "Bullet");
    }

    public void SetObject(GameObject setObject, int count, string name)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(setObject) as GameObject;
            obj.transform.name = name;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);
            obj.transform.parent = bullets;
            objectManager.Add(obj);
        }
    }

    public GameObject GetObject(string _Name)
    {
        if (objectManager == null)
            return null;

        int Count = objectManager.Count;
        for (int i = 0; i < Count; i++)
        {
            if (_Name != objectManager[i].name)
                continue;

            GameObject Obj = objectManager[i];

            if (Obj.activeSelf)
            {
                if (i == Count - 1)
                {
                    SetObject(Obj, 1, "Bullet");
                    return objectManager[i + 1];
                }
                continue;
            }
            return objectManager[i];
        }
        return null;
    }

    public void Reload(int _Count)
    {
        for (int i = 0; i < _Count; i++)
        {
            objectManager[i].SetActive(false);
        }
    }

    public void MemoryDelete()
    {
        if (objectManager == null)
            return;

        int Count = objectManager.Count;

        for (int i = 0; i < Count; i++)
        {
            GameObject obj = objectManager[i];
            GameObject.Destroy(obj);
        }
        objectManager = null;
    }
}
