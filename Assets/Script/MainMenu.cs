using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    public Button NewGameButton;
    public Button ExitButton;
    public Button OptionButton;

    [Header("Option Menu")]
    public GameObject optionMenu;
    public Button ExitButtonOnOptionPanel;
    public Button GraphicButton;
    public Button AudioButton;

    [Header("Graphic Panel")]
    public GameObject graphicPanel;
    public Button LowButton;
    public Button MediumButton;
    public Button HighButton;
    public Button BackButtonOnGraphicPanel;

    [Header("Audio Panel")]
    public GameObject AudioPanel;
    public Button BackButtonOnAudioPanel;
    public Slider musicSlider;
    public Slider sfxSlider;

    public Image panel;
    public Image panelStudio;
    public Image panelLogoGame;
    public Image panelHitam;
    private void Start()
    {
        // Hubungkan tombol seperti biasa
        NewGameButton.onClick.AddListener(NewGame);
        ExitButton.onClick.AddListener(ExitGame);
        OptionButton.onClick.AddListener(Option);

        ExitButtonOnOptionPanel.onClick.AddListener(() =>
        {
            optionMenu.SetActive(false);
        });

        GraphicButton.onClick.AddListener(ShowGraphicPanel);
        AudioButton.onClick.AddListener(ShowAudioPanel);

        LowButton.onClick.AddListener(LowQuality);
        MediumButton.onClick.AddListener(MediumQuality);
        HighButton.onClick.AddListener(HighQuality);
        BackButtonOnGraphicPanel.onClick.AddListener(ShowOptionPanel);
        BackButtonOnAudioPanel.onClick.AddListener(ShowOptionPanel);

        optionMenu.SetActive(false);

        // Matikan semua panel dulu
        //panel.enabled = false;
        panelStudio.enabled = false;
        panelLogoGame.enabled = false;

        // Mulai intro sequence
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        // Step 1: Panel
        panel.enabled = true;
        UIAutoAnimation panelAnim = panel.GetComponent<UIAutoAnimation>();
        panelAnim.EntranceAnimation();
        yield return new WaitForSeconds(2f);
        panelAnim.ExitAnimation();
        //yield return new WaitForSeconds(2f);

        // Step 2: panelStudio
        panel.enabled = false;
        panelStudio.enabled = true;
        UIAutoAnimation studioAnim = panelStudio.GetComponent<UIAutoAnimation>();
        studioAnim.EntranceAnimation();
        yield return new WaitForSeconds(2f);
        studioAnim.ExitAnimation();
        //yield return new WaitForSeconds(2f);

        // Step 3: Panel lagi
        panelStudio.enabled = false;
        panel.enabled = true;
        panelAnim.EntranceAnimation();
        yield return new WaitForSeconds(2f);
        panelAnim.ExitAnimation();
        //yield return new WaitForSeconds(2f);

        // Step 4: panelLogoGame
        panel.enabled = false;
        panelLogoGame.enabled = true;
        UIAutoAnimation logoAnim = panelLogoGame.GetComponent<UIAutoAnimation>();
        logoAnim.EntranceAnimation();
        yield return new WaitForSeconds(2f);
        logoAnim.ExitAnimation();
        yield return new WaitForSeconds(2f);
        panelLogoGame.enabled = false;
        panelHitam.enabled = false;
        // Optional: di sini bisa lanjutkan ke menu utama atau aktifkan tombol
        // misalnya:
        // ShowMainMenu();
    }


    public void NewGame()
    {
        panel.enabled = true;
        UIAutoAnimation anim = panel.GetComponent<UIAutoAnimation>();
        anim.EntranceAnimation();

        StartCoroutine(StartNewGameWithDelay());
    }

    private IEnumerator StartNewGameWithDelay()
    {
        // Aktifkan animasi panel

        // Pastikan panel aktif (jika misalnya panel awalnya nonaktif)
        //panel.gameObject.SetActive(true);

        // Tunggu 3 detik
        yield return new WaitForSeconds(3f);

        // Pindah scene
        SceneManager.LoadScene("Cutscene-Opening 1");

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Option()
    {
        optionMenu.SetActive(true);
    }

    public void ShowAudioPanel()
    {
        AudioPanel.SetActive(true);
    }

    public void ShowGraphicPanel()
    {
        graphicPanel.SetActive(true);
    }

    public void ShowOptionPanel()
    {
        optionMenu.SetActive(true);
        graphicPanel.SetActive(false);
        AudioPanel.SetActive(false);
    }

    public void LowQuality()
    {
        QualitySettings.SetQualityLevel(0);
    }

    public void MediumQuality()
    {
        QualitySettings.SetQualityLevel(2);
    }

    public void HighQuality()
    {
        QualitySettings.SetQualityLevel(5);
    }
}
