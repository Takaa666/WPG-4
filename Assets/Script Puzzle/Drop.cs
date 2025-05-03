using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public int ID;

    public void OnDrop(PointerEventData eventData)
    {
        var piece = eventData.pointerDrag.GetComponent<PuzzleAnak>();
        if (piece != null)
        {
            piece.SetDropZone(this);
        }
    }
}
