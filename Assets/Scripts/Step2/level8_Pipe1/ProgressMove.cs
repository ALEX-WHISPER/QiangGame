using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressMove : MonoBehaviour {
    public GameObject progressImage;
    public float moveDuration;
    public Vector3 terminalPos;
    public Image progressBar;
    public float barMoveSpeed;

    private bool ifEnd = false;
    private bool updateCtrl = false;
	// Use this for initialization
	void Start () {
        ImageMove();
	}
	
	// Update is called once per frame
	void Update () {
        if(!ifEnd)
        {
            BarMove();
        }
        if(ifEnd && !updateCtrl)
        {
            GetComponent<PipeScoreManager>().GameOver();
            updateCtrl = true;
        }
	}

    void ImageMove()
    {
        TweenPosition.Begin(progressImage, moveDuration, terminalPos);
        Debug.Log("Start Time:" + Time.time);
    }

    void BarMove()
    {
        progressBar.fillAmount -= barMoveSpeed * Time.deltaTime;
        if(progressBar.fillAmount == 0)
        {
            ifEnd = true;
            Debug.Log("End Time: " + Time.time); 
        }
    }
}
