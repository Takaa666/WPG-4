using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public GameObject pressInteract;
    public GameObject interactionUI; // UI yang muncul saat interaksi
    private bool canInteract = false; // Cek apakah player dalam jarak interaksi
    private bool hasInteracted = false; // Cek apakah sudah berinteraksi

    void Start()
    {
        if (pressInteract != null)
            pressInteract.SetActive(false);

        if (interactionUI != null)
            interactionUI.SetActive(false); // Sembunyikan UI saat mulai
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

            if (interactionUI != null)
                interactionUI.SetActive(false); // Sembunyikan UI saat keluar trigger

            if (pressInteract != null)
                pressInteract.SetActive(false);
        }
    }

    void Update()
    {
        if (canInteract && !hasInteracted && Input.GetKeyDown(KeyCode.E))
        {
            hasInteracted = true;

            if (interactionUI != null)
            {
                interactionUI.SetActive(true);
            }

            if (pressInteract != null)
            {
                pressInteract.SetActive(false);
            }

            // Mulai proses penghancuran setelah 3 detik
            Invoke(nameof(DestroyInteractionObjects), 3f);
        }
    }

    void DestroyInteractionObjects()
    {
        if (interactionUI != null)
        {
            Destroy(interactionUI);
        }
        Destroy(gameObject); // Hancurkan object ini (yang ada script-nya)
    }
}
