using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioClips : MonoBehaviour
{
    public static AudioClips audioClips;
    public AudioClip gameMusic;
    public AudioClip menuMusic;
    public AudioClip bulletHit;
    public AudioClip buttonClick;
    public AudioClip powerup;
    public AudioClip tankDeath;
    public AudioClip tankFire;
}
