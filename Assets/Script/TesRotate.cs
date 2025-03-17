using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesRotate : MonoBehaviour
{
    public float rotationSpeed = 300f; // Kecepatan rotasi (derajat per detik)
    private RectTransform imageTransform;
    private bool isRotating = false;
    private float targetRotation; // Target rotasi berikutnya

    void Start()
    {
        imageTransform = GetComponent<RectTransform>();
        targetRotation = imageTransform.eulerAngles.z; // Set awal rotasi
    }

    public void RotateImage()
    {
        if (!isRotating)
        {
            targetRotation += 90f; // Tambah 90 derajat setiap klik
            StartCoroutine(RotateSmoothly(targetRotation));
        }
    }

    private IEnumerator RotateSmoothly(float targetAngle)
    {
        isRotating = true;
        float startAngle = imageTransform.eulerAngles.z;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * (rotationSpeed / 90f); // Normalisasi waktu agar smooth
            float newAngle = Mathf.LerpAngle(startAngle, targetAngle, elapsedTime);
            imageTransform.rotation = Quaternion.Euler(0, 0, newAngle);
            yield return null;
        }

        imageTransform.rotation = Quaternion.Euler(0, 0, targetAngle); // Pastikan tepat di target
        isRotating = false;
    }
}
