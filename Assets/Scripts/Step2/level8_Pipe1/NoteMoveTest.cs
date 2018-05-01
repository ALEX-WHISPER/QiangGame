using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMoveTest : MonoBehaviour
{
    public float moveSpeed;
    public float speedValueGrowRate;
    public float speedTimeGrowRate;
    public float growScaleRate;
    public float growTimeRate;

    [HideInInspector]
    public bool ifStop = false;

    private float nextGrow = 0f;
    private float nextSpeedUp = 0f;

    void Start()
    {
        //MoveDown();
        Debug.Log("Start: " + Time.time);
    }

    void FixedUpdate()
    {
        if (ifStop)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }

        //  scaling up every fixed interval
        if (Time.time > nextGrow && !ifStop)
        {
            nextGrow = Time.time + growTimeRate;
            ScaleGrow();
        }

        //  speeding up every fixed interval
        if(Time.time > nextSpeedUp)
        {
            nextSpeedUp = Time.time + speedTimeGrowRate;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1 * moveSpeed * Time.deltaTime);
            moveSpeed += speedValueGrowRate;
        }
    }

    void MoveDown()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -1 * moveSpeed * Time.deltaTime);
    }

    void ScaleGrow()
    {
        Vector2 currentScale = transform.localScale;
        Vector2 newScale = new Vector2(currentScale.x + Mathf.Sign(currentScale.x) * growScaleRate, currentScale.y + growScaleRate);
        transform.localScale = newScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Finish")
        {
            Debug.Log("End: " + Time.time);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
