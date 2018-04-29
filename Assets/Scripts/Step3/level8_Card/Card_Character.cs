using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card_Character : MonoBehaviour, IPointerClickHandler {
    public enum CardState
    {
        POSITIVE, NEGATIVE
    }

    public int cardID;
    protected CardState cardState = CardState.POSITIVE;
    protected bool isSuccess = false;
    protected bool isMatching = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSuccess) return;
        
        else
        {
            if (!isMatching)
            {
                FlipOver();
                SendCardID();
            }
        }
    }

    protected void FlipOver()
    {
        if(cardState == CardState.POSITIVE)
        {
            GetComponent<Animator>().SetTrigger("flipOver");
            cardState = CardState.NEGATIVE;
        }
    }

    protected void FlipBack()
    {
        if(cardState == CardState.NEGATIVE)
        {
            GetComponent<Animator>().SetTrigger("flipBack");
            cardState = CardState.POSITIVE;
        }
    }

    public void GetMatchResult(bool isMatched)
    {
        if (isSuccess) return;

        else
        {
            isSuccess = isMatched;
            
            if (!isSuccess)
                FlipBack();
        }
    }

    public void IsHandling(bool isMatching)
    {
        this.isMatching = isMatching;
    }

    public bool GetIsSuccess()
    {
        return this.isSuccess;
    }

    protected virtual void SendCardID() { }
}
