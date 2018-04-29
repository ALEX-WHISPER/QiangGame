using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2_Museum0 : MonoBehaviour {
    public Animator clickLightAnim;
    public GameObject enterPanel;

    void Start()
    {
        clickLightAnim.SetBool("ifStoneTwinkle", true);
        enterPanel.SetActive(false);
    }

    public void ClickSculpture()
    {
        if(!enterPanel.activeSelf)
        {
            enterPanel.SetActive(true);
            clickLightAnim.gameObject.SetActive(false);
        }
    }

    public void ClickEnter()
    {
        GameObject.Find("GameControl").GetComponent<EnterNextLevel>().enabled = true;
    }
}
