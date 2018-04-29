using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PreConversation : Conversation {
    public string markedWordsContent;   //  提示文本
    public Vector2 handPointMapPos;
    public Vector2 handPointBagPos;

    public GameObject guideHand;    //  指引手(含动画)，点击指定物体后消失
    public GameObject bagPanel;         //  背包界面(含地图)
    public GameObject bagPanel_Copy;    //  背包界面(空)
    public GameObject mapPanel;     //  大地图界面

    private bool updateCtrl = false;    //  Update 函数的间隔布尔量
    private GameObject guideHandObj;    //  实例化的指引手所复制的物体

    //  监测此时鼠标是否悬浮在按钮范围内，若是，则点击后只触发按钮事件，不触发对话内容，否则表明鼠标在可点击的屏幕范围中，则可以触发对话内容
    private bool isInBtn = false;

	new void Awake()
	{
		base.Awake ();
	}

	protected override void Start()
	{
		count = -1;
		base.Start ();

        markedWords.text = markedWordsContent;
	}

	protected override void Update()
	{
        if (isAlreadyTalk && !updateCtrl)   //  若对话已触发完毕
        {
            //  实例化指引手图标，并复制为一自定义的游戏物体. 另，为防止 Update() 中无限实例化(因为每一帧都执行)，故用 updateCtrl 作为间隔量使实例化操作只执行一次
            guideHandObj = Instantiate(guideHand) as GameObject;
            guideHandObj.transform.position = handPointBagPos;
            updateCtrl = true;
        }

        //  若实例化指引手操作完成(间接表明对话已触发完毕)，或鼠标在按钮范围内，则鼠标点击时，将不会触发对话
        else if ((isAlreadyTalk && updateCtrl) || isInBtn)
            return;
		base.Update ();
	}

    public void ShowBagPanel()  //  点击背包按钮，打开背包界面
    {
        isInBtn = true;     //  当背包界面打开时，鼠标只在背包界面点击有响应，在背包外的屏幕位置点击无响应

        if (!isAlreadyTalk)     //  若未等对话完毕就点击背包按钮，则打开的是空的背包
        {
            bagPanel_Copy.SetActive(true);
        }
        else   //   若对话完毕后点击背包按钮，则打开的是有地图的背包，并将指引手销毁掉
        {
            //Destroy(guideHandObj);
            bagPanel.SetActive(true);

            if(guideHandObj == null)
            {
                guideHandObj = Instantiate(guideHand) as GameObject;
            }

            guideHandObj.transform.position = new Vector2(handPointMapPos.x, handPointMapPos.y);
            guideHand.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
    }

    public void CloseBagPanel()     //  点击背包界面的关闭按钮，关闭背包
    {
        isInBtn = false;

        if (!isAlreadyTalk)
            bagPanel_Copy.SetActive(false);

        bagPanel.SetActive(false);
        Destroy(guideHandObj);
    }

    public void ShowMapPanel()      //  点击背包中的地图道具，打开地图
    {
        mapPanel.SetActive(true);
        bagPanel.SetActive(false);
        Destroy(guideHandObj);
        GetComponent<EnterNextLevel>().enabled = true;
    }

    public void MouseEnterBtns()    //  鼠标进入各按钮范围
    {
        isInBtn = true;
    }

    public void MouseExitBtns()     //  鼠标移除按钮范围
    {
        isInBtn = false;
    }
}
