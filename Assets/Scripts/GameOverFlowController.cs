using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts._base;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverFlowController : UnityBaseBehaviour
{

    public Text ActionText;

    public void UpdateActionText(string s)
    {
        ActionText.text = s;
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public override void Start()
    {
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        UpdateActionText(PlayerPrefs.GetString("lastWinner")+ Environment.NewLine + "wins!");
        yield return new WaitForSeconds(3);
        UpdateActionText("Game Over");
        yield return new WaitForSeconds(3);
        
        SceneManager.LoadScene("OptionsScene");
    }
}
