using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteract : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject interactPromptUI;

    [Header("Detection Settings")]
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask interactLayer;

    private Transform playerCamera;

    void Start()
    {
        playerCamera = Camera.main.transform;

        if (interactPromptUI != null)
        {
            interactPromptUI.SetActive(false);
        }
    }

    void Update()
    {
        CheckForInteractableObject();
    }

    void CheckForInteractableObject()
    {
        Collider[] hits = Physics.OverlapSphere(playerCamera.position + playerCamera.forward * interactRange * 0.5f, interactRange * 0.5f, interactLayer);

        foreach (Collider hit in hits)
        {
            UniqueID targetID = hit.GetComponent<UniqueID>();

            if (targetID != null && QuestLog.instance != null)
            {
                if (QuestLog.instance.CanLoot(hit.gameObject))
                {
                    interactPromptUI.SetActive(true);
                    Debug.Log("Detected");
                    return;
                }
            }
        }

        interactPromptUI.SetActive(false);
    }
}
