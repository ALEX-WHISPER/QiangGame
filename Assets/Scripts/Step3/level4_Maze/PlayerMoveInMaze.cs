using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMoveInMaze : MonoBehaviour {
    public float reMovePeriod;
    public LayerMask blockingLayer;
    public float inverseMoveTime;
    public GameObject UIPanel;
    public string nextLevelName;

    private float nextMove = 0f;
    private bool victory = false;
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        private Vector2 touchOrigin = -Vector2.one;	//Used to store location of screen touch origin for mobile controls.
#endif
    void Update()
    {
        if (victory) return;

        int h = 0;
        int v = 0;

        //if (Time.time > nextMove)
        //{
        //    nextMove = Time.time + reMovePeriod;
        //    h = (int)Input.GetAxisRaw("Horizontal");
        //    v = (int)Input.GetAxisRaw("Vertical");

        //    if (Mathf.Abs(h) != 0)
        //    {
        //        h = 1 * (int)Mathf.Sign(h);
        //        v = 0;
        //        AttemptMove(h, v);
        //    }
        //    if (Mathf.Abs(v) != 0)
        //    {
        //        h = 0;
        //        v = 1 * (int)Mathf.Sign(v);
        //        AttemptMove(h, v);
        //    }
        //}

#if UNITY_EDITOR
        if(Time.time > nextMove)
        {
            nextMove = Time.time + reMovePeriod;
            h = (int)Input.GetAxisRaw("Horizontal");
            v = (int)Input.GetAxisRaw("Vertical");

           if (Mathf.Abs(h) != 0)
            {
                h = 1 * (int)Mathf.Sign(h);
                v = 0;
                AttemptMove(h, v);
            }
            if(Mathf.Abs(v) != 0)
            {
                h = 0;
                v = 1 * (int)Mathf.Sign(v);
                AttemptMove(h, v);
            }
        }
#elif UNITY_ANDROID
        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0)
        {
            //Store the first touch detected.
            Touch myTouch = Input.touches[0];

            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            {
                //If so, set touchOrigin to the position of that touch
                touchOrigin = myTouch.position;
            }

            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                //Set touchEnd to equal the position of this touch
                Vector2 touchEnd = myTouch.position;

                //Calculate the difference between the beginning and end of the touch on the x axis.
                float x = touchEnd.x - touchOrigin.x;

                //Calculate the difference between the beginning and end of the touch on the y axis.
                float y = touchEnd.y - touchOrigin.y;

                //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                touchOrigin.x = -1;

                //Check if the difference along the x axis is greater than the difference along the y axis.
                if (Mathf.Abs(x) > Mathf.Abs(y))
                //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                {
                    h = x > 0 ? 1 : -1;
                    v = 0;
                    AttemptMove(h, v);
                }
                else
                {
                    //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                    v = y > 0 ? 1 : -1;
                    h = 0;
                    AttemptMove(h, v);
                }
            }
        }
#endif
    }

    protected void AttemptMove(int h, int v)
    {
        Debug.Log(string.Format("h: {0}, v: {1}", h, v));
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(h, v);

        RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);

        if (hit.transform == null) {
            StartCoroutine(SmoothMovement(end));
        }
    }

    protected IEnumerator SmoothMovement(Vector3 endPos)
    {
        float sqrRemainingDistance = (transform.position - endPos).sqrMagnitude;

        while(sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPos = Vector3.MoveTowards(GetComponent<Rigidbody2D>().position, endPos, inverseMoveTime * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(newPos);
            sqrRemainingDistance = (transform.position - endPos).sqrMagnitude;
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Card")
        {
            UIPanel.SetActive(true);
            victory = true;
        }
    }

    public void ClickExitMaze()
    {
        Invoke("EnterNextLevel", 1f);
    }

    private void EnterNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
