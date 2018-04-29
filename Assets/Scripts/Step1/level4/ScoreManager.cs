using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public int score = 0;
    private int previousScore = 0;
    public Text scoreText;

    void Update()
    {
        if (previousScore != score)
        {
            scoreText.text = "得分 ：" + score;
            previousScore = score;
        }
    }
}
