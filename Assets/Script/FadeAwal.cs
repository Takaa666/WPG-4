using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeAwal : MonoBehaviour
{
    public Image panel;

    // Start is called before the first frame update
    void Start()
    {
        // Jalankan coroutine supaya ada delay
        StartCoroutine(StartFadeOutAfterDelay());
    }

    // Update tidak perlu dipakai
    void Update()
    {

    }

    IEnumerator StartFadeOutAfterDelay()
    {
        // Pastikan panel aktif (kalau sebelumnya disable)
        panel.gameObject.SetActive(true);

        // Tunggu 3 detik
        yield return new WaitForSeconds(3f);

        // Panggil ExitAnimation setelah 3 detik
        UIAutoAnimation anim = panel.GetComponent<UIAutoAnimation>();
        anim.ExitAnimation();
    }
}
