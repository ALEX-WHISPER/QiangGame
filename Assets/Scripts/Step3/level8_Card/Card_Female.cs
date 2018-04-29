using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Female : Card_Character {
    protected override void SendCardID()
    {
        GameObject.FindWithTag("GameController").GetComponent<CardMatchControl>().SetSelectingFemaleID(this.cardID);
    }
}
