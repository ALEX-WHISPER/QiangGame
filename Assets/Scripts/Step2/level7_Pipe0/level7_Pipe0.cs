using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level7_Pipe0 : MonoBehaviour {
    public GameObject mask;
    public GameObject rulePanel;
    public float showRuleDelay;
    public float showRuleDuration;
    public Vector3 rulePanelScale;

    void Start()
    {
        mask.GetComponent<Animator>().enabled = true;
        Invoke("ShowRulePanel", showRuleDelay);
    }

    void ShowRulePanel()
    {
        TweenScale.Begin(rulePanel, showRuleDuration, rulePanelScale);
    }

    public void ClickPlayBtn()
    {
        GetComponent<EnterNextLevel>().enabled = true;
    }
}
