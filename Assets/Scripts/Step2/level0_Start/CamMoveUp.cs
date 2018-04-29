using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveUp : MonoBehaviour {
    public float targetPosOnY;
    public float speed;

	// Update is called once per frame
	void Update () 
    {
        CameraMoveUp();
	}

    void CameraMoveUp()
    {
        if(transform.position.y < targetPosOnY)
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }
}
