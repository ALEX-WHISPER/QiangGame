using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickBuilding : MonoBehaviour {
    public int building_id;
    public GameObject thisBuildingIntroduce;    //  the introducing board object on clicked

    [HideInInspector]
    public bool ifHadBeenClicked = false;   //  whether this building has been clicked, which also means, if the next building is able to be clicked
    [HideInInspector]
    public GameObject thisBuildingIntroduce_Obj;    //  prefab of the introducing board of this building

    public BuildingIntroduce buildingClickControl;  //  like the manager to manage all the clickable buildings
    public bool isTriggerable;  //  turn false once the introducing board been created, turn true once introducing board been destroyed

    private int this_id;
    private Transform obj_Parent = null;

    void Start()
    {
        this_id = building_id;
        isTriggerable = true;

        if (this_id < buildingClickControl.buidingClickScript.Length - 1)
            obj_Parent = GameObject.FindWithTag("Buildings").transform;
    }

    public void OnClickThisBuilding()
    {
        //  1. not the index of 0; 2. the last building has been clicked; 3. this building is clickable(introducing board on start/ on destroy) 
        if (this_id != 0 && (buildingClickControl.buidingClickScript[this_id - 1].ifHadBeenClicked) && isTriggerable)
        {
            CallOnClick();
            isTriggerable = false;

            //  destroy the introducing board of the last building
            DestroyLastBuildingContent(this_id -1);
        }
        else if (this_id == 0 && isTriggerable)
        {
            CallOnClick();
            isTriggerable = false;
        }
    }

    void CallOnClick()
    {
        //  instantiate the introducing board
        thisBuildingIntroduce_Obj = Instantiate(thisBuildingIntroduce, 
                            thisBuildingIntroduce.transform.position, 
                            thisBuildingIntroduce.transform.rotation) as GameObject;

        //  set parent holder
        if (this_id < buildingClickControl.buidingClickScript.Length - 1 && obj_Parent.transform != null)
        {
            thisBuildingIntroduce_Obj.transform.SetParent(obj_Parent, false);
        }

        //  this building has been clicked, which is the neccessary condition to make the next building clickable
        this.ifHadBeenClicked = true;

        //  move the guide hand to the next position
        buildingClickControl.CalledOnClick();
    }

    void DestroyLastBuildingContent(int lastBuilding_Id)
    {
        //  destroy the introducing board object of the last building
        Destroy(buildingClickControl.buidingClickScript[lastBuilding_Id].thisBuildingIntroduce_Obj);  
    }
}
