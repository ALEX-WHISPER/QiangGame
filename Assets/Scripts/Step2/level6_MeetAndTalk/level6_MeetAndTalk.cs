using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level6_MeetAndTalk : Conversation {
    public MeetBoyTrigger meetTrigger;
    public GameObject player;
    public GameObject boy;
    public GameObject mask;
    public string boyFirstWord;
    public Sprite playerFreeze;
    public float showBoyWordDelay;
    public Vector2 playerMoveTo;
    public float playerMoveDuration;

    private bool updateCtrl = false;
    private bool updateCtrl_1 = false;
    private bool ifCanTalk = false;

    protected override void Start()
    {
        if(boy.activeSelf)
        {
            boy.SetActive(false);
        }
        base.Start();
    }

    protected override void Update()
    {
        if(meetTrigger.ifBoyShow && !updateCtrl)
        {
            GetComponent<ClicktoMove>().enabled = false;
            player.GetComponent<PlayerControl>().enabled = false;
            player.GetComponent<Animator>().enabled = false;
            player.GetComponent<SpriteRenderer>().sprite = playerFreeze;
            boy.SetActive(true);

            Invoke("ShowBoyFirstWord", showBoyWordDelay);

            updateCtrl = true;
        }

        if(ifCanTalk)
        {
            base.Update();
        } 

        if (isAlreadyTalk && !updateCtrl_1) 
        {
            player.GetComponent<Animator>().enabled = true;
            player.GetComponent<Animator>().SetBool("Walk", true);
            TweenPosition.Begin(player, playerMoveDuration, playerMoveTo);

            boy.GetComponent<Animator>().SetBool("BoyRunBack", true);
            mask.GetComponent<Animator>().enabled = true;
            GetComponent<EnterNextLevel>().enabled = true;
        }
    }

    void ShowBoyFirstWord()
    {
        base.NPCTalkingPanel.SetActive(true);
        base.NPCTalkingPanel.GetComponentInChildren<Text>().text = boyFirstWord;
        ifCanTalk = true;
    }
}
