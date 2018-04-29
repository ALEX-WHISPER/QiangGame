using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EachOptionManager : MonoBehaviour {
    [HideInInspector]
    public bool ifBeenChosen = false;

    public GameObject optionImage;
    public int option_id;

    public TheTestProcess testCheck;

    public void ClickThis()
    {
        ifBeenChosen = true;
        optionImage.SetActive(true);

        testCheck.SingleSelection(option_id);
        testCheck.CheckAnswer(option_id);
    }

    public void ResetOption()
    {
        ifBeenChosen = false;
        optionImage.SetActive(false);
    }
}
