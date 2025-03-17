using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject mainMenu;

    public Button NewGameButton;
    public Button LoadGameButton;
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

    public void Start()
    {
        NewGameButton.onClick.AddListener(NewGame);
        LoadGameButton.onClick.AddListener(LoadGame);
        ExitButton.onClick.AddListener(ExitGame);
        OptionButton.onClick.AddListener(Option);

        ExitButtonOnOptionPanel.onClick.AddListener(() =>
        {
            mainMenu.SetActive(true);
            optionMenu.SetActive(false);
        });

        GraphicButton.onClick.AddListener(ShowGraphicPanel);
        AudioButton.onClick.AddListener(ShowAudioPanel);

        LowButton.onClick.AddListener(LowQuality);
        MediumButton.onClick.AddListener(MediumQuality);
        HighButton.onClick.AddListener(HighQuality);
        BackButtonOnGraphicPanel.onClick.AddListener(ShowOptionPanel);

        BackButtonOnAudioPanel.onClick.AddListener(ShowOptionPanel);


        mainMenu.SetActive(true);
        optionMenu.SetActive(false);



    }

    public void NewGame()
    {
        SceneManager.LoadScene("Main Coba");
    }

    public void LoadGame() { }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Option()
    {
        mainMenu.SetActive(false);
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
