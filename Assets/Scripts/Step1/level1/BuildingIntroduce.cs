using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIntroduce : MonoBehaviour
{
    public ClickBuilding[] buidingClickScript;
    public GameObject guideHand;

    private GameObject guideHandObj;

    void Start()
    {
        guideHandObj = Instantiate(guideHand, guideHand.transform.position, guideHand.transform.rotation);
        guideHandObj.transform.position = buidingClickScript[0].gameObject.transform.position;
    }

    public void CalledOnClick()
    {
        for (int i = 0; i < buidingClickScript.Length; i++)
        {
            if (buidingClickScript[i].ifHadBeenClicked)
            {
                if (i == buidingClickScript.Length - 1) //  若申请全局管理的建筑是最后一个建筑
                {
                    Destroy(guideHandObj);  //  销毁指引手
                    GetComponent<EnterNextLevel>().enabled = true;  //  角色移动，开始进入下一关
                }

                //  若申请全局管理的建筑不是最后一个，则将指引手的位置移动到下一建筑
                else
                {
                    guideHandObj.transform.position = buidingClickScript[i + 1].gameObject.transform.position;
                }
            }
        }
    }
}
