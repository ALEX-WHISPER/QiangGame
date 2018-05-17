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
            currentNote.note_Obj = other.gameObject;    //  current note that enters the trigger area currently
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

            //  miss the note
            if(!ifSuccess)
            {
                gameManager.AddHitPoints(false);
            } 
        }
    }

    //  successful hit, add hit points and score value
    public void NoteSuccess()
    {
        //  instantiate the explosion effect
        Vector2 explotionPos = new Vector2(currentNote.note_Obj.transform.position.x, transform.position.y);
        Instantiate(successExplosion, explotionPos, successExplosion.transform.rotation);

        //  destroy the note
        DestroyNote();

        //  add hit points
        gameManager.AddHitPoints(true);

        //  add score value based on note type
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
