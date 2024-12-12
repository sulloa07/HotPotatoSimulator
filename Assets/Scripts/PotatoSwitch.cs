using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PotatoSwitch : MonoBehaviour
{
    /*
     *Responsibilities of this script:
     *turn gravity on or off and set our velocity
     *that's basically it. This used to control who held the potato till i realized
     *that was better left to an external manager.
     */

    private GameManager gameManager;

    private Rigidbody rb;

    public void instantiatePotato(GameObject gameManagerGO)
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameManager = gameManagerGO.GetComponent<GameManager>();
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
