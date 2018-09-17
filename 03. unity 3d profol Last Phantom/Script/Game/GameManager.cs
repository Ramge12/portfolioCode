using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [Header("GameManager Value")]
    [SerializeField] private int delayTime = 3;
    [SerializeField] private int gameFrame = 50; 
    [SerializeField] private Image progressBar;

    private static int sceneCount = 0;
    private static GameManager gManager;
    public static GameManager gManagerCall() { return gManager; }

    private void Awake()
    {
        Application.targetFrameRate = gameFrame;

        gManager = this;

       if(SceneManager.GetActiveScene().name== "LoadingScene")
       {
           StartCoroutine(LoadScene()); 
       }
    }

    private void OnDestroy()
    { 
        gManager = null;
    }

    public void NextSceneLoad()
    {
        sceneCount++;
        StartCoroutine(SceneMove(delayTime));
    }

    public void ThisSceneReLoad()
    {
        StartCoroutine(SceneMove(0));
    }

    public void ReturnToTitle()
    {
        sceneCount = 0;
        StartCoroutine(SceneMove(0));
    }

    IEnumerator SceneMove(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LoadingScene");
        yield return null;
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneCount); //비동기적 연산 코루틴 AsyncOperation
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)  //연산이 완료되떄까지
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)    //연산의 진행상황
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                    op.allowSceneActivation = true; //연산을 마치고 씬을 활성화
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
        }
    }
}
