using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct noteData
{
   public bool ifLong;
   public GameObject note_Obj;
}

public class NoteEnterTrigger : MonoBehaviour {
    //[HideInInspector]
    public bool ifNoteEnter = false;
    public GameObject successExplosion;

    public noteData currentNote;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LongNote" || other.tag == "ShortNote")
        {
            ifNoteEnter = true;
            currentNote.note_Obj = other.gameObject;
        }
        if (other.tag == "LongNote") currentNote.ifLong = true;
        if (other.tag == "ShortNote") currentNote.ifLong = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "LongNote" || other.tag == "ShortNote")
            ifNoteEnter = false;
    }

    public void NoteSuccess()
    {
        Vector2 explotionPos = new Vector2(currentNote.note_Obj.transform.position.x, transform.position.y);
        Instantiate(successExplosion, explotionPos, successExplosion.transform.rotation);
        DestroyNote();
    }

    private void DestroyNote()
    {
        Destroy(currentNote.note_Obj);
        ifNoteEnter = false;
    }
}
