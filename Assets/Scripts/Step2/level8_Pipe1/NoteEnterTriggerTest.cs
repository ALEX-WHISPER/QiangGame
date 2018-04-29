using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NoteTagName 
{
    public string shortNote_L, shortNote_R, longNote_L, longNote_R;
}

public class NoteEnterTriggerTest : MonoBehaviour {
    //[HideInInspector]
    public bool ifNoteEnter = false;
    public GameObject successExplosion;
    public NoteTagName noteTagName;
    public noteData currentNote;
    public bool ifSuccess = false;

    private PipeScoreManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameControl").GetComponent<PipeScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == noteTagName.longNote_L ||
            other.tag == noteTagName.longNote_R ||
            other.tag == noteTagName.shortNote_L ||
            other.tag == noteTagName.shortNote_R)
        {
            ifNoteEnter = true;
            ifSuccess = false;
            currentNote.note_Obj = other.gameObject;
        }
        if (other.tag == noteTagName.longNote_L || other.tag == noteTagName.longNote_R) currentNote.ifLong = true;
        if (other.tag == noteTagName.shortNote_L || other.tag == noteTagName.shortNote_R) currentNote.ifLong = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == noteTagName.longNote_L ||
            other.tag == noteTagName.longNote_R ||
            other.tag == noteTagName.shortNote_L ||
            other.tag == noteTagName.shortNote_R)
        {
            ifNoteEnter = false;

            if(!ifSuccess)
            {
                gameManager.AddHitPoints(false);
            } 
        }
    }

    public void NoteSuccess()
    {
        Vector2 explotionPos = new Vector2(currentNote.note_Obj.transform.position.x, transform.position.y);
        Instantiate(successExplosion, explotionPos, successExplosion.transform.rotation);
        DestroyNote();

        gameManager.AddHitPoints(true);

        if (currentNote.ifLong)
        {
            gameManager.AddScorePoints("long");
        }

        else
        {
            gameManager.AddScorePoints("short");
        }
    }

    private void DestroyNote()
    {
        Destroy(currentNote.note_Obj);
        ifNoteEnter = false;
    }
}
