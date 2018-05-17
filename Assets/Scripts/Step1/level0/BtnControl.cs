using UnityEngine;

/// <summary>
/// Hit goat head to enter game
/// </summary>
public class BtnControl : MonoBehaviour
{
    public GameObject goatAnim;
    public GameObject bgAnim;
    public GameObject girlAnim;

    public float girlAnimDelay;

    public void ClickStartBtn()
    {
        //  羊头动画(position, scale)
        goatAnim.GetComponent<Animator>().SetTrigger("TurnSmaller");

        //  背景渐变动画(透明度)
        bgAnim.GetComponent<Animator>().SetTrigger("ShowBackground");

        //  延迟调用女孩行走动画
        Invoke("GirlWalkAnim", girlAnimDelay);
    }

    void GirlWalkAnim()
    {
        //  女孩行走动画
        girlAnim.GetComponent<Animator>().SetTrigger("GirlWalk");
        GetComponent<EnterNextLevel>().enabled = true;
    }
}
