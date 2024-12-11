using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Text potatoHolderText; 

     private AudioManager audioManager;

    void Start()
    {
        

        //find potato
         //potato = GameObject.FindGameObjectsWithTag("Potato")[0];
         //potatoLogic = potato.GetComponent<PotatoSwitch>();

        //find camera
        playerCam = gameObject.transform.GetChild(0).gameObject;

        // quincy - find audio source
        audioManager = FindObjectOfType<AudioManager>();

        // quincy - make sure the potato holder text starts off 
        if (potatoHolderText != null) {
            potatoHolderText.gameObject.SetActive(false);
        } 
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
        // quincy - and show text that tells player they have it
        if (holdingPotato)
        {
            potato.transform.position = handPosition;
            potatoHolderText.gameObject.SetActive(true);
        } else if (!holdingPotato) {
            potatoHolderText.gameObject.SetActive(false);
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
        // quincy - potato doesnt turn with the camera so leaving it above player and adding text that tells player they have it
        handPosition.x = gameObject.transform.position.x;
        handPosition.y = gameObject.transform.position.y + 2;
        handPosition.z = gameObject.transform.position.z;
        
    }

    public void pickUpPotato()
    {
        //make me the potato haver if I'm not already, and put the potato in my hands
        hasPotato = true;
        holdingPotato = true;

        // quincy - play sound
        if (audioManager != null && audioManager.potatoPickup != null) {
            audioManager.PlaySFX(audioManager.potatoPickup);
        }

        //tell the potato that we are the current holder (For explosion purposes)
        potatoLogic.setPotatoHaver(gameObject);
        potatoLogic.turnOffGravity();

    }

    public void throwPotato()
    {
        holdingPotato = false;
        potatoLogic.turnOnGravity();

        // quincy - play sound
        if (audioManager != null && audioManager.potatoThrow != null) {
            audioManager.PlaySFX(audioManager.potatoThrow);
        }

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
