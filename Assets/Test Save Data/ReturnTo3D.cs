using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReturnTo3D : MonoBehaviour
{
    public GameObject trigger;
    public static bool triggerDestroy = false;
    public static bool questFotoAktif = false;
    public GameObject pieceParent;


    private void Update()
    {
        if (pieceParent.transform.childCount == 0)
        {
            OnMiniGameComplete();
        }
        LoadAndSetPlayerPosition();
        
    }

    public static void OnMiniGameComplete()
    {
        SceneChanger changer = FindObjectOfType<SceneChanger>();
        if (changer != null)
        {
            StartMiniGame.miniGame = false;
            triggerDestroy = true;
            questFotoAktif = true;
            TotalItem.totalItem = 0;
            changer.ChangeScene("BismillahFInal");
            //trigger.SetActive(false);
        }
    }

    private void LoadAndSetPlayerPosition()
    {
        // Load data dari exitgamedata.json
        SceneData loadedData = LoadSceneDataFromFile("exitgamedata.json");

        if (loadedData != null)
        {
            // Mencari data Player dari SceneData
            GameObjectData playerData = loadedData.gameObjects.Find(data => data.name == "Player");
            if (playerData != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    // Set posisi, rotasi, dan scale Player sesuai data yang disimpan
                    player.transform.position = playerData.position;
                    player.transform.rotation = playerData.rotation;
                    player.transform.localScale = playerData.scale;

                    Debug.Log("Player position, rotation, and scale have been set from saved data.");
                }
            }
        }
    }

    // Fungsi untuk load data dari file exitgamedata.json
    private SceneData LoadSceneDataFromFile(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log($"Scene data loaded from {path}");
            return JsonUtility.FromJson<SceneData>(json);
        }
        else
        {
            Debug.LogWarning($"No scene data file found at {path}. Returning null.");
            return null;
        }
    }
}

