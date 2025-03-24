using UnityEngine;
using UnityEngine.EventSystems;

public class DragLever : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector3 originalPosition;
    public Transform snapPoint; // Tempat tujuan untuk tuas

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float distance = Vector3.Distance(rectTransform.position, snapPoint.position);

        if (distance < 50f) // Jarak ambang untuk snap
        {
            rectTransform.position = snapPoint.position;
        }
        else
        {
            rectTransform.position = originalPosition;
        }
    }
}
