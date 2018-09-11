using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

    [SerializeField] private GameObject gameMenuUI;

    public void MenuButton()
    {
        Time.timeScale = 0.0f;
        gameMenuUI.SetActive(true);
    }

    public void ReturnGame()
    {
        Time.timeScale = 1.0f;
        gameMenuUI.SetActive(false);
    }

    public void ReturnTitle()
    {
        Time.timeScale = 1.0f;
        GameManager.gManagerCall().ReturnToTitle();
    }

    public void ReTryThisGame()
    {
        Time.timeScale = 1.0f;
        GameManager.gManagerCall().ThisSceneReLoad();
    }
}
