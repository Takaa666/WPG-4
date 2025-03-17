using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickToInventory : MonoBehaviour
{
    //[SerializeField] private LootableItem lootableItem;
    public QuestSO questSO;
    public GameObject halo;
    // Start is called before the first frame update
    private void Awake()
    {
        questSO.onQuestStarted.AddListener(OnQuestStarted);
    }

    private void OnQuestStarted()
    {
        halo.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
