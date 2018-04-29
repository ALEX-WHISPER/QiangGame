using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetBoyTrigger : MonoBehaviour {
    public bool ifBoyShow = false;

    void OnTriggerEnter2D(Collider2D other)
    {
         if(other.tag == "Player")
         {
             ifBoyShow = true;
         }
    }
}
