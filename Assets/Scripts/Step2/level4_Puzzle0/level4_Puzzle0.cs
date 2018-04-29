using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level4_Puzzle0 : MonoBehaviour {

    public void ClickLock()
    {
        GameObject.Find("GameControl").GetComponent<EnterNextLevel>().enabled = true;
    }
}
