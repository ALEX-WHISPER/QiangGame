using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickBuilding : MonoBehaviour {
    public int building_id;
    public GameObject thisBuildingIntroduce;
    [HideInInspector]
    public bool ifHadBeenClicked = false;
    [HideInInspector]
    public GameObject thisBuildingIntroduce_Obj;
    public BuildingIntroduce buildingClickControl;
    public bool isClicked;

    private int this_id;
    private Transform obj_Parent = null;

    void Start()
    {
        this_id = building_id;

        if (this_id < buildingClickControl.buidingClickScript.Length - 1)
            obj_Parent = GameObject.FindWithTag("Buildings").transform;
    }

    public void OnClickThisBuilding()
    {
        //  1. 非第 0 个建筑介绍；  
        //  2. 上一个建筑介绍已被触发； 
        //  3. 本建筑介绍尚未被点击（此条件为控制在关闭前只能打开一个介绍物体，否则可无限点击、不断生成） 
        if (this_id != 0 && (buildingClickControl.buidingClickScript[this_id - 1].ifHadBeenClicked) && !isClicked)
        {
            isClicked = true;
            CallOnClick();
            DestroyLastBuildingContent(this_id -1);
            return;
        }
        else if (this_id == 0 && !isClicked)
        {
            isClicked = true;
            CallOnClick();
        }
    }

    void CallOnClick()
    {
        //  点击后实例化的物体
        thisBuildingIntroduce_Obj = Instantiate(thisBuildingIntroduce, 
                            thisBuildingIntroduce.transform.position, 
                            thisBuildingIntroduce.transform.rotation) as GameObject;

        //  若建筑非最后一个，将其置于统一的父物体内
        if (this_id < buildingClickControl.buidingClickScript.Length - 1 && obj_Parent.transform != null)
        {
            thisBuildingIntroduce_Obj.transform.SetParent(obj_Parent, false);
        }

        //  该建筑已被触发过
        this.ifHadBeenClicked = true;

        //  呼叫全局管理
        buildingClickControl.CalledOnClick();
    }

    void DestroyLastBuildingContent(int lastBuilding_Id)
    {
        for (int i = 0; i < lastBuilding_Id + 1; i++)
        {
            buildingClickControl.buidingClickScript[i].isClicked = false;
        }
        Destroy(buildingClickControl.buidingClickScript[lastBuilding_Id].thisBuildingIntroduce_Obj);  
    }
}
