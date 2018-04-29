using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragUnit : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {
    [Serializable]
    public class Area
    {
        public int max_X;
        public int min_X;
        public int max_Y;
        public int min_Y;
    }

    public Area dragArea;
    private Vector3 newPosition;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //rectTransform.position = new Vector2
        //    (
        //        Mathf.Clamp(GetComponent<RectTransform>().position.x, dragArea.min_X, dragArea.max_X),
        //        Mathf.Clamp(GetComponent<RectTransform>().position.y, dragArea.min_Y, dragArea.max_Y)
        //    );
        transform.position = new Vector2
            (
                Mathf.Clamp(transform.position.x, dragArea.min_X, dragArea.max_X),
                Mathf.Clamp(transform.position.y, dragArea.min_Y, dragArea.max_Y)
            );
	}

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle
            (rectTransform, Input.mousePosition, eventData.enterEventCamera, out newPosition);
        transform.position = newPosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
 
    }
}
