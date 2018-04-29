using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothNoteControlTest : MonoBehaviour {

    public HoldNoteTest leftNoteControl;
    public HoldNoteTest rightNoteControl;
    public GameObject successExplosion;
    public string noteTag_BothNote;
    public bool ifSuccess = false;

    private GameObject note_Obj;
    private bool ifNoteEnter;
    private PipeScoreManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameControl").GetComponent<PipeScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == noteTag_BothNote)
        {
            ifNoteEnter = true;
            note_Obj = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == noteTag_BothNote)
        {
            ifNoteEnter = false;
            note_Obj = null;
            if(!ifSuccess)
            {
                gameManager.AddHitPoints(false);
            }    
        }
    }

    void Update()
    {
        if (leftNoteControl.isClick && rightNoteControl.isClick && ifNoteEnter)
        {
            ifSuccess = true;
            NoteSuccess();
        }
    }

    void NoteSuccess()
    {
        if (note_Obj != null)
        {
            Vector2 explotionPos = new Vector2(note_Obj.transform.position.x, transform.position.y);
            Instantiate(successExplosion, explotionPos, successExplosion.transform.rotation);   
            DestroyNote();
            gameManager.AddHitPoints(true);
            gameManager.AddScorePoints("both");
        }
    }

    private void DestroyNote()
    {
        Destroy(note_Obj);
        ifNoteEnter = false;
    }
}
