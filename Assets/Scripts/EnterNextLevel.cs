using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNextLevel : MonoBehaviour {
    public string nextLevelName;
    public float enterLevelDelay;

    void Start()
    {
        Cursor.visible = true;
        Invoke("EnterLevel", enterLevelDelay);
    }

    void EnterLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
