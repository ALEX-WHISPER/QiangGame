 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour {

    enum MoveState
    {
        Idle,                         //  在洞里静止时
        MovingUp,            //  向上运动状态
        Stay,                       //  停留地面状态
        MovingDown      //   向下运动状态
    }
    private MoveState moveState;
    private SpriteRenderer spriteRen;
    private bool ifGoodSprite;

    public int moleID;
    public ScoreManager scoreManager;
    public float durationTime;
    public float repeatRate;
    public float invokeRepeatDelay;
    public float stayTime;
    public Sprite[] hitMole;
    public Sprite[] idleMole;

    public Vector3 moveUpTo;
    public Vector3 moveDownTo;
    void Awake()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating("Move", invokeRepeatDelay, repeatRate);
    }

    void Move()
    {
        moveState = MoveState.Idle;

        if (Random.Range(-4, 0) < -2)      //  范围：[-4, 0]
        {
            spriteRen.sprite = idleMole[0];
            ifGoodSprite = false;
        }
        else
        {
            spriteRen.sprite = idleMole[1];
            ifGoodSprite = true;
        }

        if (Random.Range(0, 9) == 2)
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(Input.mousePosition, transform.position, Color.red);
            RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, gameObject.transform.position);
            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.gameObject == gameObject)
                {
                    moveState = MoveState.MovingDown;

                    if (ifGoodSprite == false)
                    {
                        spriteRen.sprite = hitMole[0];
                        scoreManager.score += 1;
                    }
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
