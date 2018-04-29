using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBuildingIntroduce : MonoBehaviour {
    public int this_id;
    private BuildingIntroduce buildingClickControl;
    void Start()
    {
        EventTriggerTest.Get(gameObject).onClick += OnClickThisBtn;
        buildingClickControl = GameObject.FindWithTag("GameController").GetComponent<BuildingIntroduce>();
    }

    void OnClickThisBtn(GameObject go)
    {
        Destroy(go.transform.parent.gameObject);
        buildingClickControl.buidingClickScript[this_id].isClicked = false;
    }
}
