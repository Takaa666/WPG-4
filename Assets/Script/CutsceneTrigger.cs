using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneTrigger : MonoBehaviour
{

    public GameObject cutsceneObject;
    public bool preRendered;
    public VideoPlayer preRenderedVideo;
    public string cutsceneTriggerName;
    public PlayerMovement playerMovementScript;
    public Camera playerCam;
    public float cutsceneTime;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovementScript.enabled = false;
            if (preRendered == false)
            {
                cutsceneObject.SetActive(true);
                playerCam.enabled = false;
                StartCoroutine(cutsceneRoutine());
            }
            if (preRendered == true)
            {
                cutsceneObject.SetActive(true);
                StartCoroutine(cutsceneRoutine());
                preRenderedVideo.Play();
            }
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    IEnumerator cutsceneRoutine()
    {
        yield return new WaitForSeconds(cutsceneTime);
        playerMovementScript.enabled = true;
        playerCam.enabled = true;
        if (preRendered == true)
        {
            preRenderedVideo.Stop();
            cutsceneObject.SetActive(false);
        }
    }
}


