using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleManager : MonoBehaviour {
    public GameObject[] puzzleUnits;
    public Transform puzzleBoard;
    public GameObject winPanel;
    public static int selectedCount = 0;
    public float tweenDuration;
    public float disSelectDelay;
    public float offset_X;
    public float offset_Y;
    public float width;
    public float height;

    //[HideInInspector]
    public bool mouseInPuzzle = false;
    public bool ifGameOver = false;

    private int column = 3;
    private int row = 3;
    private List<Vector2> unitPosList = new List<Vector2>();
    private List<GameObject> unitObjList = new List<GameObject>();
    private GameObject unitInstance = null;
    private bool isHandling = false;
    private Dictionary<int, Vector2> unitDic = new Dictionary<int, Vector2>();

    private GameObject selectedUnit_1;
    private GameObject selectedUnit_2;

	// Use this for initialization
    void Start()
    {
        InitPuzzleUnits();
        winPanel.SetActive(false);
    }

    void InitUnitPosList()
    {
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                this.unitPosList.Add(new Vector2(i * width + offset_X, j * height + offset_Y));
            }
        }
    }

    void InitPuzzleUnits()
    {
        InitUnitPosList();

        foreach (GameObject unit in puzzleUnits)
        {
            Vector2 thisUnitPos = unitPosList[Random.Range(0, unitPosList.Count - 1)];  //  确认当前元素生成的位置
            this.unitInstance = Instantiate(unit, thisUnitPos, Quaternion.identity);
            this.unitInstance.GetComponent<SelectUnit>().gameOver = false;

            this.unitInstance.transform.SetParent(puzzleBoard);
            this.unitDic.Add(unit.GetComponent<SelectUnit>().id, thisUnitPos);

            this.unitObjList.Add(unitInstance);
            this.unitPosList.Remove(thisUnitPos);    //  将已使用的位置向量从 List 中移除，防止不同元素堆叠在同一位置上
        }

        this.unitPosList.Clear();
    }

    //public void ResetPuzzle()
    //{
    //    InitUnitPosList();

    //    foreach (GameObject unit in unitObjList)
    //    {
    //        Vector2 thisUnitPos = unitPosList[Random.Range(0, unitPosList.Count - 1)];  //  确认当前元素的新位置
    //        unit.transform.position = thisUnitPos;
    //        unitPosList.Remove(thisUnitPos);    //  将已使用的位置向量从 List 中移除，防止不同元素堆叠在同一位置上
    //    }

    //    unitPosList.Clear();
    //}

    public void UnitSelected(GameObject unit)
    {
        if (isHandling) return;     //  存在两个碎片正在交换位置，此时不能进行对其他碎片的操作
        
        //  若无碎片正在交换位置，则可操作当前选中的碎片
        //unit.GetComponent<SelectUnit>().OnSelected();
        selectedCount++;

        if(selectedCount == 1)  //  为 1 时，表示当前只选中了一个碎片
        {
            selectedUnit_1 = unit;
        }
        else if (selectedCount == 2)    //  为 2 时，当前已选中了第二个碎片，可进行位置的交换与胜利的判定
        {
            selectedUnit_2 = unit;
            
            selectedCount = 0;      //  立刻置为 0，保证下一轮交换顺利进行
            isHandling = true;      //  开始对当前选中的两碎片进行位置交换，此时不能再操作其它碎片
            
            UnitPosExchange();      //  位置交换

            if (GameWin())          //  胜利判断
            {
                Debug.Log("!!! Win !!!");
                winPanel.SetActive(true);
                ifGameOver = true;
                FreezeAllWhenWin();
            }
            Invoke("UnitDisSelected", disSelectDelay);  //  位置交换后，将选中效果去除
        }
        else
        {
            return;
        }
    }

    void UnitDisSelected()
    {
        selectedUnit_1.GetComponent<SelectUnit>().DisSelected();
        selectedUnit_2.GetComponent<SelectUnit>().DisSelected();
        isHandling = false;
    }

    //  交换两个碎片的位置，并修改它们在字典中的值
    void UnitPosExchange()
    {
        Vector3 unit1_Pos = selectedUnit_1.transform.position;
        Vector3 unit2_Pos = selectedUnit_2.transform.position;

        TweenPosition.Begin(selectedUnit_1, tweenDuration, unit2_Pos);
        TweenPosition.Begin(selectedUnit_2, tweenDuration, unit1_Pos);

        SetUnitDicById(selectedUnit_1.GetComponent<SelectUnit>().id, unit2_Pos);
        SetUnitDicById(selectedUnit_2.GetComponent<SelectUnit>().id, unit1_Pos);
    }

    bool GameWin()  //  根据id号获取各碎片的当前位置，若均与其正确位置一一对应，则胜利
    {
        bool ifWin = true;  //  先假定胜利条件为真
        InitUnitPosList();  //  重新初始化位置列表，以 unitPosList 为正确位置列表
        foreach (int elementId in this.unitDic.Keys)
        {
            if (this.unitDic[elementId] == this.unitPosList[elementId])   //  当前项的id号所对应的值与正确位置的值相等，表示本项处于正确位置，继续遍历下一项
            {
                continue;
            }
            else     //  一旦有某一项不处于正确的位置上，则判定为失败，无需继续遍历
            {
                ifWin = false;
                break;
            }
        }
        return ifWin;
    }

    //  根据元素id号(键)，修改其位置(值)
    void SetUnitDicById(int id, Vector2 newPos)
    {
        foreach(int elementId in unitDic.Keys)
        {
            if (elementId == id)    //  根据传入的id，在字典中找到该项，即修改项
            {
                unitDic[elementId] = newPos;    //  将值修改为传入的向量值，完成该项修改即可退出循环
                break;
            }
            else   // 否则继续遍历
            {
                continue;
            }
        }
    }

    //  游戏胜利后，对拼图不可再操作
    void FreezeAllWhenWin()
    {
        foreach (GameObject unit in this.unitObjList)
        {
            unit.GetComponent<SelectUnit>().gameOver = true;
        }
    }


    public void MouseInPuzzle()
    {
        mouseInPuzzle = true;
    }

    public void MouseOutPuzzle()
    {
        mouseInPuzzle = false;
    }

    public void ClickCloseWinPanel()
    {
        if(winPanel.activeSelf)
        {
            winPanel.SetActive(false);
        }
    }
}
