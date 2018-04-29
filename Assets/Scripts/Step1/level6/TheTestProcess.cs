using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheTestProcess : MonoBehaviour {
    public int test_id;
    public Button[] option;
    public int theRightAnswer_id;

    [HideInInspector]
    public bool ifChosenRight = false;

    public NPCTestToEnterDoor allTestCheck;

    void Start()
    {
        ClearAllOption();
    }

    void ClearAllOption()
    {
        for (int i = 0; i < option.Length; ++i)
        {
            option[i].GetComponent<EachOptionManager>().ResetOption();
        }
    }

    public void SingleSelection(int select_id)
    {
        for (int i = 0; i < option.Length; ++i)
        {
            if (i != select_id)
                option[i].GetComponent<EachOptionManager>().ResetOption();
        }
    }

    public void CheckAnswer(int select_id)
    {
        if (select_id == theRightAnswer_id)
        {
            ifChosenRight = true;
            allTestCheck.TestResultCheck(test_id);
        }
        else
            ifChosenRight = false;
    }
}
