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
            this.femaleCardID = femaleCardID;
            CountingControl();
            isAlreadyFemale = true;
        }
    }

    private void CountingControl()
    {
        if(selectedCount < 2)
            selectedCount++;
    }

    private void MatchSuccess()
    {
        cards_Male[maleCardID].GetMatchResult(true);
        cards_Female[femaleCardID].GetMatchResult(true);
    }

    private void MatchFail()
    {
        cards_Male[maleCardID].GetMatchResult(false);
        cards_Female[femaleCardID].GetMatchResult(false);
    }

    private void SendMatching()
    {
        cards_Male[maleCardID].IsHandling(true);
        cards_Female[femaleCardID].IsHandling(true);
    }

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

    private void UIPanelActive()
    {
        UIPanel.SetActive(true);
    }

    public void ClickExitBtn()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
