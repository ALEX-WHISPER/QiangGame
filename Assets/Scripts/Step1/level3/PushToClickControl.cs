using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushToClickControl : MonoBehaviour {
    public GameObject NPCTalkingPanel;
    public YouHaveToClickThatBtn click;

    void Update()
    {
        if (NPCTalkingPanel.activeSelf)
            click.defaultActive = true;
        else if (!NPCTalkingPanel.activeSelf)
            click.ifConActive = false;
    }
}
