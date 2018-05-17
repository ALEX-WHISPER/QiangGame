using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LatConversation : Conversation
{
    public GameObject comicObj;
    public GameObject hitMoleRuleObj;
    public GameObject[] comics;
    public Vector3[] btn_PageTurnPos;
    public float comicPageTranDuration;

    public float comicShowDuration;
    public Vector3 comicShowScale;
    public Vector3 ruleShowScale;
    public string nextLevelName;
    private bool ifConTrigger = true;
    private bool ifHitMoleRuleTrigger = false;
    private bool nextPageForm = true;
    new void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        count = -1;
        base.Start();
    }

    protected override void Update()
    {
        if (isAlreadyTalk || !ifConTrigger || GetComponent<ClickBtn>().ifOpenBag) return;
        if (comicObj.activeSelf) ifConTrigger = false;
        if (count == 2 * NPCWords.Count - 1) ifHitMoleRuleTrigger = true;
        base.Update();
    }

    public void ClickComicBtn()     //  点击播放按钮出现漫画界面
    {
        if (!ifHitMoleRuleTrigger)
        {
            comicObj.SetActive(true);
            TweenScale.Begin(comicObj, comicShowDuration, comicShowScale);
        }
    }
    public void MouseEnterBtn()
    {
        ifConTrigger = false;
    }

    public void MouseExitBtn()
    {
        ifConTrigger = true;
    }

    public void ClickTurnPage()     //  点击翻页按钮
    {
        if (comics[0].activeSelf == true)   //  第一张漫画为激活状态
        {  
            //  set position of btn_TurnPage
            GameObject.Find("TurnPage").transform.position = btn_PageTurnPos[1];

            //  deactivate the first page, which tends to display the second page
            comics[0].SetActive(false);
        }
        else
        {
            //  set position of btn_TurnPage
            GameObject.Find("TurnPage").transform.position = btn_PageTurnPos[0];

            //  activate the first page, which tends to block the second page
            comics[0].SetActive(true);
        }
        ChangeTurnPageBtnForm();
    }

    //  reverse scale.x to change the arrow direction
    void ChangeTurnPageBtnForm()
    {
        Vector2 theScale = GameObject.Find("TurnPage").transform.localScale;
        theScale.x *= -1;
        GameObject.Find("TurnPage").transform.localScale = theScale;
    }
    public void ClickCloseComicBtn()    //  关闭漫画界面
    {
        comicObj.SetActive(false);
        ifConTrigger = true;
    }

    public void ClickHitMoleRule()
    {
        if (ifHitMoleRuleTrigger) 
        {
            hitMoleRuleObj.SetActive(true);
            TweenScale.Begin(hitMoleRuleObj, comicShowDuration, ruleShowScale);
        }
    }

    public void ClickPlayHitMoleGame()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
