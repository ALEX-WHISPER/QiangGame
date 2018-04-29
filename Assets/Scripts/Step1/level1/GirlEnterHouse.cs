using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlEnterHouse : MonoBehaviour {
    private GameObject player;
    public float walkingDuration;
    public float moveSpeed;
    public Vector3 thisPos;

    private bool updateCtrl = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        AnimPlay();
    }

    void AnimPlay()
    {
        player.SetActive(false);
        GetComponent<Animator>().SetBool("GirlEnter",true);
    }
}
