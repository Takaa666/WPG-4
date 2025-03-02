using UnityEngine;
using UnityEngine.UI;

public class PickUpInteractable : MonoBehaviour
{
    public GameObject interactionUI; // UI untuk ditampilkan ketika berinteraksi
    private bool isPlayerNear = false;
    private bool isUIActive = false;

    void Start()
    {
        // Pastikan UI dinonaktifkan di awal
        if (interactionUI != null)
        {
            interactionUI.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactionUI != null && isUIActive)
            {
                interactionUI.SetActive(false); // Sembunyikan UI ketika pemain keluar dari collider
                isUIActive = false;
            }
        }
    }

    void Update()
    {
        // Jika pemain berada di dekat item dan menekan tombol 'E'
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (interactionUI != null)
            {
                // Toggle untuk membuka atau menutup UI
                isUIActive = !isUIActive;
                interactionUI.SetActive(isUIActive);
            }
        }
    }
}
