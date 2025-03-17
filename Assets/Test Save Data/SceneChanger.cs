using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string previousSceneName;

    public void ChangeScene(string sceneName)
    {
        previousSceneName = SceneManager.GetActiveScene().name;

        // Save data only if leaving SampleScene
        if (previousSceneName == "BismillahFinal")
        {
            Debug.Log($"Saving data before leaving {previousSceneName}...");
            SceneStateManager.Instance.SaveContinue();
        }

        SceneManager.LoadScene(sceneName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BismillahFinal")
        {
            // If returning to SampleScene after a mini-game
            if (ReturnTo3D.questFotoAktif)
            {
                Debug.Log("Loading data from exitgamedata.json after returning from mini-game...");
                SceneStateManager.Instance.LoadContinue();
                ReturnTo3D.questFotoAktif = false; // Reset flag
            }
            // Handle normal scene load after new game or continue
            else if (ToMainWorld.New == true)
            {
                Debug.Log($"Loading data after starting a new game in {scene.name}...");
                SceneStateManager.Instance.LoadSceneData();
            }
            else if (MainMenuManager.Load == true)
            {
                Debug.Log($"Loading data after continuing game in {scene.name}...");
                SceneStateManager.Instance.LoadContinue();
            }

        }
    }
}