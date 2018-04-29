using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetStone : MonoBehaviour {
    public GameObject stoneLight;
    public GameObject stone_Obj_Light;
    public GameObject stoneInBag;
    public GameObject bigMap;
    public float hideStoneDelay;
    public float showBigMapDelay;

    private bool updateCtrl = true;
    private bool updateCtrl1 = true;
    private GameObject theStone;

    void Start()
    {
        theStone = GameObject.FindWithTag("Card");
        StoneTwinkle();
    }

    void Update()
    {
        if (GetComponent<GetCard>().ifCardShowing && updateCtrl1)
        {
            Invoke("ShowBigMap", showBigMapDelay);
            updateCtrl1 = false;
        }
    }

    void StoneTwinkle()
    {
        stoneLight.GetComponent<Animator>().SetBool("ifStoneTwinkle", true);
        stone_Obj_Light.GetComponent<Animator>().SetBool("ifStoneTwinkle", true);
    }

    void ShowBigMap()
    {
        bigMap.SetActive(true);
    }
}
