using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCard : MonoBehaviour {
    public GameObject card;
    public GameObject cardInBag;
    public Transform bagPanel;
    public float cardShowDuration;
    public float cardShowDelay = 0;
    public Vector3 cardShowScale;
    public Sprite cardFrontSide;
    public Sprite cardBackSide;

    [HideInInspector]
    public bool ifCardShowing = false;

    private GameObject card_Obj;
    private GameObject cardInBag_Obj;
    private bool ifCardOnFrontSide = true;

    public void ShowCard()
    {
        //  卡片展示
        Invoke("CardTweenScale", cardShowDelay);

        //  层级置顶
        GetComponent<ClickBtn>().isInBtn = true;

        //  卡片收纳
        TakeCardInBag();

        ifCardShowing = true;
    }

    void CardTweenScale()
    {
        TweenScale.Begin(card, cardShowDuration, cardShowScale);
    }

    public void TurnCard()
    {
        if (ifCardOnFrontSide)
        {
            GameObject.FindWithTag("Card").GetComponent<Image>().sprite = cardBackSide;
            ifCardOnFrontSide = false;
        }
        else 
        {
            GameObject.FindWithTag("Card").GetComponent<Image>().sprite = cardFrontSide;
            ifCardOnFrontSide = true;
        }
            
    }

    public void ClickCloseCard()
    {
        card.SetActive(false);
        GetComponent<ClickBtn>().isInBtn = false;

        ifCardShowing = false;
    }

    void TakeCardInBag()
    {
        //  卡片作为新道具被收纳进背包中
        cardInBag_Obj = Instantiate(cardInBag, cardInBag.transform.position, cardInBag.transform.rotation) as GameObject;
        cardInBag_Obj.transform.SetParent(bagPanel, false);
    }
}
