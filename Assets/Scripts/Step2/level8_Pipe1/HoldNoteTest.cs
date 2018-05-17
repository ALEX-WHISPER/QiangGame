using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldNoteTest : MonoBehaviour {

    public NoteEnterTriggerTest noteTrigger;
    public float changeRate;    //  reducing rate of scale.y on long note
    public bool ifLeftHoldPan;
    public GameObject leftTouch;    //  left touch effect prefab
    public GameObject rightTouch;   //  right touch effect prefab

    [HideInInspector]
    public bool isClick;

    private GameObject theNote; //  cuurent note that enters the trigger area on clicking down
    private GameObject touchObj;    //  touching effect object
    private bool ifLong;    //  if the current note is long
    private bool isUp;      //  whether click up
    private NoteMoveTest noteMove;
    private float fillAmount = 1f;

    void Start()
    {
        EventTriggerTest.Get(gameObject).onPressDown += OnClickDown;
        EventTriggerTest.Get(gameObject).onPressUp += OnClickUp;
    }

    //  click down the interactable panel on whole side
    void OnClickDown(GameObject go)
    {
        isUp = false;
        isClick = true;
        GetAndHandleTheNote();

        //  instantiate the touching effect object on every touch
        if (ifLeftHoldPan)
        {
            touchObj = Instantiate(leftTouch, leftTouch.transform.position, leftTouch.transform.rotation) as GameObject;
        }
        else
        {
            touchObj = Instantiate(rightTouch, rightTouch.transform.position, rightTouch.transform.rotation) as GameObject;
        }

        //  if the current note is long type
        if (ifLong)
            StartCoroutine(TurnningShort());

        //  if not long type
        else if (!ifLong)
        {
            //  as long as the note enters the trigger area, then this touch is a successful hit
            if (noteTrigger.ifNoteEnter)
            {
                noteTrigger.ifSuccess = true;
                noteTrigger.NoteSuccess();
            }
        }
    }

    //  click up/loose holding
    void OnClickUp(GameObject go)
    {
        isClick = false;

        Destroy(touchObj);  //  destroy the touching effect prefab

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

    //  get the reference of the note's moving script to change its moving state when it's a long note
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

    //  scale the long note on holding down
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

            //  when the scale.y is tiny enough, trigger to successful hit, then exit this loop
            if (theNote.transform.localScale.y <= 0)
            {
                theNote.transform.localScale = new Vector3(0f, 0f, 0f);

                noteTrigger.ifSuccess = true;
                noteTrigger.NoteSuccess();
                break;
            }

            //  stop the note
            noteMove.ifStop = true;
            
            //  set new scale every frame
            float newScaleOnY = theNote.transform.localScale.y - changeRate;
            Vector3 newScale = new Vector3(theNote.transform.localScale.x, newScaleOnY, theNote.transform.localScale.z);
            theNote.transform.localScale = newScale;

            yield return null;
        }
    }
}
