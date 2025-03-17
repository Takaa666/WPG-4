using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string title;
    public string description;
    public bool isActive = true;
    public Player player;


    public bool secondQuest;
    public bool thirdQuest;
    public QuestGoal goal;
    public bool isCompleted = false;

    public bool questFoto;

    public void Complete()
    {
        isActive = false;
        Debug.Log(title + " was completed");
        secondQuest = true;
        thirdQuest = false;
    }

    public void SecondQuestComplete()
    {
        secondQuest = false;
        thirdQuest = true;
    }

    public void ThirdQuestComplete()
    {
        thirdQuest = false;
        isCompleted = true; // Set the flag to true when the third quest is completed
    }

    public void QuestFotoCompleted()
    {
        questFoto = false;
    }

}
