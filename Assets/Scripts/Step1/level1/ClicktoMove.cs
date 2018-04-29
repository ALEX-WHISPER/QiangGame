using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicktoMove : MonoBehaviour {
    public GameObject clickLight;       //  光晕效果预制件
    public GameObject player;           //  玩家
    public float clickLimitOnY_Max;         //  点击范围(垂直方向最小值)
    public float clickLimitOnY_Min;         //  点击范围(垂直方向最大值)
    //public ClickBtn clickBtnScript;
    
    [HideInInspector]
    public float distance;     //  目标位置与玩家目前为止的距离
    [HideInInspector]
    public bool ifCanMove = false;
    
    private float clickPos;     // 鼠标点击的屏幕位置经过转换后的世界坐标位置
    private Vector3 screenPosition;
    private Vector3 mousePositionOnScreen;
    private Vector3 mousePositionInWorld;
     
    void Update()
    {
        if (GetComponent<ClickBtn>() != null && GetComponent<ClickBtn>().isInBtn)
            return;

        //  点击产生光晕效果
        if (Input.GetMouseButtonDown(0))
        {
            screenPosition = Camera.main.WorldToScreenPoint(transform.position);    //  代码所依附的空物体的位置，无所谓在哪，用了WorldToScreenPoint函数之后它的z轴值变为跟camera一样
            mousePositionOnScreen = Input.mousePosition;        //  虽然属于屏幕坐标，但它的z轴值不是屏幕坐标系的z轴值
            mousePositionOnScreen.z = screenPosition.z;             //  把刚取到的camera z轴坐标值赋给鼠标点击坐标，只有这时的值才是真正在屏幕点击的位置
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);  //   将屏幕坐标转换为世界坐标

            if (mousePositionInWorld.y > clickLimitOnY_Max || mousePositionInWorld.y < clickLimitOnY_Min)
                return;

            Instantiate(clickLight, mousePositionInWorld, clickLight.transform.rotation);
            clickPos = mousePositionInWorld.x;
            distance = clickPos - player.transform.position.x;
        }

        //  人物朝鼠标点击的位置移动
        if (Mathf.Abs(distance) > 0.1f && mousePositionInWorld.y <= clickLimitOnY_Max && mousePositionInWorld.y >= clickLimitOnY_Min)
        {
            ifCanMove = true;

            clickPos = mousePositionInWorld.x;
            distance = clickPos - player.transform.position.x;
        }
        else
            ifCanMove = false;
    }
}
