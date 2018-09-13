using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScarabSpawnPoint : MonoBehaviour {

    [Header("ScrabSpawn Tansform")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform questUI;
    [SerializeField] private GameObject[] Origin;
    [SerializeField] private List<GameObject> objectManager;
    [SerializeField] private Camera mainCamera;
    [Header("ScrabSpawn Value")]
    [SerializeField] private int scrabCount = 10;
    [SerializeField] private int maxSpawn = 3;

    [Header("ScrabSpawn UI")]
    [SerializeField] private Text countText;
    [SerializeField] private Text countInfo;

    [Header("ScrabSpawn Scripts")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerInventory playerInventory;

    private int scarabCount;
    private string scarabName;
    private Transform spawnPoint;

    static ScarabSpawnPoint scarabSpawnPoint;
    public static ScarabSpawnPoint scarabSpawnPointCall() { return scarabSpawnPoint; }

    private void Awake()
    {
        scarabSpawnPoint = this;
        spawnPoint = GetComponent<Transform>();
        SetObject(Origin[0], scrabCount, scarabName);
    }

    private void OnDestroy()
    {
        MemoryDelete();
        scarabSpawnPoint = null;
    }

    void SetObject(GameObject gameObject, int count, string objectName)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(gameObject) as GameObject;
            obj.transform.name = objectName + i.ToString();
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);
            obj.transform.parent = spawnPoint;
            objectManager.Add(obj);
        }
    }

    GameObject GetObject(string objectName)
    {
        if (objectManager == null)
            return null;

        int count = objectManager.Count;

        for (int i = 0; i < count; i++)
        {
            if (objectName != objectManager[i].name)
                continue;

            GameObject obj = objectManager[i];

            if (obj.activeSelf)
            {
                if (i == count - 1)
                {
                    SetObject(obj, 1, scarabName);
                    return objectManager[i + 1];
                }
                continue;
            }
            return objectManager[i];
        }
        return null;
    }

    void MemoryDelete()
    {
        if (objectManager == null)
            return;

        int count = objectManager.Count;

        for (int i = 0; i < count; i++)
        {
            GameObject obj = objectManager[i];
            GameObject.Destroy(obj);
        }
        objectManager = null;
    }

    int CountScarab()
    {
        int count = 0;
        for (int i = 0; i < scrabCount; i++)
        {
            if (objectManager[i].activeSelf)
            {
                count++;
            }
        }
        return count;
    }

    public IEnumerator StartSpawn()
    {
        int countNum = 0;
        questUI.gameObject.SetActive(true);

        while (true)
        {
            if (countNum == 10) break;

            if (CountScarab()< maxSpawn)
            {
                GameObject sacrab = GetObject(scarabName + countNum.ToString());
                sacrab.SetActive(true);
                sacrab.transform.position = spawnPoint.position + spawnPoint.forward * 2;
                sacrab.transform.GetComponent<ScarabController>().nav.Warp(spawnPoint.position + spawnPoint.forward * 2);
                sacrab.transform.GetComponent<ScarabController>().targetRange.target = player;
                sacrab.transform.GetComponent<ScarabController>().sacrabSpawnPoint = this;
                sacrab.transform.GetComponent<ScarabController>().player = player;
                sacrab.transform.GetComponent<ScarabController>().SetTextCamera(mainCamera.transform.GetComponent<CameraController>());
                countNum++;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    public void CountUI()
    {
        scarabCount++;
        countText.text = scarabCount + "/10";

        if(scarabCount==10)
        {
            countText.gameObject.SetActive(false);
            countInfo.text = "퀘스트가 완료되었습니다. 다음 스테이지로 넘어값니다";
            GameManager.gManagerCall().NextSceneLoad();
            playerInventory.Save();
            playerStats.Save();
        }
    }

}
