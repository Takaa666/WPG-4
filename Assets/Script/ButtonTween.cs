using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI buttonText;
    private RawImage hoverImage;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        hoverImage = GetComponent<RawImage>();
        hoverImage.enabled = false;  // Initially hide the hover image
    }

    public void OnHover()
    {
        hoverImage.enabled = true;  // Show the hover image
        buttonText.DOColor(Color.white, 0.3f);  // Change button text color to white with a tween
    }

    public void Normal()
    {
        hoverImage.enabled = false;  // Hide the hover image
        buttonText.DOColor(Color.gray, 0.3f);  // Change button text color to gray with a tween
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Normal();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover();
    }
}
