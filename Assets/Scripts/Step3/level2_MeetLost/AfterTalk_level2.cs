using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterTalk_level2 : AfterNormalConversation {
    public Animator hideAnim;
    public string nextLevelName;
    
    protected new void Update()
    {
        base.Update();
    }

    protected override void AfterConversation()
    {
        if (hideAnim.enabled == false)
            hideAnim.enabled = true;

        Invoke("EnterNextLevel", 2f);
    }

    private void EnterNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
