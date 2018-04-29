using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_Move_Step2 : MonoBehaviour {

    public Transform guideHandPos;
    public GameObject introducePanel;
    public GameObject guideHand;

    private GameObject guideHand_Obj;

    void Start()
    {
        ShowGuideHand();
        introducePanel.SetActive(false);
    }

    void ShowGuideHand()
    {
        guideHand_Obj = Instantiate(guideHand, guideHandPos.position, guideHand.transform.rotation) as GameObject;
    }

    public void ClickBuilding()
    {
        if(guideHand_Obj != null)
        {
            Destroy(guideHand_Obj);
        }
        if(!introducePanel.activeSelf)
        {
            introducePanel.SetActive(true);
        }
    }

    public void ClickCloseBuilding()
    {
        introducePanel.SetActive(false);
    }
}
