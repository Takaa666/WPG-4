using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionMenu;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        // Nonaktifkan input pemain saat pause menu aktif
        if (isPaused)
        {
            Time.timeScale = 0f; // Memastikan game tidak berhenti
            // Nonaktifkan semua input pemain
            var playerInput = FindObjectOfType<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.enabled = false;
            }
        }
        else
        {
            Time.timeScale = 1f; // Memastikan game tidak berhenti
            // Aktifkan kembali input pemain
            var playerInput = FindObjectOfType<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.enabled = true;
            }
        }
    }

    public void OpenOptionMenu()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void CloseOptionMenu()
    {
        optionMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);

        // Aktifkan kembali input pemain
        Time.timeScale = 1f; // Memastikan game tidak berhenti
        var playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.enabled = true;
        }
    }
}
