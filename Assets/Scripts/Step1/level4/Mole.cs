 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour {

    enum MoveState
    {
        Idle,       //  在洞里静止时
        MovingUp,   //  向上运动状态
        Stay,       //  停留地面状态
        MovingDown  //   向下运动状态
    }
    private MoveState moveState;
    private SpriteRenderer spriteRen;
    private bool ifGoodSprite;
    [Range(0, 1f)]
    private float niceGuyRate = 0.5f;

    public int moleID;
    public ScoreManager scoreManager;
    public float durationTime;
    public float repeatRate;
    public float invokeRepeatDelay;
    public float stayTime;
    
    public Sprite[] hitMole;
    public Sprite[] idleMole;

    public Vector3 moveUpTo;    //  destinated pos on moving up
    public Vector3 moveDownTo;  //  destinated pos on moving down

    void Awake()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //  invoke Move() in invokeRepeatDelay seconds, and then repeatedly every repeatRate seconds
        InvokeRepeating("Move", invokeRepeatDelay, repeatRate);
    }

    public float NiceGuyRate { get { return this.niceGuyRate; } set { this.niceGuyRate = value; } }
    public float BadGuyRate { get { return 1f - this.niceGuyRate; } }

    void Move()
    {
        moveState = MoveState.Idle;

        //  spawn bad guy
        if (Random.Range(0, 1f) >= niceGuyRate)
        {
            spriteRen.sprite = idleMole[0];
            ifGoodSprite = false;
        }

        //  spawn nice guy
        else
        {
            spriteRen.sprite = idleMole[1];
            ifGoodSprite = true;
        }

        if (Random.Range(0, 10) == moleID)
            Invoke("MoveUp", 0f);
    }

    void MoveUp()
    {
        moveState = MoveState.MovingUp;
        TweenPosition.Begin(gameObject, durationTime, moveUpTo);
        StartCoroutine(Stay());
    }

    IEnumerator Stay()
    {
        moveState = MoveState.Stay;
        yield return new WaitForSeconds(stayTime);
        MoveDown();
    }

    void MoveDown()
    {
        moveState = MoveState.MovingDown;
        TweenPosition.Begin(gameObject, durationTime, moveDownTo);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && (moveState == MoveState.MovingUp || moveState == MoveState.Stay))
        {
            //  create a ray from the input mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //  ray direction: Vector3.forward
            RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, Vector3.forward);

            //  if the ray hit anything
            if (hitInfo.transform != null)
            {
                //  if the ray hit itself
                if (hitInfo.transform.gameObject == gameObject)
                {
                    //  move down to go back to its hole
                    moveState = MoveState.MovingDown;

                    //  hit bad man to add score
                    if (ifGoodSprite == false)
                    {
                        spriteRen.sprite = hitMole[0];
                        scoreManager.score += 1;
                    }
                    //  hit good man to lose score
                    else
                    {
                        spriteRen.sprite = hitMole[1];
                        scoreManager.score -= 1;
                    }
                }
            }
        }
    }
}
