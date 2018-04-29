using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldNoteTest : MonoBehaviour {

    public NoteEnterTriggerTest noteTrigger;
    public float changeRate;
    public bool ifLeftHoldPan;
    public GameObject leftTouch;
    public GameObject rightTouch;

    //[HideInInspector]
    public bool isClick;

    private GameObject theNote;
    private GameObject touchObj;
    private bool ifLong;
    private bool isUp;
    private NoteMoveTest noteMove;
    private float fillAmount = 1f;
    // Use this for initialization
    void Start()
    {
        EventTriggerTest.Get(gameObject).onPressDown += OnClickDown;
        EventTriggerTest.Get(gameObject).onPressUp += OnClickUp;
    }

    void OnClickDown(GameObject go)
    {
        isUp = false;
        isClick = true;
        GetAndHandleTheNote();

        if (ifLeftHoldPan)
        {
            touchObj = Instantiate(leftTouch, leftTouch.transform.position, leftTouch.transform.rotation) as GameObject;
        }
        else
        {
            touchObj = Instantiate(rightTouch, rightTouch.transform.position, rightTouch.transform.rotation) as GameObject;
        }

        if (ifLong)
            StartCoroutine(TurnningShort());
        else if (!ifLong)
        {
            if (noteTrigger.ifNoteEnter)
            {
                noteTrigger.ifSuccess = true;
                noteTrigger.NoteSuccess();
            }
        }
    }

    void OnClickUp(GameObject go)
    {
        isClick = false;

        Destroy(touchObj);

        if (ifLong)
        {
            isUp = true;
        }
    }

    //  键盘测试：LeftControl对应点击左边屏幕，rightControl对应点击右边屏幕
    void Update()
    {
        if (
            (Input.GetKeyDown(KeyCode.LeftControl) && ifLeftHoldPan) ||
            (!ifLeftHoldPan && Input.GetKeyDown(KeyCode.RightControl))
          )
        {
            OnClickDown(gameObject);
        }

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
            noteMove = theNote.GetComponent<NoteMoveTest>();
        }
    }

    private IEnumerator TurnningShort()
    {
        while (true)
        {
            if (noteMove == null) break;

            if (isUp || !noteTrigger.ifNoteEnter)
            {
                noteMove.ifStop = false;
                break;
            }

            if (theNote.transform.localScale.y <= 0)
            {
                theNote.transform.localScale = new Vector3(0f, 0f, 0f);
                noteTrigger.ifSuccess = true;
                noteTrigger.NoteSuccess();
                break;
            }

            noteMove.ifStop = true;
            
            float newScaleOnY = theNote.transform.localScale.y - changeRate;
            Vector3 newScale = new Vector3(theNote.transform.localScale.x, newScaleOnY, theNote.transform.localScale.z);
            theNote.transform.localScale = newScale;

            yield return null;
        }
    }
}
