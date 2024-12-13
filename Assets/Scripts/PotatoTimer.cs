using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
public class PotatoTimer : AttributesSync
{

    /*
     * The function of this script:
     * when told to by the GameManager, start a timer
     * when timer runs out, tell GameManager to perform an explosion.
     * 
    */

    [SynchronizableField]
    public int timerMin = 15; //measured in seconds

    [SynchronizableField]
    public int timerMax = 60; //min and max are inclusive

    [SynchronizableField]
    public bool timerGoing = false;

    
    private GameManager gameManager;

    [SynchronizableField]
    private float timeRemaining;

    
    private AudioManager audioManager;


    public void instantiate(GameObject gameManagerGO)
    {
        gameManager = gameManagerGO.GetComponent<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void startTimer()
    {
        timerGoing = true;
        timeRemaining = (float)Random.Range(timerMin, timerMax + 1);
    }

    void FixedUpdate()
    {
        if (timerGoing)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timerGoing = false;
                Explode();
            }
        }


    }

    void Explode()
    {
        gameManager.Explode();

        // quincy - play sound
        if (audioManager != null && audioManager.potatoExplode != null)
        {
            audioManager.PlaySFX(audioManager.potatoExplode);
        }

    }

}
