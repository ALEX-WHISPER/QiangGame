using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIntroduce : MonoBehaviour
{
    public ClickBuilding[] buidingClickScript;  //  interactable buildings array
    public GameObject guideHand;    //  the guide hand prefab

    private GameObject guideHandObj;    //  reference object of the instance of guideHand prefab

    void Start()
    {
        //  instantiate guide hand prefab, and put it in the chapter board's position on start
        guideHandObj = Instantiate(guideHand, guideHand.transform.position, guideHand.transform.rotation);
        guideHandObj.transform.position = buidingClickScript[0].gameObject.transform.position;
    }

    public void CalledOnClick()
    {
        for (int i = 0; i < buidingClickScript.Length; i++)
        {
            if (buidingClickScript[i].ifHadBeenClicked)
            {
                //  if it's the last building that had been clicked
                if (i == buidingClickScript.Length - 1)
                {
                    //  destroy the guide hand and enter to the next level
                    Destroy(guideHandObj);
                    GetComponent<EnterNextLevel>().enabled = true;
                }

                //  if it isn't the last building, then move the guideHand to the position of the next building
                else
                {
                    guideHandObj.transform.position = buidingClickScript[i + 1].gameObject.transform.position;
                }
            }
        }
    }
}
