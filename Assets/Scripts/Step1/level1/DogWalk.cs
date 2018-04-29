using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogWalk : MonoBehaviour {
    public float dogPosBegin;
    public float dogPosEnd;
    public float moveSpeed;
    public float moveDelay;
    private Animator anim;
    private int ifFacingRight = 1;
    private bool ifShouldMove = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (ifShouldMove)
        {
            Move();
            Stay();
        }
    }

    void Move()
    {
        transform.Translate(ifFacingRight * Vector3.right * moveSpeed * Time.deltaTime);
    }

    void Stay()
    {
        if (!(transform.position.x > dogPosBegin && transform.position.x < dogPosEnd))
        {
            ifShouldMove = false;
            anim.SetBool("ifStay", true);
            Invoke("FlipAndMove", moveDelay);
        }
    }

    void FlipAndMove()
    {
        ifFacingRight *= -1;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        ifShouldMove = true;
        anim.SetBool("ifStay", false);
    }
}
