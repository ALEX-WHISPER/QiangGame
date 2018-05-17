using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicktoMove : MonoBehaviour {
    public GameObject clickLight;       //  光晕效果预制件
    public GameObject player;           //  玩家
    public float clickLimitOnY_Max;         //  点击范围(垂直方向最大值)
    public float clickLimitOnY_Min;         //  点击范围(垂直方向最小值)
    
    [HideInInspector]
    public float distance;     //  目标位置与玩家目前为止的距离
    [HideInInspector]
    public bool ifCanMove = false;
    
    private float clickPos_X;     // 鼠标点击的屏幕位置经过转换后的世界坐标位置的x坐标
    private Vector2 mousePositionInWorld;

    void Update()
    {
        //  点击其他按钮时，不触发该脚本功能
        if (GetComponent<ClickBtn>() != null && GetComponent<ClickBtn>().isInBtn)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            //   将鼠标点击处的坐标从屏幕坐标转换为世界坐标
            mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //  若点击位置不合法，则不进入有效点击逻辑处理
            if (mousePositionInWorld.y > clickLimitOnY_Max || mousePositionInWorld.y < clickLimitOnY_Min)
                return;

            //  实例化光晕效果
            Instantiate(clickLight, mousePositionInWorld, clickLight.transform.rotation);

            //  鼠标触发点的世界坐标的x值
            clickPos_X = mousePositionInWorld.x;

            //  计算目标点与主角当前位置的水平距离
            distance = clickPos_X - player.transform.position.x;
        }

        //  若距离值 > 0.1, 则应继续运动
        if (Mathf.Abs(distance) > 0.1f && mousePositionInWorld.y <= clickLimitOnY_Max && mousePositionInWorld.y >= clickLimitOnY_Min)
        {
            ifCanMove = true;

            //  持续更新距离值
            clickPos_X = mousePositionInWorld.x;
            distance = clickPos_X - player.transform.position.x;
        }

        //  若距离值 <= 0.1，或点击位置超出边界范围, 则应停下
        else
            ifCanMove = false;
    }
}
