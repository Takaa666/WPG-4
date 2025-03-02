using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Create new quest")]
public class QuestSO : ScriptableObject
{
    public UnityEvent onQuestStarted = new UnityEvent();
    [Header("Quest Stats")]
    public string questName;
    public string questDescription;
    public string targetObjectID;

    //public Dialogue.CharacterDialogue[] characterDialogues;
    public float dialogueSpeed;


    [Header("Quest Objectives")]
    public string[] objectiveTask;

    public QuestSO[] requiredQuests;

    [Header("Target Object")]
    public GameObject targetObject;

    private GameObject initialTargetObject;
    private GameObject initialTargetHalo;
    public GameObject haloEffect;

    private void OnEnable()
    {
        initialTargetObject = targetObject;
        initialTargetHalo = haloEffect;
    }

    public void ResetTargetObject()
    {
        targetObject = initialTargetObject;
        haloEffect = initialTargetHalo;
    }

    public void SetHaloEffect(bool isActive)
    {
        if (haloEffect != null)
        {
            haloEffect.SetActive(isActive);
        }
    }

    public void StartQuest()
    {
        onQuestStarted.Invoke();
    }

    /*public void StartQuestDialogue(Dialogue dialogueComponent)
    {
        if (dialogueComponent != null)
        {
            dialogueComponent.gameObject.SetActive(true);
            dialogueComponent.InitializeDialogue(characterDialogues, dialogueSpeed);
        }
    }*/
}
