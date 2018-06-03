using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardMatchControl : MonoBehaviour {
    public Card_Male[] cards_Male;
    public Card_Female[] cards_Female;
    public Card_Character[] cards_All;
    public GameObject UIPanel;
    public string nextLevelName;
    private int selectedCount = 0;
    private int maleCardID = 0;
    private int femaleCardID = 0;

    private bool isAlreadyMale = false;
    private bool isAlreadyFemale = false;
    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        MatchControl();

        //  if game over, activate the result panel in 2 seconds
        if (CheckGameOver())
        {
            isGameOver = true;
            Invoke("UIPanelActive", 2f);
        }
    }

    public void MatchControl()
    {
        if(selectedCount == 2)
        {
            SendMatching();

            //  once the selected id is equal, set this match be success
            if (maleCardID == femaleCardID)
            {
                MatchSuccess();
            }
            else
            {
                MatchFail();
            }
            SendMatched();
            ResetMatch();
        }
    }

    public void SetSelectingMaleID(int maleCardID)
    {
        if (isAlreadyMale)
        {
            for (int i = 0; i < cards_Male.Length; i++)
            {
                cards_Male[i].GetMatchResult(false);
            }
            ResetMatch();
            return;
        }
        else
        {
            this.maleCardID = maleCardID;
            CountingControl();
            isAlreadyMale = true;
        }
    }

    public void SetSelectingFemaleID(int femaleCardID)
    {
        //  if the previous one is already female, return the result of false
        if (isAlreadyFemale)
        {
            for (int i = 0; i < cards_Female.Length; i++)
            {
                cards_Female[i].GetMatchResult(false);
            }
            ResetMatch();
            return;
        }
        else
        {
            //  set this matching female card's id
            this.femaleCardID = femaleCardID;

            //  add the selected count
            CountingControl();

            isAlreadyFemale = true;
        }
    }

    private void CountingControl()
    {
        if(selectedCount < 2)
            selectedCount++;
    }

    //  the current 2 selected characters are matched successfully
    private void MatchSuccess()
    {
        cards_Male[maleCardID].GetMatchResult(true);
        cards_Female[femaleCardID].GetMatchResult(true);
    }

    //  the current 2 selected characters are failed to match
    private void MatchFail()
    {
        cards_Male[maleCardID].GetMatchResult(false);
        cards_Female[femaleCardID].GetMatchResult(false);
    }

    //  the current 2 selected characters in under handling
    private void SendMatching()
    {
        cards_Male[maleCardID].IsHandling(true);
        cards_Female[femaleCardID].IsHandling(true);
    }

    //  matching process is over
    private void SendMatched()
    {
        cards_Male[maleCardID].IsHandling(false);
        cards_Female[femaleCardID].IsHandling(false);
    }

    private void ResetMatch()
    {
        selectedCount = 0;
        maleCardID = 0;
        femaleCardID = 0;
        isAlreadyMale = false;
        isAlreadyFemale = false;
    }

    //  游戏胜利条件判断：所有角色匹配状态均为成功时，游戏胜利，否则游戏失败
    private bool CheckGameOver()
    {
        foreach(Card_Character card in cards_All)
        {
            if(!card.GetIsSuccess())
            {
                return false;
            }
        }
        return true;
    }

    //  activate victory UI panel
    private void UIPanelActive()
    {
        UIPanel.SetActive(true);
    }

    //  click the btn in victory panel to enter the next level
    public void ClickExitBtn()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
