using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/new quest")]
public class TestQuestSO : ScriptableObject
{
    public string title;
    public string description;
    public bool isActive;
    public Player player;

    public bool inProgress;
    public bool isCompleted;
    public GameObject book;

}
