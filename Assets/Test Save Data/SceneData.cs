using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneData
{
    public List<GameObjectData> gameObjects = new List<GameObjectData>();
    public List<QuestProgressData> questProgress = new List<QuestProgressData>();
    public int totalItemCount;
    //public Vector3 playerPosition; // Add this to store the player's last position
}



[Serializable]
public class QuestProgressData
{
    public string questName;  // To identify the quest
    public bool inProgress;
    public bool hasCompleted;
}

[Serializable]
public class GameObjectData
{
    public string uniqueID;
    public string name;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public bool isDestroyed;
}
