using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMoveDown : MonoBehaviour {
    public float maxSpeedValue;
    public float minSpeedValue;
    private float speed;

    void Start()
    {
        speed = Random.Range(minSpeedValue, maxSpeedValue) * -1;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
