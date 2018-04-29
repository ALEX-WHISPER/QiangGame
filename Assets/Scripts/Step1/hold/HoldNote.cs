using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldNote : MonoBehaviour {
    public NoteEnterTrigger noteTrigger;
    public float changeRate;
    public bool ifLeftHoldPan;

    //[HideInInspector]
    public bool isClick;

    private GameObject theNote;
    private bool ifLong;
    private bool isUp;
    private Image img;
    private NoteMoveDown noteMove;
    private float fillAmount = 1f;
	// Use this for initialization
	void Start () 
    { 
        EventTriggerTest.Get(gameObject).onPressDown += OnClickDown;
        EventTriggerTest.Get(gameObject).onPressUp += OnClickUp;
	}

    void OnClickDown(GameObject go)
    {
        isUp = false;
        isClick = true;
        GetAndHandleTheNote();
        
        if (ifLong)
            StartCoroutine(TurnningShort());
        else if (!ifLong)
        {
            if (noteTrigger.ifNoteEnter)
                noteTrigger.NoteSuccess();
        }
    }
    
    void OnClickUp(GameObject go)
    {
        isClick = false;

        if (ifLong)
        {
            isUp = true;
            img.fillAmount = fillAmount;
        }
    }

    //  键盘测试：LeftControl对应点击左边屏幕，rightControl对应点击右边屏幕
    void Update()
    {
        if (
            (Input.GetKeyDown(KeyCode.LeftControl) && ifLeftHoldPan) ||
            (!ifLeftHoldPan && Input.GetKeyDown(KeyCode.RightControl))
          )
            OnClickDown(gameObject);

        if (
            (Input.GetKeyUp(KeyCode.LeftControl) && ifLeftHoldPan) ||
            (!ifLeftHoldPan && Input.GetKeyUp(KeyCode.RightControl))
           )
            OnClickUp(gameObject);
    }

    void GetAndHandleTheNote()
    {
        theNote = noteTrigger.currentNote.note_Obj;
        ifLong = noteTrigger.currentNote.ifLong;

        if (theNote == null) return;

        if (ifLong)
        {
            img = theNote.GetComponent<Image>();
            noteMove = theNote.GetComponent<NoteMoveDown>();
        } 
    }

    private IEnumerator TurnningShort()
    {
        while (true)
        {
            if (noteMove == null) break;

            if (isUp || !noteTrigger.ifNoteEnter)
            {
                noteMove.enabled = true;
                break;
            }

            noteMove.enabled = false;
            img.fillAmount -= changeRate * Time.deltaTime;
            fillAmount = img.fillAmount;

            if (img.fillAmount == 0)
            {
                noteTrigger.NoteSuccess();
                break;
            }
            yield return null;
        }
    }
}
