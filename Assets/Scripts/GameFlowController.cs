using System.Collections;
using Assets.Scripts._base;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlowController : UnityBaseBehaviour
{
    public int SecondsToWaitBeforeStart = 3;
    public BallBehavior BallBehavior;
    public GameUIController UiController;
    public SoundController Sounds;

    private int maxGoals;
    int player1Goals = 0;
    int player2Goals = 0;

    public override void Start()
    {
        maxGoals = PlayerPrefs.HasKey("PointsToWin") ? PlayerPrefs.GetInt("PointsToWin") : 10;
        GameObject.Find("PointText").GetComponent<Text>().text = maxGoals.ToString();
        bool middleLine = PlayerPrefs.HasKey("ActiveMiddleLine") && PlayerPrefs.GetString("ActiveMiddleLine") == "true";
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("MiddleLine"))
        {
            var middleLineBehavior = o.GetComponent<MiddleLineBehavior>();
            middleLineBehavior.IsActive = middleLine;
            if(!middleLine)
                middleLineBehavior.SetEnabled(false);
        }

        StartCoroutine(StartNewRound(true));
    }

    public IEnumerator GoalPlayer1()
    {
        return OnGoal(true);
    }

    public IEnumerator GoalPlayer2()
    {
        return OnGoal(false);
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    private IEnumerator OnGoal(bool isPlayer1)
    {
        if (isPlayer1)
            player1Goals++;
        else
            player2Goals++;

        UiController.UpdatePlayerGoals(isPlayer1 ? player1Goals.ToString() : player2Goals.ToString(), isPlayer1);
        if (HasFinished())
        {
            string playerName = PlayerPrefs.GetString((isPlayer1 ? "Player1" : "Player2") + "Name");
            PlayerPrefs.SetString("lastWinner", string.IsNullOrEmpty(playerName) ? isPlayer1 ? "Player 1" : "Player 2" : playerName);
            SceneManager.LoadScene("GameOver");
        }
        BallBehavior.ResetBall(isPlayer1);
        yield return StartCoroutine(GoalAnimation());
        StartCoroutine(StartNewRound(isPlayer1));
    }

    private IEnumerator StartNewRound(bool isPlayer1)
    {
        yield return StartCoroutine(StartAnimation());
        BallBehavior.StartBallMovement(isPlayer1);
    }

    private IEnumerator StartAnimation()
    {
        var s = SecondsToWaitBeforeStart;
        while (s > 0)
        {
            Sounds.WallSound.Play();
            UiController.UpdateActionText(s.ToString());
            yield return new WaitForSeconds(1);
            s--;
        }
        UiController.UpdateActionText("PONG");
        yield return new WaitForSeconds(1);
        UiController.UpdateActionText(string.Empty);
    }

    private IEnumerator GoalAnimation()
    {
        UiController.UpdateActionText("GOAL!!");
        yield return new WaitForSeconds(3);
    }

    private bool HasFinished()
    {
        return player1Goals == maxGoals || player2Goals == maxGoals;
    }

}
