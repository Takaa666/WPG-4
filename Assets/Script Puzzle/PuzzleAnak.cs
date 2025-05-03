using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleAnak : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int ID;
    public GameObject completeObject;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform parentAfterDrag;
    private Drop currentDropZone;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
        completeObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root); // Move to top of hierarchy to avoid clipping
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (currentDropZone != null && ID == currentDropZone.ID)
        {
            Destroy(gameObject);
            Destroy(gameObject.GetComponent<Drop>());
            completeObject.SetActive(true);
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
            transform.SetParent(parentAfterDrag);
        }
    }

    public void SetDropZone(Drop dropZone)
    {
        currentDropZone = dropZone;
    }

    public void ClearDropZone()
    {
        currentDropZone = null;
    }
}
