using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3Quest : MonoBehaviour {

    [SerializeField] private GameObject BossUI;
    [SerializeField] private GameObject questUI;

    [Header("Quest3 UI")]
    [SerializeField] private Text questTitle;
    [SerializeField] private Text questInfo;
    [SerializeField] private Image questClearUI;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Button clearButton;

    private bool clear = false;

    private void Awake()
    {
        StartCoroutine(WaitingUI());
    }

    public void StartUI()
    {
        BossUI.SetActive(true);
        questUI.SetActive(true);
    }

    IEnumerator WaitingUI()
    {
        yield return new WaitForSeconds(2f);
        StartUI();
        float backGroudAlpha = backgroundImage.color.a;
        while (true)
        {
            backGroudAlpha -= Time.deltaTime*0.5f;
            if(backGroudAlpha<=0)
            {
                backGroudAlpha = 0;
                break;
            }
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, backGroudAlpha);
            questTitle.color = new Color(questTitle.color.r, questTitle.color.g, questTitle.color.b, backGroudAlpha);
            questInfo.color = new Color(questInfo.color.r, questInfo.color.g, questInfo.color.b, backGroudAlpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(2f);
        questUI.SetActive(false);
        yield return null;
    }

    public void QuestClearUI()
    {
        if(!clear)StartCoroutine(StartClear());
        clear = true;
    }

    IEnumerator StartClear()
    {
        BossUI.SetActive(false); 
        questClearUI.gameObject.SetActive(true);
        float backGroudAlpha =0;
        while (true)
        {
            backGroudAlpha += Time.deltaTime*0.5f;
            if (backGroudAlpha >= 1)
            {
                backGroudAlpha = 1;
                break;
            }
            questClearUI.color = new Color(questClearUI.color.r, questClearUI.color.g, questClearUI.color.b, backGroudAlpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        clearButton.gameObject.SetActive(true);
        yield return null;
    }
}
