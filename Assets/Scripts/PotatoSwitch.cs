using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    /*
     *The function of this script:
     *any player the potato touches becomes the new potato holder and automatically grabs the potato
     */
    void Start()
    {
        //pick someone to start as potato holder
    }

    void OnCollisionEnter(Collision coll)
    {
        //if collided thing is a player who is not the current potato holder, make them the potato holder and put the potato in their hands
    }
}
