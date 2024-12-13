using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPotatoInterface : MonoBehaviour
{


    /*
     * This script attaches to the player
     * it controls:
     * Storing whether this player is HOLDING the potato
     * picking up the potato
     * Holding the potato (making kinematic and teleporting it to hand position)
     * throwing the potato (making non-kinematic and giving a velocity boost in the target direction)
     */

    private bool gameStarted = false;

    public float throwStrength;

    private int playerIndex;

    private bool holdingPotato;
    private Vector3 handPosition;

    private GameObject playerCam;

    private GameObject potato;
    private PotatoSwitch potatoLogic;

    private bool isPlayerIn;

    [SerializeField]
    private Text potatoHolderText;

    private Alteruna.Avatar _avatar;

    private AudioManager audioManager;
    private GameManager gameManager;

    void Start()
    {

        _avatar = GetComponent<Alteruna.Avatar>();

        if (!_avatar.IsMe)
            return;

        //find camera
        playerCam = gameObject.transform.GetChild(0).gameObject;

        // quincy - find audio source
        audioManager = FindObjectOfType<AudioManager>();

        // quincy - make sure the potato holder text starts off 
        if (potatoHolderText != null)
        {
            potatoHolderText.gameObject.SetActive(false);
        }
    }

    //this is called by the GameManager to tell the player what's what when the game begins
    public void learnPotato(GameObject thePotato, GameObject theManager, int playerListIndex)
    {
        if (!_avatar.IsMe)
            return;

        potato = thePotato;
        potatoLogic = thePotato.GetComponent<PotatoSwitch>();

        gameManager = theManager.GetComponent<GameManager>();

        playerIndex = playerListIndex;

    }

    void OnCollisionEnter(Collision coll)
    {
        if (!_avatar.IsMe)
            return;

        //if I collide with the potato, put it in my hands and make me the potato holder if I'm not already
        if (coll.gameObject.CompareTag("Potato"))
        {
            pickUpPotato();
        }
    }

    void Update()
    {

        if (!_avatar.IsMe)
            return;

        if (!gameStarted && Input.GetAxis("Submit") > 0.1)
        {
            gameStarted = true;
            GameObject manager = GameObject.Find("GameManager");
            GameManager managerScript = manager.GetComponent<GameManager>();
            // managerScript.gameBegins();
        }


        updateHandPosition();

        //if we're holding the potato, make it throwable, move it to our hand position
        // quincy - and show text that tells player they have it
        if (holdingPotato)
        {
            if (Input.GetAxis("Fire1") > 0)
            {
                throwPotato();
            }
            potato.transform.position = handPosition;
            potatoHolderText.gameObject.SetActive(true);
        }
        else if (!holdingPotato)
        {
            potatoHolderText.gameObject.SetActive(false);
        }

        //if we click, attempt to throw the potato

    }

    void updateHandPosition()
    {

        if (!_avatar.IsMe)
            return;

        //update handPosition: let's say for now it's just a little above the player and we can change this as necessary
        // quincy - potato doesnt turn with the camera so leaving it above player and adding text that tells player they have it
        handPosition.x = gameObject.transform.position.x;
        handPosition.y = gameObject.transform.position.y + 2;
        handPosition.z = gameObject.transform.position.z;

    }

    public void pickUpPotato()
    {

        if (!_avatar.IsMe)
            return;

        //put the potato in my hands
        holdingPotato = true;
        potatoLogic.turnOffGravity();

        // quincy - play sound
        if (audioManager != null && audioManager.potatoPickup != null)
        {
            audioManager.PlaySFX(audioManager.potatoPickup);
        }

        //tell the GameManager that we have the potato now
        gameManager.setPotatoHaver(gameObject);

    }

    public void throwPotato()
    {

        if (!_avatar.IsMe)
            return;

        holdingPotato = false;
        potatoLogic.turnOnGravity();

        // quincy - play sound
        if (audioManager != null && audioManager.potatoThrow != null)
        {
            audioManager.PlaySFX(audioManager.potatoThrow);
        }

        //give potato a velocity boost in the direction the player is lookin
        Vector3 lookingDir = playerCam.transform.forward;

        Vector3 throwVel = lookingDir * throwStrength;

        potatoLogic.setVelocity(throwVel);

    }

    public bool isHoldingPotato()
    {

        if (!_avatar.IsMe)
            return false;

        return holdingPotato;
    }

    public void setIndex(int newIndex)
    {

        if (!_avatar.IsMe)
            return;

        playerIndex = newIndex;
    }

    public int getIndex()
    {

        return playerIndex;
    }

}
