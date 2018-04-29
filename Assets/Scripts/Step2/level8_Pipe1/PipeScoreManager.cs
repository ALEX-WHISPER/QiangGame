using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum GameLevel 
{
    scoreLow, scoreMiddle, scoreHigh
}

public class PipeScoreManager : MonoBehaviour {
    public GameObject resultPanel;
    public Image resultImage;
    public Text resultScoreText;
    public float resultPanelShowDuration;
    public Vector3 resultPanelMoveTo;
    public GameObject nextLevelBtn;

    public Sprite scoreLow_Image;
    public Sprite scoreMiddle_Image;
    public Sprite scoreHigh_Image;

    public int hitCount;
    public int scoreValue;

    public Text scoreText;
    public Text hitText;

    public int shortNoteScore;
    public int longNoteScore;
    public int bothNoteScore;

    public int scoreValue_Low;  //  0-this: Low
    public int scoreValue_Middle;    //  low-this: Middle

    public string thisLevelName;
    public string nextLevelName;

    private bool ifWin = false;
    private GameLevel gameLevel;
    private Image resultPanelImage;

    void Start()
    {
        scoreText.text = scoreValue.ToString();
        hitText.text = hitCount.ToString();
    }

    public void AddHitPoints(bool ifHit)
    {
        if (ifHit)
        {
            hitCount++;
        }
        else
        {
            hitCount = 0;
        }
        hitText.text = hitCount.ToString();
    }

    public void AddScorePoints(string noteType)
    {
        if(noteType == "short")
        {
            scoreValue += shortNoteScore;
        }
        else if(noteType == "long")
        {
            scoreValue += longNoteScore;
        }
        else if(noteType == "both")
        {
            scoreValue += bothNoteScore;
        }
        scoreText.text = scoreValue.ToString();
    }

    public void GameOver()
    {
        GetComponent<CreateNoteWave>().enabled = false;

        //  判断胜负与等级
        if (scoreValue > scoreValue_Low)    //  > 最低分
        {
            ifWin = true;   //  获胜

            if (scoreValue > scoreValue_Middle)     //  中等分段
            {
                gameLevel = GameLevel.scoreHigh;
                resultImage.sprite = scoreHigh_Image;
            }
            else  //    高分段
            {
                gameLevel = GameLevel.scoreMiddle;
                resultImage.sprite = scoreMiddle_Image;
            }
        }
        else  //    <= 最低分，为低分段，游戏失败
        {
            ifWin = false;
            gameLevel = GameLevel.scoreLow;

            resultImage.sprite = scoreLow_Image;
            nextLevelBtn.SetActive(false);
        }

        TweenPosition.Begin(resultPanel, resultPanelShowDuration, resultPanelMoveTo);
        resultScoreText.text = scoreValue.ToString();
    }

    public void ClickReplay()
    {
        GetComponent<EnterNextLevel>().nextLevelName = thisLevelName;
        GetComponent<EnterNextLevel>().enabled = true;
    }

    public void ClickNextLevel()
    {
        GetComponent<EnterNextLevel>().nextLevelName = nextLevelName;
        GetComponent<EnterNextLevel>().enabled = true;
    }
}
