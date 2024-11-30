using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PotatoSwitch : MonoBehaviour
{
    /*
     *The function of this script:
     *control Who Holds The Potato
     */

    private GameObject potatoHaver;
    private PlayerPotatoInterface potatoHaverLogic;

    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        //pick someone to start as potato holder
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int picked = Random.Range(0, players.Length - 1);
        potatoHaver = players[picked];
        potatoHaverLogic = potatoHaver.GetComponent<PlayerPotatoInterface>();
        //give that player the potato
        potatoHaverLogic.pickUpPotato();
    }

    public void setPotatoHaver(GameObject newHaver)
    {
        potatoHaver = newHaver;
        potatoHaverLogic = potatoHaver.GetComponent<PlayerPotatoInterface>();

    }

    public GameObject getPotatoHaver()
    {
        return potatoHaver;
    }

    public void turnOffGravity()
    {
        rb.isKinematic = true;
    }
    public void turnOnGravity()
    {
        rb.isKinematic = false;
    }
    
    public void setVelocity(Vector3 vel)
    {
        rb.velocity = vel;
    }

    
}
