using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
    public int tmp;
    public Text timeText;
    private ScoreManager scoreManeger;
    private GameController gameControl;
    void Start()
    {
        gameControl = GetComponent<GameController>();
        scoreManeger = GetComponent<ScoreManager>();
        //开启一个协程
        StartCoroutine("changeTime");
    }
    void Update()
    {
        timeText.text = tmp.ToString();
    }

    IEnumerator changeTime()
    {
        while (tmp > 0)
        {
            //暂停一秒
            yield return new WaitForSeconds(1);
            tmp--;
        }
        if (tmp == 0)
        {
            gameControl.GameOver(scoreManeger.score);
            GetComponent<Hand>().HandReset();
            //Time.timeScale = 0;
        }
    }
}
