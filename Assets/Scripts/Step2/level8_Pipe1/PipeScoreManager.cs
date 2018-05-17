using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum GameLevel 
{
    scoreLow, scoreMiddle, scoreHigh
}

public class PipeScoreManager : MonoBehaviour {
    public GameObject resultPanel;  //  游戏结果界面
    public Image resultImage;       //  根据分段高低替换显示图片
    public Text resultScoreText;    //  最终分数
    public float resultPanelShowDuration;   //  resultPanel移动至目标位置处需经历的时间
    public Vector3 resultPanelMoveTo;       //  resultPanel出现后运动的目标位置
    public GameObject nextLevelBtn;     //  进入下一关按钮

#region ResultImage sprites
    public Sprite scoreLow_Image;
    public Sprite scoreMiddle_Image;
    public Sprite scoreHigh_Image;
#endregion

    public int hitCount;    //  连击数
    public int scoreValue;  //  当前分数

    public Text scoreText;  //  当前分数UI
    public Text hitText;    //  连击数UI

    public int shortNoteScore;  //  击中短音符获得的分数
    public int longNoteScore;   //  击中长音符获得的分数
    public int bothNoteScore;   //  击中双音符获得的分数

    public int scoreValue_Low;  //  0-this: Low
    public int scoreValue_Middle;    //  low-this: Middle

    public string thisLevelName;
    public string nextLevelName;

    private bool ifWin = false;
    private GameLevel gameLevel;
    private Image resultPanelImage;

    void Start()
    {
        //  初始化分数、连击数UI
        scoreText.text = scoreValue.ToString();
        hitText.text = hitCount.ToString();
    }

    //  判断连击数
    public void AddHitPoints(bool ifHit)
    {
        //  若击中，连击数累加
        if (ifHit)
        {
            hitCount++;
        }

        //  若未击中，连击数清零
        else
        {
            hitCount = 0;
        }

        //  更新连击数UI
        hitText.text = hitCount.ToString();
    }

    //  击中音符后，根据击中的音符类型加分
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

    //  倒计时结束，游戏结束
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

        //  显示游戏结果界面
        TweenPosition.Begin(resultPanel, resultPanelShowDuration, resultPanelMoveTo);

        //  最终分数
        resultScoreText.text = scoreValue.ToString();
    }

    //  重玩
    public void ClickReplay()
    {
        GetComponent<EnterNextLevel>().nextLevelName = thisLevelName;
        GetComponent<EnterNextLevel>().enabled = true;
    }

    //  进入下一关
    public void ClickNextLevel()
    {
        GetComponent<EnterNextLevel>().nextLevelName = nextLevelName;
        GetComponent<EnterNextLevel>().enabled = true;
    }
}
