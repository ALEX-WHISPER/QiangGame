using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothNoteControl : MonoBehaviour {

    public HoldNote leftNoteControl;
    public HoldNote rightNoteControl;
    public GameObject successExplosion;

    private GameObject note_Obj;
    private bool ifNoteEnter;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BothNote")
        {
            ifNoteEnter = true;
            note_Obj = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BothNote")
        {
            ifNoteEnter = false;
            note_Obj = null;
        }
    }

    void Update()
    {
        if (leftNoteControl.isClick && rightNoteControl.isClick && ifNoteEnter)
        {
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
        }
    }

    private void DestroyNote()
    {
        Destroy(note_Obj);
        ifNoteEnter = false;
    }
}
