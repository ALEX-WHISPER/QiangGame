using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickBtn : MonoBehaviour {
    public Animator panelShow;
    public GameObject theBag;
    public GameObject chapterContent;

    [HideInInspector]
    public bool isInBtn = false;
    [HideInInspector]
    public bool ifOpenBag = false;

    private GameObject chapterContent_Obj;
    //private GameObject emptyBagObj;

    public void ClickExitBtn() //  点击返回按钮
    {
        panelShow.SetTrigger("showPanel");
        isInBtn = true;
    }

    public void ClickContinue() //  点击继续游戏
    {
        panelShow.SetTrigger("hidePanel");
        isInBtn = false;
    }

    public void ClickBagBtn()   //  点击背包按钮
    {
        theBag.SetActive(true);
        isInBtn = true;
        ifOpenBag = true;
    }

    public void ClickCloseBagBtn()   //  点击背包界面中的关闭按钮
    {
        theBag.SetActive(false);
        isInBtn = false;
        ifOpenBag = false;
    }

    public void ClickChapterBtn()
    {
        chapterContent.SetActive(true);
    }

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
