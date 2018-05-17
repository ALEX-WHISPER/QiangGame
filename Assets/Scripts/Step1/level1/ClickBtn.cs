using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickBtn : MonoBehaviour {
    public Animator panelShow;  //  setting panel on click exit btn
    public GameObject theBag;   //  bag panel
    public GameObject chapterContent;   //  the introducing panel at the start point

    [HideInInspector]
    public bool isInBtn = false;
    [HideInInspector]
    public bool ifOpenBag = false;
    
    //  点击返回按钮
    public void ClickExitBtn() 
    {
        panelShow.SetTrigger("showPanel");
        isInBtn = true;
    }

    //  点击继续游戏
    public void ClickContinue()
    {
        panelShow.SetTrigger("hidePanel");
        isInBtn = false;
    }

    //  点击背包按钮
    public void ClickBagBtn()
    {
        theBag.SetActive(true);
        isInBtn = true;
        ifOpenBag = true;
    }

    //  点击背包界面中的关闭按钮
    public void ClickCloseBagBtn()
    {
        theBag.SetActive(false);
        isInBtn = false;
        ifOpenBag = false;
    }

    //  click the introducing board at the start point
    public void ClickChapterBtn()
    {
        chapterContent.SetActive(true);
    }

    //  close the introducing board
    public void ClickCloseChapterBtn()
    {
        chapterContent.SetActive(false);
    }

    public void MouseInBtn()    //  鼠标悬停在按钮上或背包界面中时，按下鼠标不触发人物行走事件
    {
        isInBtn = true;
    }

    public void MouseExitBtn()
    {
        isInBtn = false;
    }
}
