using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    public GameObject canvasQuest;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        if (canvasQuest == null)
        {
            canvasQuest = GameObject.Find("QuestList"); // Replace with the actual name of your Canvas GameObject
            if (canvasQuest == null)
            {
                Debug.LogError("canvasQuest not found in the scene.");
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (canvasQuest != null)
        {
            if (StartMiniGame.miniGame == true)
            {
                canvasQuest.SetActive(false);
            }
            if (ReturnTo3D.triggerDestroy == true)
            {
                canvasQuest.SetActive(true);
            }
        }
        else
        {
            //Debug.LogWarning("canvasQuest is null. Ensure it's assigned and not destroyed.");
        }
    }
}
