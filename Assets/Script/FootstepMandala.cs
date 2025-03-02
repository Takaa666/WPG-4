using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepMandala : MonoBehaviour
{
     public AudioSource stepAudioSource;

    void Update()
    {
        // Cek jika pemain menekan tombol W, A, S, atau D
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // Jika audiosource tidak sedang bermain, mainkan suara langkah
            if (!stepAudioSource.isPlaying)
            {
                stepAudioSource.Play();
            }
        }
        else
        {
            // Hentikan suara jika tidak ada input
            if (stepAudioSource.isPlaying)
            {
                stepAudioSource.Stop();
            }
        }
    }
}
