using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DinnerConversation : MonoBehaviour, IPointerClickHandler {
    public GameObject[] conObj;
    private int clickCount = -1;

    void Start()
    {
        SetAllConHidden();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 1)
        {
            SetIndexConActive(++clickCount);
        }
    }

    private void SetIndexConActive(int conIndex)
    {
        for (int i = 0; i < conObj.Length; i++ )
        {
            if (i == conIndex)
            {
                conObj[i].SetActive(true);
            }
            else
            {
                conObj[i].SetActive(false);
            }
        }
    }

    private void SetAllConHidden()
    {
        for (int i = 0; i < conObj.Length; i++ )
        {
            conObj[i].SetActive(false);
        }
    }

    public int GetConversationIndex()
    {
        return this.clickCount;
    }
}
