using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainWorld : MonoBehaviour
{
    public static bool New = false;

    void Awake()
    {
        New = true;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return SceneManager.LoadSceneAsync("BismillahFinal");
        yield return new WaitForEndOfFrame(); // Ensure the scene is fully loaded before proceeding

        if (SceneStateManager.Instance != null)
        {
            SceneStateManager.Instance.ClearSceneData();
            SceneStateManager.Instance.SaveSceneData();
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = new Vector3(-53.11f, -0.081f, 2.88f); // Set starting position
                SceneStateManager.Instance.SaveSceneData(); // Save this starting position
            }
        }
        else
        {
            Debug.LogError("SceneStateManager instance not found.");
        }
    }
}
