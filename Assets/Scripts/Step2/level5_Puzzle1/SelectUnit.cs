using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUnit : MonoBehaviour {
    public int id;
    public bool gameOver = false;
    public string managerTagName;
    private PuzzleManager puzzleManager;
    private SpriteRenderer sprite;
    private bool isSelected;
    private bool mouseInPuzzle = false;
	// Use this for initialization
	void Start () {
        puzzleManager = GameObject.FindWithTag(managerTagName).GetComponent<PuzzleManager>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(gameOver)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1") && !isSelected && !gameOver && puzzleManager.mouseInPuzzle)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawLine(Input.mousePosition, transform.position, Color.red);
            RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, gameObject.transform.position);
            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.gameObject == this.gameObject)
                {
                    OnSelected();
                    puzzleManager.UnitSelected(gameObject);
                }
                else
                {
                    DisSelected();
                }
            }
            else
            {
                return;
            }
        }
	}

    public void OnSelected()
    {
        isSelected = true;
        sprite.color = Color.red;
    }

    public void DisSelected()
    {
        isSelected = false;
        sprite.color = Color.white;
    }
}
