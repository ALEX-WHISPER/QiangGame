using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public Mole[] allMoles;
    [Range(0f, 1f)]
    public float niceGuyRate = 0.5f;

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

    private void Start() {
        MolesPropertiesManager();
    }

    private void MolesPropertiesManager() {
        //  set all moles' niceGuyRate
        for (int i = 0; i < allMoles.Length; i++) {
            allMoles[i].NiceGuyRate = this.niceGuyRate;
        }
    }

    public void GameOver(int finalScore)
    {
        ifGameOver = true;

        //  update the score value on game over
        score = finalScore;
        scoreNumberText.text = score.ToString();

        //  show the result panel
        TweenPosition.Begin(resultPanel, resultPanelMoveDuration, pos_resultPanelMoveTo);

        //  if won, set the text content and color, then show the card
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

    //  replay
    public void ClickRePlay()
    {
        Replay();
    }
    
    //  go to next level
    public void ClickContinue()
    {
        //  unable to continue if you lose
        if (!ifWin) return;
        EnterNextLevel();
    }

    //  reload this scene
    void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(thisLevelName);
    }

    //  load next scene
    void EnterNextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextLevelName);
    }

    void InvokeShowCard()
    {
        GetComponent<GetCard>().ShowCard();
    }

    #region TEST
    public void ForeToEnterNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}
