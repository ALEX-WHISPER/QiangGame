using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject resultPanel;
    public Text scoreNumberText;
    public Text resultText;
    public float resultPanelMoveDuration;
    public Vector3 pos_resultPanelMoveTo;
    public float showCardDelay = 0;

    public string win_Str;
    public string fail_Str;
    public string thisLevelName;
    public string nextLevelName;

    public int minScoreForWin;
    public bool ifGameOver = false;
    private int score;
    private bool ifWin = false;

    //public void GameOver(int finalScore)
    public void GameOver(int finalScore)
    {
        ifGameOver = true;
        score = finalScore;
        TweenPosition.Begin(resultPanel, resultPanelMoveDuration, pos_resultPanelMoveTo);
        scoreNumberText.text = score.ToString();

        if (score >= minScoreForWin)
        {
            ifWin = true;
            resultText.text = win_Str;
            resultText.color = Color.red;

            Invoke("InvokeShowCard", showCardDelay);
        }
        else
        {
            ifWin = false;
            resultText.text = fail_Str;
            resultText.color = Color.black;
        }
    }

    public void ClickRePlay()
    {
        Replay();
    }

    public void ClickContinue()
    {
        if (!ifWin) return;
        EnterNextLevel();
    }

    void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(thisLevelName);
    }
    void EnterNextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextLevelName);
    }

    void InvokeShowCard()
    {
        GetComponent<GetCard>().ShowCard();
    }
}
