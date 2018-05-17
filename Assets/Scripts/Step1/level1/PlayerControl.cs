using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public ClicktoMove clickMoveScript;
    public float moveSpeed; //  运动速度

    private bool ifFacingRight = true;  //  是否正面朝右侧
    private Animator playerAnim;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (clickMoveScript.ifCanMove) {
            //  move towards the direction of the target
            transform.Translate(Mathf.Sign(clickMoveScript.distance) * Vector3.right * moveSpeed * Time.deltaTime);

            //  anim: turn to walk
            PlayerWalkAnim_Play();

            //  check if flipping needed
            Flip();
        } else {
            //  turn to idle
            PlayerWalkAnim_Stop();
        }
    }

    //  转身
    void Flip()
    {
        //  目标位置在右侧，但面朝左；目标位置在左侧，但面朝右
        if ((Mathf.Sign(clickMoveScript.distance) < 0 && ifFacingRight) || 
            (Mathf.Sign(clickMoveScript.distance) > 0 && !ifFacingRight))
        {
            ifFacingRight = !ifFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    //  动画状态：idle -> walk
    public void PlayerWalkAnim_Play()
    {
        playerAnim.SetBool("Walk", true);
    }

    //  动画状态：walk -> idle
    public void PlayerWalkAnim_Stop()
    {
        playerAnim.SetBool("Walk", false);
    }
}
