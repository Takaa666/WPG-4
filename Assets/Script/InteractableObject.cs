using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public GameObject pressInteract;
    private bool canInteract = false; // Cek apakah player dalam jarak interaksi
    private bool hasInteracted = false; // Cek apakah sudah berinteraksi

    void Start()
    {
        if (pressInteract != null)
            pressInteract.SetActive(false);

       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pressInteract != null)
                pressInteract.SetActive(true);

            canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;

            

            if (pressInteract != null)
                pressInteract.SetActive(false);
        }
    }

    void Update()
    {
        if (canInteract && !hasInteracted && Input.GetKeyDown(KeyCode.E))
        {
            hasInteracted = true;

            

            if (pressInteract != null)
            {
                pressInteract.SetActive(false);
            }

            Destroy(gameObject);
        }
    }

    
}
