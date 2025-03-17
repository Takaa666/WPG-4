using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{

    public GameObject minigame;
    public PlayerMovement playerMovementScript;
    public Animator animator;
    public CinemachineFreeLook cm;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            playerMovementScript.enabled = false;
            animator.enabled = false;
            cm.enabled = false;
            minigame.SetActive(true);
        }
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
