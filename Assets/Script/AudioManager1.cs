using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
    [SerializeField]  AudioSource musicSource ;
    [SerializeField]  AudioSource sfxSource ;

    public AudioClip musicClip;
    public AudioClip takeObjectClip;
    public AudioClip dropObjectClip;
}
