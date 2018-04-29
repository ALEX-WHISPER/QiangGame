using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public ClicktoMove clickMoveScript;
    public float moveSpeed;

    private bool ifFacingRight = true;
    private Animator playerAnim;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (clickMoveScript.ifCanMove)
        {
            transform.Translate(Mathf.Sign(clickMoveScript.distance) * Vector3.right * moveSpeed * Time.deltaTime);
            PlayerWalkAnim_Play();
            Flip();
        }
        else
            PlayerWalkAnim_Stop();
    }

    void Flip()
    {
        //  朝右运动，但面朝左；朝左运动，但面朝右
        if ((Mathf.Sign(clickMoveScript.distance) < 0 && ifFacingRight) || 
            (Mathf.Sign(clickMoveScript.distance) > 0 && !ifFacingRight))
        {
            ifFacingRight = !ifFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void PlayerWalkAnim_Play()
    {
        playerAnim.SetBool("Walk", true);
    }

    public void PlayerWalkAnim_Stop()
    {
        playerAnim.SetBool("Walk", false);
    }
}
