using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour {
    public GameObject mask;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Finish")
        {
            mask.GetComponent<Animator>().enabled = true;
            GameObject.Find("GameControl").GetComponent<EnterNextLevel>().enabled = true;
        }
    }
}
