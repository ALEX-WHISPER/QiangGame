using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterTalk_Level1 : AfterNormalConversation {
    public Animator girlAnim;
    public Animator hideAnim;
    public string nextLevelName;

    protected new void Update()
    {
        base.Update();
    }

    protected override void AfterConversation()
    {
        //  1. 开始播放动画

        //  动画1：系上红丝带
        if(girlAnim.enabled == false)
            girlAnim.enabled = true;

        Invoke("HideAndLoadNextScene", 3f);          
    }

    private void HideAndLoadNextScene()
    {
        //  动画2：转场渐变
        if (hideAnim.enabled == false)
            hideAnim.enabled = true;

        //  2. 跳转场景
        SceneManager.LoadScene(nextLevelName);
    }
}
