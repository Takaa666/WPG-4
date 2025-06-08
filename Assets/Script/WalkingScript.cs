using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingScript : MonoBehaviour
{
    public List<AudioClip> walkSounds;
    public List<AudioClip> runningSound;
    public List<AudioClip> pickUpSound;

    public AudioSource audioSource;

    public int pos;
    public int p;
    public void playSound()
    {
        pos = (int)Mathf.Floor(Random.Range(0, walkSounds.Count));
        audioSource.PlayOneShot(walkSounds[pos]);
    }

    public void RunningSound()
    {
        p = (int)Mathf.Floor(Random.Range(0, runningSound.Count));
        audioSource.PlayOneShot(runningSound[p]);
    }

    public void PickUpSound()
    {
        p = (int)Mathf.Floor(Random.Range(0, pickUpSound.Count));
        audioSource.PlayOneShot(pickUpSound[p]);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
