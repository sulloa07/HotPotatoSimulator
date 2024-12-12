using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
     * GameManager is responsible for these things:
     * storing who is the current potato posessor and informing them with all necessary methods
     * Selecting a player to start as the potato posessor
     * Spawning the potato when the game begins
     * Teleporting players to spawn points when the game begins
     * Selecting a player to hold the potato when the round begins
     * Telling the player when to start its timer
     * Managing the Explosion to end a round
     * 
     */

    public GameObject potatoPrefab;
    private GameObject potato;

    private PotatoSwitch potatoLogic;
    private PotatoTimer potatoTimer;

    private List<PlayerPotatoInterface> allPlayers = new List<PlayerPotatoInterface>(); //list of all players. Will never be altered once filled.
    private List<PlayerPotatoInterface> inPlayers = new List<PlayerPotatoInterface>();  //list of players who are still in the game. Will be reduced when a player Explodes.

    private GameObject potatoHaver;


    // Start is called before the first frame update
    void Start()
    {

    }

    /*
     * Activates when the game Starts
     * Gets a list of all players
     * Makes all players In
     * Teleports all players to spawnpoints
     * Spawns potato
     * Informs all players of potato object reference
     */
    public void gameBegins()
    {
        //get list of players
        GameObject[] allPlayerGameObjects = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < allPlayerGameObjects.Length; i++)
        {
            PlayerPotatoInterface thisPlayerScript = allPlayerGameObjects[i].GetComponent<PlayerPotatoInterface>();
            allPlayers.Add(thisPlayerScript);
            inPlayers.Add(thisPlayerScript); //all players start In
        }

        //find list of spawnpoints
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoint");

        //teleport each player to a spawnpoint
        for (int i = 0; i < allPlayers.Count; i++)
        {
            GameObject thisPlayer = allPlayers[i].gameObject;
            GameObject thisSpawn = spawnpoints[i];

            Vector3 spawnPos = thisSpawn.transform.position;
            spawnPos.y += 2;

            thisPlayer.transform.position = spawnPos;
        }

        //create the potato
        potato = Instantiate<GameObject>(potatoPrefab);
        potatoLogic = potato.GetComponent<PotatoSwitch>();
        potatoTimer = potato.GetComponent<PotatoTimer>();

        //inform each player of object references and grants them each an index which they can communicate to us with
        for (int i = 0; i < allPlayers.Count; i++)
        {
            allPlayers[i].learnPotato(potato, gameObject, i);
        }
        //inform the potato of object references
        potatoLogic.instantiatePotato(gameObject);
        potatoTimer.instantiate(gameObject);

    }


    /*
     * Activates when a round starts (just after an explosion or when the game begins)
     * If there is only one player In, they win.
     * Otherwise
     * out of all players currently In the game, select one to hold the potato
     * activate their PickUpPotato method
     * start the potato's timer
     */
    public void roundBegins()
    {
        if (inPlayers.Count < 2)
        {
            //TODO win game
        }
        else
        {
            int whichPlayer = Random.Range(0, inPlayers.Count); //gets random index in in-players list
            inPlayers[whichPlayer].pickUpPotato();

            //set player indices so they can later tell us where they are in our inPlayers list
            for (int i = 0; i < inPlayers.Count; i++)
            {
                inPlayers[i].setIndex(i);
            }
        }

        potatoTimer.startTimer();
    }
    // Update is called once per frame
    void Update()
    {

    }

    /*
     * round-ending explosion
     * potatoHaver object -> physically remove in some way (animation?? we should def spruce this up somehow)
     * get its script component and ask for its index to find which player it is in our inPlayersList
     * remove that player from the inPlayersList to make them be Out
     * start new round with currently-in players
     */
    public void Explode()
    {
        Vector3 newHaverPosition = potatoHaver.transform.position;
        newHaverPosition.y = -30;
        potatoHaver.transform.position = newHaverPosition;

        PlayerPotatoInterface potatoHaverScript = potatoHaver.GetComponent<PlayerPotatoInterface>();
        int haverIndex = potatoHaverScript.getIndex();

        inPlayers.RemoveAt(haverIndex);

        roundBegins();
    }

    public void setPotatoHaver(GameObject haver)
    {
        potatoHaver = haver;
    }
}