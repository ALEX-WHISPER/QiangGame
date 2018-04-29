using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTestToEnterDoor : MonoBehaviour {
    public CamFollowOnY camMove;    //  相机移动脚本
    public Animator playerAnim;

    public GameObject guideHand;    //  指引手
    public Vector2 guideHandOnTestPanelPos;    //  指引手出现在题版上的位置
    public Vector2 guideHandOnDoorPos;      //  指引手出现在门上的位置
    public float guideHandShowDealy;    //  指引手出现的延迟时长

    public string NPCWordsTxt;      //  NPC 提示性内容的文本
    public GameObject NPCWordsPan;  //  NPC 提示内容板块
    
    public GameObject testPanel;        //  题版
    public GameObject testPanelMask;    //  题版背景遮罩
    public TheTestProcess[] tests;      //  题目

    public float durationTime;      //  TweenScale 的持续时间
    public Vector2 testPanelTweenScale;     //  testPanel 的目标 scale

    public GameObject theDoor;
    public Sprite door_opened;

    private bool updateCtrl = true;
    private bool updateCtrl1 = true;
    private GameObject guideHand_Obj;
    private bool ifCanClickTestPanel = false;
    private bool ifCanClickTheDoor = false;
    private bool ifAllTestsRight = false;
    void Update()
    {
        if (camMove.ifMoveDone && updateCtrl)
        {
            //  NPC 提示文字出现
            NPCWordsPan.SetActive(true);
            NPCWordsPan.GetComponentInChildren<Text>().text = NPCWordsTxt;

            //  指引手出现
            StartCoroutine(ShowGuideHand(guideHandOnTestPanelPos, guideHandShowDealy));
            ifCanClickTestPanel = true;

            updateCtrl = false;
        }

        if (ifAllTestsRight && updateCtrl1)
        {
            //  TestPanael 消失
            testPanel.GetComponent<Animator>().SetBool("ifCanHide", true);
            testPanelMask.SetActive(false);
            Invoke("HideTestPanel", durationTime);

            //  Card 出现
            GetComponent<GetCard>().ShowCard();

            //  门开
            theDoor.GetComponent<SpriteRenderer>().sprite = door_opened;
            StartCoroutine(ShowGuideHand(guideHandOnDoorPos, guideHandShowDealy));
            ifCanClickTheDoor = true;

            updateCtrl1 = false;
        }
    }

    void HideTestPanel()
    {
        testPanel.SetActive(false);
    }

    IEnumerator ShowGuideHand(Vector2 guideHandShowPos, float delay)
    {
        yield return new WaitForSeconds(delay);
        guideHand_Obj = Instantiate(guideHand) as GameObject;
        guideHand_Obj.transform.position = guideHandShowPos;
    }

    public void ClickOpenTestPanel()    //  点击位置告示栏上的指引手后，出现题版
    {
        if (!ifCanClickTestPanel || GetComponent<ClickBtn>().ifOpenBag) return;

        Destroy(guideHand_Obj);
        NPCWordsPan.SetActive(false);

        testPanel.SetActive(true);
        testPanelMask.SetActive(true);
        
        ifCanClickTestPanel = false;
    }

    public void ClickToEnterTheDoor()   //  点击位于门处的指引手，播放人物进门动画
    {
        if (!ifCanClickTheDoor || GetComponent<ClickBtn>().ifOpenBag || GetComponent<GetCard>().ifCardShowing) return;

        Destroy(guideHand_Obj);

        playerAnim.SetBool("EnterTheDoor", true);

        GetComponent<EnterNextLevel>().enabled = true;
        
        ifCanClickTheDoor = false;
    }

   public void TestResultCheck(int theRightTest_id)
    {
        for (int i = 0; i < tests.Length; ++i)
        {
            if (i == theRightTest_id) continue;
            if (!tests[i].ifChosenRight) return;
        }
        ifAllTestsRight = true;
    }
}
