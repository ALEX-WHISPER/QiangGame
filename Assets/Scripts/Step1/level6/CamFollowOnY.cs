using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowOnY : MonoBehaviour {
    public float camTargetPosOnY;
    public float duration;
    public float camMoveDelay;

    [HideInInspector]
    public bool ifMoveDone = false;

    void Start()
    {
        Invoke("CameraMove", camMoveDelay);
    }

    void Update()
    {
        if (transform.position.y >= camTargetPosOnY)
            ifMoveDone = true;
    }

    void CameraMove()
    {
        Vector3 tagetPos = new Vector3(transform.position.x, camTargetPosOnY, transform.position.z);
        TweenPosition.Begin(gameObject, duration, tagetPos);
    }
}
