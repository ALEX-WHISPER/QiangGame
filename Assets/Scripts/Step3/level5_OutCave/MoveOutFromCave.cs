using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveOutFromCave : MonoBehaviour {
    public Animator playerAnim;
    public Animator hideAnim;
    public string nextLevelName;

    void Start()
    {
        Invoke("PlayerMoveAnim", 0.5f);
    }

    void PlayerMoveAnim()
    {
        if (playerAnim.enabled == false)
        {
            playerAnim.enabled = true;
            Invoke("HideAnim", 0.5f);
        }
    }

    void HideAnim()
    {
        if(hideAnim.enabled == false)
            hideAnim.enabled = true;
        Invoke("EnterNextLevel", 2f);
    }

    private void EnterNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
