using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestData
{
    public QuestSO quest;
    public Text questlogColor;
    public Image completeUI;
    [Space]

    [Header("Quest Progress")]
    public bool inProgress;
    public bool HasCompleted;
    [Space]

    [Header("Objective")]
    public string[] objectives;
    public int currentObjectiveIndex;

    

    public QuestSO GetQuest()
    {
        return quest;
    }

    public string ShowCurrentObjective()
    {
        return objectives[currentObjectiveIndex];
    }

    public void UpdateQuestData()
    {
        Debug.Log("Updating quest data for: " + quest.questName);
        objectives = new string[quest.objectiveTask.Length];
        objectives = quest.objectiveTask;
        Debug.Log("Objectives updated for quest: " + quest.questName);
    }


    public void OnCompleted()
    {
        Debug.Log("OnCompleted called for quest: " + quest.questName);
        quest.targetObject = null;
        if (quest.targetObject == null)
        {
            inProgress = false;
            HasCompleted = true;
            //quest.haloEffect.SetActive(false);
            Debug.Log("Quest completed: " + quest.questName);
        }
        else
        {
            Debug.Log("Target object is not null for quest: " + quest.questName);
        }
    }


}
