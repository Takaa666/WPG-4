using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public GameObject interactionUI; // The game object (e.g., UI element) to show on interaction
    private bool canInteract = false; // Flag to check if player is in range

    void Start()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false); // Hide the UI object at the start
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true; // Player is within interaction range
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false; // Player is out of interaction range
            if (interactionUI != null)
            {
                interactionUI.SetActive(false); // Hide the UI object when player exits
            }
        }
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.Q)) // If player is in range and presses "Q"
        {
            if (interactionUI != null)
            {
                interactionUI.SetActive(true); // Show the UI object
            }
        }
    }
}
