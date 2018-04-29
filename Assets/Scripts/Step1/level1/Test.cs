using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Test : MonoBehaviour {

    private bool isUp;
    private Image img;
    void Start () 
    {
        //EventTriggerTest.Get(gameObject).onPressDown += (go) => { Debug.Log("按下！"); };
        //EventTriggerTest.Get(gameObject).onPressUp += (go) => { Debug.Log("抬起！"); };
        /*EventTriggerListener.Get(gameObject).onSelect += (go) => { Debug.Log("选中！"); };
        EventTriggerListener.Get(gameObject).onEnter += (go) => { Debug.Log("进入！"); };
        EventTriggerListener.Get(gameObject).onExit += (go) => { Debug.Log("退出！"); };*/

        img = GetComponent<Image>();
        EventTriggerTest.Get(gameObject).onPressDown += OnClickDown;
        EventTriggerTest.Get(gameObject).onPressUp += OnClickUp;
    }
    void OnClickDown(GameObject go)
    {
        isUp = false;
        StartCoroutine(grow());
    }
    void OnClickUp(GameObject go)
    {
        isUp = true;
        img.fillAmount = 0f;
    }
    private IEnumerator grow()
    {
        while (true)
        {
            if (isUp)
            {
                break;
            }
            img.fillAmount += 0.5f * Time.deltaTime;
            if (img.fillAmount == 0)
            {
                break;
            }
            yield return null;
        }
    }
}
