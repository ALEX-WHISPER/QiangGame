using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Male : Card_Character {
    protected override void SendCardID()
    {
        GameObject.FindWithTag("GameController").GetComponent<CardMatchControl>().SetSelectingMaleID(this.cardID);
    }
}
