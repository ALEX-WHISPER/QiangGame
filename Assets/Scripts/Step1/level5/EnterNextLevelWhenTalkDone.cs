using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterNextLevelWhenTalkDone : Conversation
{
    private bool ifConTrigger = true;
    private bool updateCtrl = true;
    new void Awake()
    {
        Cursor.visible = true;
        base.Awake();
    }

    protected override void Start()
    {
        count = -1;
        GetComponent<ClicktoMove>().enabled = false;
        base.Start();
    }

    protected override void Update()
    {
        if (!ifConTrigger || GetComponent<ClickBtn>().ifOpenBag) return;

        if (isAlreadyTalk && updateCtrl)
        {
            GetComponent<EnterNextLevel>().enabled = true;
            updateCtrl = false;
        }

        base.Update();
    }

    public void MouseEnterBtn()
    {
        ifConTrigger = false;
    }

    public void MouseExitBtn()
    {
        ifConTrigger = true;
    }
}
