using Fungus;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [Header("Quest system base objects")]
    [SerializeField]
    [Tooltip("The quests that can populate the questlog")]
    QuestSO[] questlog;
    [SerializeField]
    [Tooltip("Quest class that will be used to track objective and progress for future save system")]
    public QuestData[] questSaveable;

    [Header("UI Components")]
    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private TextMeshProUGUI characterNameText;  // Reference to the TextMeshProUGUI for the character name
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    [Tooltip("The quests log object that pop up with all quest")]
    GameObject questLogParent;

    [Header("Fungus Components")]
    [SerializeField] private Flowchart flowchart;

    public static QuestLog instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        questSaveable = new QuestData[questlog.Length];

        for (int i = 0; i < questlog.Length; i++)
        {
            questSaveable[i] = new QuestData();
            questSaveable[i].quest = questlog[i];
            questSaveable[i].UpdateQuestData();
            Debug.Log("Quest initialized: " + questSaveable[i].quest.questName + ", Target Object: " + questSaveable[i].quest.targetObject.name);

            if (questSaveable[i].inProgress)
            {
                questSaveable[i].quest.StartQuest();
            }
            else
            {
                questSaveable[i].quest.SetHaloEffect(false);
            }

            // Ensure we have enough children in questLogParent
            if (i < questLogParent.transform.childCount)
            {
                Transform questLogUI = questLogParent.transform.GetChild(i);
                if (questLogUI != null)
                {
                    questSaveable[i].questlogColor = questLogUI.GetComponentInChildren<Text>();
                    questSaveable[i].completeUI = questSaveable[i].questlogColor.GetComponentInChildren<Image>();
                }

            }
            else
            {
                Debug.LogError("No child found at index " + i + " in QuestLogParent.");
            }

            UpdateQuestColor(i);
        }
        if (!questSaveable[0].inProgress && !questSaveable[0].HasCompleted)
        {
            StartQuest(questlog[0]);
        }        
        ShowInProgressQuestDialog();

    }

    private void ShowInProgressQuestDialog()
    {
        for (int i = 0; i < questSaveable.Length; i++)
        {
            if (questSaveable[i].inProgress)
            {
                //ShowDialog(questSaveable[i].quest.pustakawan + "\n" + questSaveable[i].quest.dialog);
                break;
            }
        }
    }

    public void UpdateQuestColor(int i)
    {

        if (!questSaveable[i].inProgress && !questSaveable[i].HasCompleted)
        {
            questSaveable[i].questlogColor.enabled = false;
            questSaveable[i].completeUI.enabled = false;
        }
        else
        {
            questSaveable[i].questlogColor.enabled = true;

            if (questSaveable[i].HasCompleted)
            {
                questSaveable[i].completeUI.enabled = true;
                if (questSaveable[i].completeUI.GetComponent<UIAutoAnimation>())
                {
                    questSaveable[i].completeUI.GetComponent<UIAutoAnimation>().EntranceAnimation();
                }
            }
        }
    }

    
    
    public void StartQuest(QuestSO quest)
    {
        for (int i = 0; i < questlog.Length; i++)
        {
            if (questSaveable[i].quest == quest)
            {
                questSaveable[i].inProgress = true;
                UpdateQuestColor(i);

                if (questSaveable[i].inProgress && questSaveable[i].quest.targetObject != null )
                {
                    questSaveable[i].quest.StartQuest();
                }
                TriggerFungusDialogue(quest);
                /*Dialogue dialogueComponent = dialogBox.GetComponent<Dialogue>();
                if (dialogueComponent != null)
                {
                    quest.StartQuestDialogue(dialogueComponent);
                }*/
                break;
            }
        }
    }

    public void CheckQuestProgress()
    {
        for (int i = 0; i < questSaveable.Length; i++)
        {
            if (questSaveable[i].inProgress && questSaveable[i].quest.targetObject == null)
            {
                CompleteQuest(questSaveable[i].quest);
                if (i < questSaveable.Length - 1)
                {
                    StartQuest(questSaveable[i + 1].quest);
                }
            }
        }
    }

    public void CheckTargetObjectDestruction(GameObject destroyedObject)
    {
        var uniqueID = destroyedObject.GetComponent<UniqueID>().uniqueID;
        Debug.Log("Checking destruction for: " + destroyedObject.name + ", ID: " + uniqueID);

        for (int i = 0; i < questlog.Length; i++)
        {
            
            if (questSaveable[i].inProgress && questSaveable[i].quest.targetObjectID == uniqueID)
            {
                Debug.Log("Target object found in quest: " + questSaveable[i].quest.questName);
                CompleteQuest(questSaveable[i].quest);
                break;
            }
            else
            {
                Debug.Log("No matching target object or quest not in progress for quest: " + questSaveable[i].quest.questName);
            }
        }
    }

    private void TriggerFungusDialogue(QuestSO quest)
    {
        
        string blockName = quest.questName + "Dialogue";
        Block targetBlock = flowchart.FindBlock(blockName);
        if (targetBlock != null)
        {
            flowchart.ExecuteBlock(targetBlock);
        }
        else
        {
            Debug.LogWarning("No block found for quest: " + quest.questName);
        }
        
    }


    public void CompleteQuest(QuestSO quest)
    {
        for (int i = 0; i < questlog.Length; i++)
        {
            if (questSaveable[i].quest == quest)
            {
                Debug.Log("Completing quest: " + quest.questName);
                
                questSaveable[i].OnCompleted();
                Debug.Log("Quest inProgress: " + questSaveable[i].inProgress);
                Debug.Log("Quest HasCompleted: " + questSaveable[i].HasCompleted);
                UpdateQuestColor(i);
                questSaveable[i].quest.SetHaloEffect(false);


                Debug.Log("Quest " + quest.questName + " completed.");
                SceneStateManager.Instance.SaveContinue();
                Debug.Log("Game data saved to exitgamedata.json after completing a quest.");
                // Start the next quest if available
                if (i + 1 < questlog.Length)
                {
                    StartQuest(questlog[i + 1]);
                    break;
                }
            }
        }
    }

    public bool CanLoot(GameObject targetObject)
    {
        var uniqueID = targetObject.GetComponent<UniqueID>().uniqueID;
        for (int i = 0; i < questlog.Length; i++)
        {
            if (questSaveable[i].inProgress && !questSaveable[i].HasCompleted && questSaveable[i].quest.targetObjectID == uniqueID)
            {
                return true;
            }
        }
        return false;
    }

    
        private void OnApplicationQuit()
    {
        foreach (QuestSO quest in questlog)
        {
            quest.ResetTargetObject();
        }
    }

        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < questSaveable.Length; i++)
        {
            if (questSaveable[i].inProgress)
            {
                questSaveable[i].quest.SetHaloEffect(true);
            }
            else if (questSaveable[i].HasCompleted || !questSaveable[i].inProgress)
            {
                questSaveable[i].quest.SetHaloEffect(false);
            }
        }
        CheckQuestProgress();
    }
}
