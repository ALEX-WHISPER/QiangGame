using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PuzzleState
{
    Puzzle_0, Puzzle_1
}
public class level5_Puzzle1 : MonoBehaviour {

    public GameObject mainCamera;
    public float moveDuration;
    public Vector3 cameraMoveFrom;
    public Vector3 cameraMoveTo;
    public PuzzleManager puzzle_0;
    public PuzzleManager puzzle_1;
    public GameObject leavePanel;
    private PuzzleState pState;
    private bool updateCtrl = false;

    void Start()
    {
        pState = PuzzleState.Puzzle_0;
        leavePanel.SetActive(false);
    }

    void Update()
    {
        if(puzzle_0.ifGameOver && puzzle_1.ifGameOver && !updateCtrl)
        {
            leavePanel.SetActive(true);
            updateCtrl = true;
        }
    }

    public void ClickNextPage()
    {
        if (pState == PuzzleState.Puzzle_0)
        {
            pState = PuzzleState.Puzzle_1;
            TweenPosition.Begin(mainCamera, moveDuration, cameraMoveTo);
        }
        else
        {
            return;
        }
    }

    public void ClickPreviousPage()
    {
        if (pState == PuzzleState.Puzzle_1)
        {
            pState = PuzzleState.Puzzle_0;
            TweenPosition.Begin(mainCamera, moveDuration, cameraMoveFrom);
        }
        else
        {
            return;
        }
    }

    public void ClickLeaveBtn()
    {
        GetComponent<EnterNextLevel>().enabled = true;
    }
}
