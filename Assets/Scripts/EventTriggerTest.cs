using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventTriggerTest : EventTrigger {

    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;    //  点击
    public VoidDelegate onPressDown;    //  按下
    public VoidDelegate onPressUp;          //  抬起
    public VoidDelegate onPointerEnter; //  鼠标进入
    public VoidDelegate onPointerExit;      //  鼠标移出

    static public EventTriggerTest Get(GameObject go)
    {
        EventTriggerTest test = go.GetComponent<EventTriggerTest>();

        if (test == null)
        {
            test = go.AddComponent<EventTriggerTest>();
        }

        return test;
    }

    static public EventTriggerTest Get(Transform transform)     //  重载
    {
        EventTriggerTest test = transform.GetComponent<EventTriggerTest>();

        if (test == null)
        {
            test = transform.gameObject.AddComponent<EventTriggerTest>();
        }
        return test;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
            onClick(gameObject);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onPressDown != null)
            onPressDown(gameObject);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onPressUp != null)
            onPressUp(gameObject);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onPointerEnter != null)
            onPointerEnter(gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onPointerExit != null)
            onPointerExit(gameObject);
    }
}
