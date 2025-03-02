using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        // Find the player object by tag or name
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    public void SavePlayerPosition(Vector3 position)
    {
        PlayerPrefs.SetFloat("PlayerX", position.x);
        PlayerPrefs.SetFloat("PlayerY", position.y);
        PlayerPrefs.SetFloat("PlayerZ", position.z);
        PlayerPrefs.Save();

        Debug.Log("Player position saved.");
    }

    public Vector3 LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerX", 0); // Default to (0, 0, 0) if not found
        float y = PlayerPrefs.GetFloat("PlayerY", 0);
        float z = PlayerPrefs.GetFloat("PlayerZ", 0);

        return new Vector3(x, y, z);
    }

    public void ApplyPlayerPosition(Vector3 position)
    {
        if (playerTransform != null)
        {
            playerTransform.position = position;
            Debug.Log("Player position loaded and applied.");
        }
    }
}
