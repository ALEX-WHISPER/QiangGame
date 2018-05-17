using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    //需要显示的鼠标的样式  
    public Texture2D texture_up;
    public Texture2D texture_down;
    public float offsetOnY;

    private bool isClicked = false;

    // Use this for initialization  
    void Start()
    {
        Cursor.visible = false;
    }
    void OnGUI()
    {
        if (GetComponent<GameController>().ifGameOver) return;

        //  获取鼠标位置  
        Vector3 mousepos = Input.mousePosition;

        if (isClicked)
        {
            //  绘制光标图
            GUI.DrawTexture(new Rect(mousepos.x, Screen.height - mousepos.y - offsetOnY, texture_down.width, texture_down.height), texture_down);
        }

        if (!isClicked)
        {
            GUI.DrawTexture(new Rect(mousepos.x, Screen.height - mousepos.y - offsetOnY, texture_up.width, texture_up.height), texture_up);
        }
    }

    void Update()
    {
        Vector3 mousepos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }
    }
    public void HandReset()
    {
        Cursor.visible = true;
        texture_up = texture_down = null;
    }
}
