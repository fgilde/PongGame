using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts._base;
using UnityEngine.UI;

public class PlayPagePreferences : UnityBaseBehaviour
{

    public Text Player1NameText;
    public Text Player2NameText;
    public InputField InputPlayer1;
    public InputField InputPlayer2;
    public InputField InputPointsToWin;
    public Toggle ActiveMiddleLineToggle;
    public Toggle AiToggle;

    public override void Awake()
    {
        if (PlayerPrefs.HasKey("Player1Name") && !string.IsNullOrEmpty(PlayerPrefs.GetString("Player1Name")))
            Player1NameText.text = PlayerPrefs.GetString("Player1Name");
        if (PlayerPrefs.HasKey("Player2Name") && !string.IsNullOrEmpty(PlayerPrefs.GetString("Player2Name")))
            Player2NameText.text = PlayerPrefs.GetString("Player2Name");
        if (PlayerPrefs.HasKey("isAI"))
            AiToggle.isOn = PlayerPrefs.GetString("isAI") == "true";
        if (PlayerPrefs.HasKey("ActiveMiddleLine"))
            ActiveMiddleLineToggle.isOn = PlayerPrefs.GetString("ActiveMiddleLine") == "true";
        if (PlayerPrefs.HasKey("PointsToWin"))
            InputPointsToWin.text = PlayerPrefs.GetInt("PointsToWin").ToString();        
    }

    public void ChangePlayer1Name(string s)
    {        
        s = InputPlayer1.text;        
        Player1NameText.text = s;
        PlayerPrefs.SetString("Player1Name", s);
    }

    public void ChangePlayer2Name(string s)
    {
        s = InputPlayer2.text;
        Player2NameText.text = s;
        PlayerPrefs.SetString("Player2Name", s);
    }

    public void ChangePointsToWin(string s)
    {
        s = InputPointsToWin.text;
        int points;
        if (!int.TryParse(s, out points))
            points = 10;
        PlayerPrefs.SetInt("PointsToWin", points);
    }

    public void toggleMiddleLine()
    {        
        PlayerPrefs.SetString("ActiveMiddleLine", ActiveMiddleLineToggle.isOn.ToString().ToLower());
    }

    public void toggleAI()
    {
        ChangeAI(AiToggle.isOn);
    }

    public void ChangeAI(bool isAI)
    {
        //AiToggle.isOn = isAI;
        PlayerPrefs.SetString("isAI", isAI.ToString().ToLower());
    }
}
