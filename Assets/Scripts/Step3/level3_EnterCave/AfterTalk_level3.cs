using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterTalk_level3 : AfterNormalConversation {
    public GameObject enterPanel;
    public string nextLevelName;

    protected new void Update()
    {
        base.Update();
    }

    protected override void AfterConversation()
    {
        enterPanel.SetActive(true);
    }

    public void ClickConfirmBtn()
    {
        EnterNextLevel();
    }

    private void EnterNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
