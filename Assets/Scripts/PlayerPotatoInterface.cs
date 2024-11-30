using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPotatoInterface : MonoBehaviour
{


    /*
     * This script attaches to the player
     * it controls:
     * picking up the potato
     * Holding the potato
     * Storing whether the player is the current potato haver and whether they're holding the potato
     * throwing the potato
     */

    public float throwStrength;

    private bool hasPotato;
    private bool holdingPotato;
    private Vector3 handPosition;

    private GameObject playerCam;

    private GameObject potato;
    private PotatoSwitch potatoLogic;

    void Start()
    {
        //find potato
        potato = GameObject.FindGameObjectsWithTag("Potato")[0];
        potatoLogic = potato.GetComponent<PotatoSwitch>();

        //find camera
        playerCam = gameObject.transform.GetChild(0).gameObject;
    }

    void OnCollisionEnter(Collision coll)
    {
        //if I collide with the potato, put it in my hands and make me the potato holder if I'm not already
        if (coll.gameObject.CompareTag("Potato"))
        {
            pickUpPotato();
        }
    }

    void Update()
    {
        updateHandPosition();

        //if we're holding the potato, move it to our hand position
        if (holdingPotato)
        {
            potato.transform.position = handPosition;
        }

        //if we click, attempt to throw the potato
        if (Input.GetAxis("Fire1") > 0)
        {
            throwPotato();
        }
    }

    void updateHandPosition()
    {
        //update handPosition: let's say for now it's just a little above the player and we can change this as necessary

        handPosition.x = gameObject.transform.position.x;
        handPosition.y = gameObject.transform.position.y + 2;
        handPosition.z = gameObject.transform.position.z;
        
    }

    public void pickUpPotato()
    {
        //make me the potato haver if I'm not already, and put the potato in my hands
        hasPotato = true;
        holdingPotato = true;

        //tell the potato that we are the current holder (For explosion purposes)
        potatoLogic.setPotatoHaver(gameObject);
        potatoLogic.turnOffGravity();

    }

    public void throwPotato()
    {
        holdingPotato = false;
        potatoLogic.turnOnGravity();

        //give potato a velocity boost in the direction the player is lookin
        Vector3 lookingDir = playerCam.transform.forward;

        Vector3 throwVel = lookingDir * throwStrength;

        potatoLogic.setVelocity(throwVel);

    }

    //just in case
    public bool isPotatoHaver()
    {
        return hasPotato;
    }

    public bool isHoldingPotato()
    {
        return holdingPotato;
    }

}
