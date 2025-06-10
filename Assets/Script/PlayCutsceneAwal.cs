using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayCutsceneAwal : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public Image panelScreen;
    public GameObject panel;
    public string nextSceneName = "WPG4_Jeje Update Map"; // Ganti dengan nama scene berikutnya

    private UIAutoAnimation anim;

    void Start()
    {
        anim = panelScreen.GetComponent<UIAutoAnimation>();
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        // 1. Jeda 1 detik di awal
        yield return new WaitForSeconds(1f);

        // 2. ExitAnimation dan nonaktifkan panelScreen & panel
        anim.ExitAnimation();
        yield return new WaitForSeconds(0.5f); // delay opsional untuk animasi keluar, sesuaikan

        panelScreen.enabled = false;
        panel.SetActive(false);

        // 3. Play playable director
        playableDirector.Play();

        // 4. Tunggu sampai playable director selesai
        yield return new WaitUntil(() => playableDirector.state != PlayState.Playing);

        // 5. Aktifkan panelScreen & panel, lakukan EntranceAnimation
        panelScreen.enabled = true;
        panel.SetActive(true);
        anim.EntranceAnimation();

        // 6. Jeda 1 detik
        yield return new WaitForSeconds(1f);

        // 7. Ganti scene
        SceneManager.LoadScene(nextSceneName);
    }
}
