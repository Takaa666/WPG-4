using UnityEngine;
using UnityEngine.EventSystems;

public class RotateLever : MonoBehaviour, IDragHandler
{
    public RectTransform leverTransform;  // Lever yang diputar
    public RectTransform linkedImages;  // UI Image lain yang ikut berotasi

    public void OnDrag(PointerEventData eventData)
    {
        // Hitung sudut rotasi berdasarkan posisi mouse
        Vector2 direction = eventData.position - (Vector2)leverTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Terapkan rotasi ke lever
        leverTransform.rotation = Quaternion.Euler(0, 0, angle);

        // Terapkan rotasi ke semua UI Image yang terhubung
        
        linkedImages.rotation = Quaternion.Euler(0, 0, angle);
        
    }
}
