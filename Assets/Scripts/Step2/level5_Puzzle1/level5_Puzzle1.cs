using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PuzzleState
{
    Puzzle_0, Puzzle_1
}
public class level5_Puzzle1 : MonoBehaviour {

    public GameObject mainCamera;   //  主相机
    public float moveDuration;      //  相机移动时长
    public Vector3 cameraMoveFrom;  //  移动起点
    public Vector3 cameraMoveTo;    //  移动终点
    public PuzzleManager puzzle_0;  //  拼图0
    public PuzzleManager puzzle_1;  //  拼图1
    public GameObject leavePanel;   //  离开面板
    private PuzzleState pState;     //  当前操作的拼图
    private bool updateCtrl = false;

    void Start()
    {
        pState = PuzzleState.Puzzle_0;  //  默认先操作拼图0
        leavePanel.SetActive(false);    //  隐藏离开面板
    }

    void Update()
    {
        //  仅当两拼图均完成时，激活离开面板
        if(puzzle_0.ifGameOver && puzzle_1.ifGameOver && !updateCtrl)
        {
            leavePanel.SetActive(true);
            updateCtrl = true;
        }
    }

    //  按钮：下一页，即将视角从拼图0移动至拼图1
    public void ClickNextPage()
    {
        if (pState == PuzzleState.Puzzle_0)
        {
            //  更改当前操作拼图状态
            pState = PuzzleState.Puzzle_1;

            //  移动相机
            TweenPosition.Begin(mainCamera, moveDuration, cameraMoveTo);
        }
        else
        {
            return;
        }
    }

    //  按钮：上一页，即将视角从拼图1移动至拼图0
    public void ClickPreviousPage()
    {
        if (pState == PuzzleState.Puzzle_1)
        {
            //  更改当前操作的拼图状态
            pState = PuzzleState.Puzzle_0;

            //  将相机移回至初始位置
            TweenPosition.Begin(mainCamera, moveDuration, cameraMoveFrom);
        }
        else
        {
            return;
        }
    }

    //  按钮：离开，进入下一场景
    public void ClickLeaveBtn()
    {
        GetComponent<EnterNextLevel>().enabled = true;
    }
}
