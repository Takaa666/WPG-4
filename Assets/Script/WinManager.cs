using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Tambahkan ini untuk ganti scene

public class WinManager : MonoBehaviour
{
    public GameObject[] questObject;
    public GameObject panel;
    public Image panelUI;

    public string nextSceneName; // Nama scene tujuan

    private bool hasTriggeredWin = false;

    void Start()
    {
        panel.SetActive(false);
        panelUI.enabled = false;
    }

    void Update()
    {
        if (hasTriggeredWin)
            return;

        bool allDestroyed = true;
        foreach (var obj in questObject)
        {
            if (obj != null)
            {
                allDestroyed = false;
                break;
            }
        }

        if (allDestroyed)
        {
            hasTriggeredWin = true;
            StartCoroutine(HandleWin());
        }
    }

    IEnumerator HandleWin()
    {
        yield return new WaitForSeconds(3f); // Tunggu 3 detik

        panel.SetActive(true);
        panelUI.enabled = true;

        var panelAnim = panel.GetComponent<UIAutoAnimation>();
        if (panelAnim != null)
        {
            panelAnim.EntranceAnimation();
        }

        yield return new WaitForSeconds(2f); // Tunggu 2 detik setelah animasi

        // Pindah scene
        SceneManager.LoadScene("Cutscene-Temon-Terbaru");
    }
}
