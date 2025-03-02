using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void NewGame()
    {
        // Reset data sebelum memulai game baru
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("SampleScene");
    }

    public void ContinueGame()
    {
        // Cek apakah ada data yang tersimpan
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            // Load scene yang disimpan terakhir
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene);
        }
        else
        {
            Debug.Log("No saved game found!");
        }
    }
}
