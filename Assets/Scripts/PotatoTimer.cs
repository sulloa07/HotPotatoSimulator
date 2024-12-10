using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoTimer : MonoBehaviour
{

    /*
     * The function of this script:
     * when the potato is brought into existence start a timer
     * constantly tick down
     * when it hits 0, blow up and also blow up the player who currently posesses the potato
     * 
    */

    public int timerMin = 15; //measured in seconds
    public int timerMax = 60; //min and max are inclusive

    private PotatoSwitch potatoSwitch;

    private float timeRemaining;

    private AudioManager audioManager;

    void Start()
    {
        timeRemaining = (float)Random.Range(timerMin, timerMax + 1);
        potatoSwitch = GetComponent<PotatoSwitch>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void FixedUpdate()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        //find player whose status is Holding Potato
        //blow them up and blow potato up
        //blowing up player: play animation and make them invisible/immovable
        //blowing up potato: play animation and delete gameobject

        GameObject holder = potatoSwitch.getPotatoHaver();

        //play explosion animation for player and make invisible/immovable

        //play explosion animation for potato

        // quincy - play sound
        if (audioManager != null && audioManager.potatoExplode != null) {
            audioManager.PlaySFX(audioManager.potatoExplode);
        }

        Object.Destroy(gameObject);

    }

}
