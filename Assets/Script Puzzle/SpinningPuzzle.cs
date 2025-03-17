using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpinningPuzzle : MonoBehaviour
{
    public Image[] layers; // 0 = Inner, 1 = Middle, 2 = Outer
    private int selectedLayer = 0; // Layer yang sedang dipilih (default: Inner)
    public Button switchButton, rotateButton; // Tombol switch & rotate

    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    public float rotationSpeed = 5f; // Kecepatan rotasi smooth

    private bool isRotating = false; // Cek apakah sedang berotasi

    void Start()
    {
        switchButton.onClick.AddListener(SwitchLayer);
        rotateButton.onClick.AddListener(() => StartCoroutine(RotateLayer(90))); // Rotasi smooth ke kanan

        UpdateHighlight(); // Atur highlight pertama kali
    }

    void SwitchLayer()
    {
        if (isRotating) return; // Cegah input jika sedang berotasi

        selectedLayer = (selectedLayer + 1) % layers.Length; // Pindah ke layer berikutnya
        UpdateHighlight();
        Debug.Log("Selected Layer: " + selectedLayer);
    }

    IEnumerator RotateLayer(float angle)
    {
        if (isRotating) yield break; // Cegah input jika sedang berotasi

        isRotating = true; // Kunci input
        switchButton.interactable = false;
        rotateButton.interactable = false;

        float targetAngle = layers[selectedLayer].transform.eulerAngles.z + angle;
        float startAngle = layers[selectedLayer].transform.eulerAngles.z;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            float currentAngle = Mathf.Lerp(startAngle, targetAngle, t);
            layers[selectedLayer].transform.rotation = Quaternion.Euler(0, 0, currentAngle);
            yield return null;
        }

        // Aktifkan kembali tombol setelah rotasi selesai
        isRotating = false;
        switchButton.interactable = true;
        rotateButton.interactable = true;
    }

    void UpdateHighlight()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].color = (i == selectedLayer) ? highlightColor : normalColor; // Ubah warna layer terpilih
        }
    }
}
