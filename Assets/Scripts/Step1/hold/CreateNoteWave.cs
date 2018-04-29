using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class createSpot
{
    public Transform leftNoteSpot, rightNoteSpot, midNoteSpot;
}

[System.Serializable]
public class createRate
{
    public float leftRate, rightRate, midRate;
}

[System.Serializable]
public class notes
{
    public GameObject shortNote_L, shortNote_R, longNote_L, longNote_R, doubleNote;
}

public enum notePosState
{
    LEFT,
    MID,
    RIGHT
}

public enum noteTypeState
{
    LONG,
    SHORT,
    DOUBLE
}

/*  1. 先计算概率，根据概率值，确定生成音符的位置 a. 左边 b. 中间 c. 右边
 *  2. 如果是 1.a 或 1.c，再确定生成音符的类型 a. 短音 b. 长音，如果是 1.b，则只有一种情况，即 c. 双音
 *  
 *  1.a 与 1.c 的概率应相等，1.b 的概率要低一些
 *  2.b 应略低于 2.a
 */
public class CreateNoteWave : MonoBehaviour {
    public createSpot noteShowPos;
    public createRate noteRate;
    public notes note;
    public Transform noteTransform;
    public float shortNoteRate;
    public float startDelay;
    public float nextNoteDelay;
    public float noteCount;

    private GameObject note_Obj;
    private GameObject note_gameObject;
    private Transform noteShowTransform;
    private notePosState posState;
    private noteTypeState typeState;
	// Use this for initialization
	void Start () 
    {
        StartCoroutine(SpawnWaves());
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startDelay);

        for (int i = 0; i < noteCount; ++i)
        {
            //  确定生成音符的位置
            posState = SetNotePos();

            //  确定生成音符的类型
            typeState = SetNoteType();

            if (posState == notePosState.LEFT)
            {
                noteShowTransform = noteShowPos.leftNoteSpot;
                if (typeState == noteTypeState.LONG) note_Obj = note.longNote_L;
                if (typeState == noteTypeState.SHORT) note_Obj = note.shortNote_L;
            }

            if (posState == notePosState.RIGHT)
            {
                noteShowTransform = noteShowPos.rightNoteSpot;
                if (typeState == noteTypeState.LONG) note_Obj = note.longNote_R;
                if (typeState == noteTypeState.SHORT) note_Obj = note.shortNote_R;
            }

            if (posState == notePosState.MID)   //  若位置为中间，则同时确定了生成音符的类型
            {
                noteShowTransform = noteShowPos.midNoteSpot;
                note_Obj = note.doubleNote;
            }

            //  实例化物体：在指定位置，生成指定类型的音符
            note_gameObject = Instantiate(note_Obj, noteShowTransform.position, noteShowTransform.rotation, noteTransform) as GameObject;

            yield return new WaitForSeconds(nextNoteDelay);
        }
    }

    notePosState SetNotePos()
    {
        int randomValue = Random.Range(0, 100);

        if (randomValue >= 0 && randomValue <= noteRate.leftRate) return notePosState.LEFT;

        else if (randomValue > noteRate.leftRate && randomValue <= noteRate.leftRate + noteRate.rightRate) return notePosState.RIGHT;

        else if (randomValue > noteRate.leftRate + noteRate.rightRate && randomValue <= 100) return notePosState.MID;

        else return 0;
        
    }

    noteTypeState SetNoteType()
    {
        float randomValue = Random.Range(0, 100);

        if (randomValue >= 0 && randomValue <= shortNoteRate) return noteTypeState.SHORT;

        else if (randomValue > shortNoteRate && randomValue <= 100) return noteTypeState.LONG;
        
        else return 0;
    }
}
