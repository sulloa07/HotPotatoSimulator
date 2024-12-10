using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SFXSource;

    [Header("Potato Audio Clips")]
    public AudioClip potatoThrow; 
    public AudioClip potatoPickup; 
    public AudioClip potatoExplode; 

    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }
}
