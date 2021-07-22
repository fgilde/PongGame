using UnityEngine;
using System.Collections;
using Assets.Scripts._base;
using UnityEngine.SceneManagement;

public class MainMenuController : UnityBaseBehaviour
{

    public GameObject StartPageUI;
    public GameObject PlayPageUI;
    public GameObject OptionsPageUI;

    public override void Awake()
    {
        ShowStartPage();
    }

    public void ShowStartPage()
    {
        ShowPage(StartPageUI);
    }

    public void ShowPlayPage()
    {
        ShowPage(PlayPageUI);
    }

    public void ShowOptionsPage()
    {
        ShowPage(OptionsPageUI);
    }

    public void PlayGame()
    {
                
        SceneManager.LoadScene("GamePlayScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    private void ShowPage(GameObject page)
    {
        StartPageUI.SetActive(false);
        PlayPageUI.SetActive(false);
        OptionsPageUI.SetActive(false);
        page.SetActive(true);
    }

}
