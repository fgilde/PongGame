using UnityEngine;
using System.Collections;
using Assets.Scripts._base;
using UnityEngine.UI;

public class GameUIController : UnityBaseBehaviour
{
    public Text Player1Name;
    public Text Player2Name;
    public Text Player1Goals;
    public Text Player2Goals;
    public Text ActionText;

    public override void Start()
    {
        Player1Name.text = PlayerPrefs.GetString("Player1Name");
        Player2Name.text = PlayerPrefs.GetString("Player2Name");                
    }

    public void UpdateActionText(string text)
    {
        ActionText.text = text;
    }

    public void UpdatePlayerGoals(string text, bool isPlayer1)
    {
        if(isPlayer1)
            Player1Goals.text = text;
        else
            Player2Goals.text = text;
    }

    
}
