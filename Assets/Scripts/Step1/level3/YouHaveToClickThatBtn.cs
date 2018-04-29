using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouHaveToClickThatBtn : MonoBehaviour {
    public GameObject NPCTalkingPanel;
    public LatConversation conversation;
    public bool ifConActive = false;
    public bool defaultActive = false;
    void Start()
    {
        ifConActive = false;
    }

    public void YouHaveToClick()
    {
        ifConActive = true;
    }

    void Update()
    {
        if (ifConActive && defaultActive)
            conversation.enabled = true;
        else
            conversation.enabled = false;
    }
}
