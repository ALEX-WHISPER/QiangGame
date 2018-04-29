using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterDinnerConversation : AfterNormalConversation{
    public GameObject stoneObj;
    public GameObject mapPanel;
    public float showMapDelay;

    private Animator stoneTwinkleAnim;

    protected new void Update()
    {
        base.Update();
    }

    protected override void AfterConversation()
    {
        stoneObj.SetActive(true);
        if (stoneObj.GetComponentInChildren<Animator>() != null)
        {
            stoneTwinkleAnim = stoneObj.GetComponentInChildren<Animator>();
            stoneTwinkleAnim.SetBool("ifStoneTwinkle", true);
        }
        Invoke("MoveOnMapToNextLevel", showMapDelay);
    }

    private void MoveOnMapToNextLevel()
    {
        mapPanel.SetActive(true);
    }
}
