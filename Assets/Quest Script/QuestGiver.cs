using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public Player player;
    public SceneStateManager sceneStateManager;

    public GameObject secondItem;
    public GameObject thirdItem;

    public GameObject Foto;

    public Text taskNotif;
    public Text taskNotif2;
    public Text taskNotif3;
    public Text taskNotif4;
    public Text taskNotif5;

    public GameObject miniGameTrigger;
    // Start is called before the first frame update
    void Start()
    {
        miniGameTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quest.isCompleted) // Check if all quests are completed
        {
            miniGameTrigger.SetActive(true);
            taskNotif3.enabled = false;
            taskNotif4.enabled = true;
            Destroy(gameObject); // Destroy the Quest Giver
            return;
        }

        if (quest.isActive)
        {
            secondItem.GetComponent<PickToInventory>().enabled = false;
            thirdItem.GetComponent<PickToInventory>().enabled=false;
            taskNotif.enabled = true;
            taskNotif4.enabled = false;
            //player.quest = quest;
        }
        else
        {
            taskNotif.enabled = false;
        }

        if (quest.secondQuest)
        {
            secondItem.GetComponent<PickToInventory>().enabled = true;
            taskNotif2.enabled = true;
            //player.quest = quest;

        }
        else
        {
            taskNotif2.enabled = false;
        }

        if (quest.thirdQuest)
        {
            thirdItem.GetComponent<PickToInventory>().enabled = true;
            taskNotif3.enabled = true;
            //player.quest = quest;
        }
        else
        {
            taskNotif3.enabled = false;
        }

        if(ReturnTo3D.questFotoAktif == true)
        {
            taskNotif4.enabled = false;
            quest.questFoto = true;
            taskNotif5.enabled = true;
            //player.quest = quest;
        }

        if (Foto.IsDestroyed())
        {
            taskNotif5.enabled = false;
            //Destroy(gameObject); 

        }
    }

    private void OnApplicationQuit()
    {
        //sceneStateManager.SaveSceneData();
    }
}

