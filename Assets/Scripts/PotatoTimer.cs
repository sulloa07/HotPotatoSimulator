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
    */

    public int timerMin = 1; //measured in seconds
    public int timerMax = 60; //min and max are inclusive

    private float timeRemaining;

    void Start()
    {
        timeRemaining = (float)Random.Range(timerMin, timerMax + 1);
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
    }
}
