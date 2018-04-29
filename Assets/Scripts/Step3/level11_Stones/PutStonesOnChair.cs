using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutStonesOnChair : MonoBehaviour {
    public GameObject[] stones;
    public Vector3[] stonePos;
    public Animator[] stoneFlash;
    public Animator chairScene;
    public Animator recoveryView;
    public Animator hideAnim;
    public float moveDuration;

    void Start()
    {
        Invoke("StonesMove", 1f);
        Invoke("StoneFlash", 1f + moveDuration);
        Invoke("HideChairScene", 1f + 2f + moveDuration);
        Invoke("RecoveryViewMove", 2f + 1f + 2f + moveDuration);
        Invoke("HideSceneAnim", 8f + 1f + 1f + 2f + moveDuration);
    }

    private void StonesMove()
    {
        for (int i = 0; i < stones.Length; i++ )
        {
            TweenPosition.Begin(stones[i], moveDuration, stonePos[i]);
        }
    }

    private void StoneFlash()
    {
        foreach(Animator flash in stoneFlash)
        {
            flash.SetTrigger("flash");
        }
    }

    private void HideChairScene()
    {
        chairScene.enabled = true;
    }

    private void RecoveryViewMove()
    {
        recoveryView.enabled = true;
    }

    private void HideSceneAnim()
    {
        hideAnim.enabled = true;
    }
}
