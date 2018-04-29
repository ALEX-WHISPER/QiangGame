using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterNormalConversation : MonoBehaviour {
    public DinnerConversation normalConversation;
    private bool isAlreadyTrigger = false;

    protected void Update()
    {
        if (normalConversation.GetConversationIndex() == normalConversation.conObj.Length && !isAlreadyTrigger)
        {
            AfterConversation();
            isAlreadyTrigger = true;
        }
        else
            return;
    }
    protected virtual void AfterConversation() { }
}
