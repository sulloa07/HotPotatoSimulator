using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    /*
     *The function of this script:
     *control Who Holds The Potato
     */

    public GameObject potatoHaver;

    void Start()
    {
        //pick someone to start as potato holder
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int picked = Random.Range(0, players.Length - 1);
        potatoHaver = players[picked];

        //put potato in their hands
        //TODO: when we know what that entails in terms of Player Conditions and whatnot
    }

    void OnCollisionEnter(Collision coll)
    {
        //if collided thing is a player who is not the current potato holder, make them the potato holder and put the potato in their hands
        if (coll.gameObject.CompareTag("Player"))
        {
            potatoHaver = coll.gameObject;

            //put potato in their hands
            //TODO: when we know what that entials in terms of Player Conditions and whatnot
        }
    }
}
