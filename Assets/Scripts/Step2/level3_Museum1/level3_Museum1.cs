using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3_Museum1 : MonoBehaviour
{
    public GameObject stitchPanel;
    public GameObject enterPanel;
    public GameObject mask;
    public GameObject enterImage;
    public GameObject guideHand;
    public Vector3 guideHandPos;
    public Animator girlEnter;
    public float btnActiveDelay;
    public float maskStartDelay;

    private bool ifBtnActive = false;
    private GameObject guideHandObj;

    void Awake()
    {
        Invoke("BtnActive", btnActiveDelay);
        guideHandObj = Instantiate(guideHand, guideHandPos, Quaternion.identity);
    }

    void Start()
    {
        if(stitchPanel.activeSelf || enterPanel.activeSelf || enterImage.activeSelf)
        {
            stitchPanel.SetActive(false);
            enterPanel.SetActive(false);
            enterImage.SetActive(false);
        }
    }

    void BtnActive()
    {
        ifBtnActive = true;
    }

    public void ClickStitch()
    {
        if(!stitchPanel.activeSelf && ifBtnActive)
        {
            stitchPanel.SetActive(true);

            if(guideHandObj != null)
            {
                Destroy(guideHandObj);
            }
        }
    }

    public void CloseStitch()
    {
        if(stitchPanel.activeSelf)
        {
            stitchPanel.SetActive(false);
            enterPanel.SetActive(true);
            enterImage.SetActive(true);
        }
    }

    public void ClickEnter()
    {
        Invoke("MaskStart",maskStartDelay);
        GetComponent<EnterNextLevel>().enabled = true;
        girlEnter.SetBool("girlEnter",true);
    }

    void MaskStart()
    {
        mask.GetComponent<Animator>().enabled = true;
    }

}
