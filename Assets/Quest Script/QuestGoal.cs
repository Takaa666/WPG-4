using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public int requiredAmount;
    public int secondRequiredAmount;
    public int thirdRequiredAmount;
    public int fourthRequiredAmount;

    public bool IsReached()
    {
        return (TotalItem.totalItem >= requiredAmount);
    }

    public bool TargetSecondQuest()
    {
        return(TotalItem.totalItem >= secondRequiredAmount);
    }

    public bool TargetThirdQuest()
    {
        return (TotalItem.totalItem >= thirdRequiredAmount);
    }

    public bool TargetFourthQuest()
    {
        return (TotalItem.totalItem >= fourthRequiredAmount);
    }

    public void Book()
    {
        PlayerMovement.itemScore();
    }
}
