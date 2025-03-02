using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager Instance;
    public SceneData sceneData = new SceneData();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //SaveSceneData();
            DontDestroyOnLoad(gameObject);
            //LoadSceneData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SaveSceneData();
    }

    public void SaveSceneData()
    {
        sceneData.gameObjects.Clear();
        sceneData.questProgress.Clear();


        foreach (var obj in FindObjectsOfType<YourObjectScript>())
        {
            GameObjectData data = new GameObjectData();
            data.uniqueID = obj.uniqueID;
            data.name = obj.gameObject.name;
            data.position = obj.transform.position;
            data.rotation = obj.transform.rotation;
            data.scale = obj.transform.localScale;
            data.isDestroyed = false;

            sceneData.gameObjects.Add(data);
        }

        foreach (var questData in QuestLog.instance.questSaveable)
        {
            QuestProgressData questProgress = new QuestProgressData();
            questProgress.questName = questData.quest.questName;
            questProgress.inProgress = questData.inProgress;
            questProgress.hasCompleted = questData.HasCompleted;

            sceneData.questProgress.Add(questProgress);
        }

        /*GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            var playerScript = player.GetComponent<YourObjectScript>();
            if (playerScript != null)
            {
                GameObjectData playerData = new GameObjectData
                {
                    uniqueID = playerScript.uniqueID,
                    name = player.name,
                    position = player.transform.position,
                    rotation = player.transform.rotation,
                    scale = player.transform.localScale,
                    isDestroyed = false
                };

                // Add player data to the sceneData
                sceneData.gameObjects.Add(playerData);
            }
        }*/
        SaveToPersistentStorage(sceneData);

        Debug.Log("Scene data saved.");
    }

    public void LoadSceneData()
    {
        sceneData = LoadFromPersistentStorage();
        ApplyLoadedData(sceneData);

        foreach (var questProgress in sceneData.questProgress)
        {
            foreach (var questData in QuestLog.instance.questSaveable)
            {
                if (questData.quest.questName == questProgress.questName)
                {
                    questData.inProgress = questProgress.inProgress;
                    questData.HasCompleted = questProgress.hasCompleted;
                    QuestLog.instance.UpdateQuestColor(Array.IndexOf(QuestLog.instance.questSaveable, questData)); // Update UI
                    break;
                }
            }
        }

    }

    private void ApplyLoadedData(SceneData loadedData)
    {
        List<string> uniqueIDsInScene = new List<string>();
        foreach (var obj in FindObjectsOfType<YourObjectScript>())
        {
            uniqueIDsInScene.Add(obj.uniqueID);
        }

        foreach (var data in loadedData.gameObjects)
        {
            if (data.isDestroyed)
            {
                continue;
            }

            var obj = FindObjectByUniqueID(data.uniqueID);
            if (obj != null)
            {
                obj.transform.position = data.position;
                obj.transform.rotation = data.rotation;
                obj.transform.localScale = data.scale;
            }
        }

        foreach (var uniqueID in uniqueIDsInScene)
        {
            if (!loadedData.gameObjects.Exists(data => data.uniqueID == uniqueID))
            {
                var obj = FindObjectByUniqueID(uniqueID);
                if (obj != null)
                {
                    Destroy(obj.gameObject);
                }
            }
        }

        Debug.Log("Scene data loaded.");
    }

    public void ClearSceneData()
    {
        sceneData = new SceneData(); // Reset the scene data
        SaveToPersistentStorage(sceneData); // Save the empty scene data

        Debug.Log("Scene data cleared.");
    }

    private YourObjectScript FindObjectByUniqueID(string uniqueID)
    {
        foreach (var obj in FindObjectsOfType<YourObjectScript>())
        {
            if (obj.uniqueID == uniqueID)
            {
                return obj;
            }
        }
        return null;
    }

    private void SaveToPersistentStorage(SceneData sceneData)
    {
        string json = JsonUtility.ToJson(sceneData);
        string path = Application.persistentDataPath + "/scenedata.json";
        File.WriteAllText(path, json);

        Debug.Log($"Scene data saved to {path}");
    }

    private SceneData LoadFromPersistentStorage()
    {
        string path = Application.persistentDataPath + "/scenedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log($"Scene data loaded from {path}");
            return JsonUtility.FromJson<SceneData>(json);
        }
        else
        {
            Debug.LogWarning($"No scene data file found at {path}. Loading default empty SceneData.");
            return new SceneData();
        }
    }

    // New Methods for saving and loading alternate save data
    public void SaveContinue()
    {
        sceneData.gameObjects.Clear();
        sceneData.questProgress.Clear();

        foreach (var obj in FindObjectsOfType<YourObjectScript>())
        {
            GameObjectData data = new GameObjectData();
            data.uniqueID = obj.uniqueID;
            data.name = obj.gameObject.name;
            data.position = obj.transform.position;
            data.rotation = obj.transform.rotation;
            data.scale = obj.transform.localScale;
            data.isDestroyed = false;

            sceneData.gameObjects.Add(data);
        }

        foreach (var questData in QuestLog.instance.questSaveable)
        {
            QuestProgressData questProgress = new QuestProgressData();
            questProgress.questName = questData.quest.questName;
            questProgress.inProgress = questData.inProgress;
            questProgress.hasCompleted = questData.HasCompleted;

            sceneData.questProgress.Add(questProgress);
        }

        sceneData.totalItemCount = TotalItem.totalItem;
        GameObject player = GameObject.FindWithTag("Player");

        

        SaveToContinue(sceneData, "exitgamedata.json");
        Debug.Log("Exit game data saved.");
    }


    public void LoadContinue()
    {
        sceneData = LoadFromContinue("exitgamedata.json");

        if (sceneData == null)
        {
            Debug.LogWarning("No exit game data found. Starting a new game.");
            sceneData = new SceneData();
            return;
        }

        ApplyLoadedData(sceneData);

        // Load Quest Progress Data
        foreach (var questProgress in sceneData.questProgress)
        {
            foreach (var questData in QuestLog.instance.questSaveable)
            {
                if (questData.quest.questName == questProgress.questName)
                {
                    questData.inProgress = questProgress.inProgress;
                    questData.HasCompleted = questProgress.hasCompleted;
                    QuestLog.instance.UpdateQuestColor(Array.IndexOf(QuestLog.instance.questSaveable, questData)); // Update UI
                    break;
                }
            }
        }
        
        TotalItem.totalItem = sceneData.totalItemCount;

        var playerData = sceneData.gameObjects.Find(data => data.uniqueID == "Player"); // Use the actual uniqueID for the player
        if (playerData != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = playerData.position;
                player.transform.rotation = playerData.rotation;
                player.transform.localScale = playerData.scale;
            }
        }

        Debug.Log("Exit game data loaded.");
    }


    private void SaveToContinue(SceneData sceneData, string fileName)
    {
        string json = JsonUtility.ToJson(sceneData);
        string path = Application.persistentDataPath + "/" + fileName;
        File.WriteAllText(path, json);

        Debug.Log($"Scene data saved to {path}");
    }

    private SceneData LoadFromContinue(string fileName)
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
            Debug.LogWarning($"No scene data file found at {path}. Loading default empty SceneData.");
            return null;
        }
    }


    private void OnApplicationQuit()
    {
        SaveContinue();
    }

    public void MarkObjectAsDestroyed(string uniqueID)
    {
        var objData = sceneData.gameObjects.Find(data => data.uniqueID == uniqueID);
        if (objData != null)
        {
            objData.isDestroyed = true;
        }
    }

    
}