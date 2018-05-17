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
    public createSpot noteShowPos;  //  音符生成的位置
    public createRate noteRate;     //  生成音符在各位置出现的概率
    public notes note;              //  供实例化的各类音符预制体
    public Transform noteObjectHolder;  //  所有音符物体的父物体
    public float shortNoteRate; //  生成音符类型为短音符的概率
    public float startDelay;    //  开始生成音符之前的延时
    public float nextNoteDelay; //  前后音符生成间隔
    public float noteCount;     //  数量

    private GameObject note_Obj;
    private GameObject note_gameObject;
    private Transform noteShowTransform;
    private notePosState posState;
    private noteTypeState typeState;

	void Start () 
    {
        StartCoroutine(SpawnWaves());
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startDelay);

        for (int i = 0; i < noteCount; ++i)
        {
            //  确定生成音符的位置: left, right, middle
            posState = SetNotePos();

            //  确定生成音符的类型: long, short
            typeState = SetNoteType();

            if (posState == notePosState.LEFT)
            {
                noteShowTransform = noteShowPos.leftNoteSpot;
                if (typeState == noteTypeState.LONG) note_Obj = note.longNote_L;    //  left long
                if (typeState == noteTypeState.SHORT) note_Obj = note.shortNote_L;  //  left short
            }

            if (posState == notePosState.RIGHT)
            {
                noteShowTransform = noteShowPos.rightNoteSpot;
                if (typeState == noteTypeState.LONG) note_Obj = note.longNote_R;    //  right long
                if (typeState == noteTypeState.SHORT) note_Obj = note.shortNote_R;  //  right short
            }

            //  若位置为中间，则同时确定了生成音符的类型: mid double
            if (posState == notePosState.MID)
            {
                noteShowTransform = noteShowPos.midNoteSpot;
                note_Obj = note.doubleNote;
            }

            //  实例化物体：在指定位置，生成指定类型的音符
            note_gameObject = Instantiate(note_Obj, noteShowTransform.position, noteShowTransform.rotation, noteObjectHolder) as GameObject;

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
