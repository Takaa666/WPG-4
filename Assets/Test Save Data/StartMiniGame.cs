using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMiniGame : MonoBehaviour
{
    public static bool miniGame = false;
    public void OnInteract()
    {
        SceneChanger changer = FindObjectOfType<SceneChanger>();
        if (changer != null)
        {
            miniGame = true; // Mark mini-game as active
            changer.ChangeScene("SampleScene 1"); // Load mini-game scene
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneStateManager.Instance.SaveContinue();
            OnInteract();
           // Destroy(gameObject);
            //TotalItem.totalItem = 0;
        }
    }
}

