using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField]
    GameObject optionMenu, menuStartButtons, specificOptionMenu;
    public bool checkOptionMenu, checkSpecificOptionMenu;

    public GameObject pauseMenuPanel, backMainMenuPanel, exitPanel;

    #region VideoSettings
    [SerializeField]
    GameObject videoMenu, applyChangeVideo;
    [SerializeField]
    Slider brightnessSlider;
    [SerializeField]
    Toggle vSyncToggle, fullscreenToggle;
    [SerializeField]
    TMP_Dropdown resolutionDropDown, FPSDropDown;
    [SerializeField]
    PostProcessVolume postProcess;
    AutoExposure autoExposure = null;
    Bloom bloom = null;
    [SerializeField]
    bool confirmedVideoChanges;
    #endregion
    #region AudioSettings
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    Slider masterValue, musicValue, SFXValue;
    [SerializeField]
    GameObject audioMenu;
    #endregion
    #region GameSettings
    [SerializeField]
    GameObject gameMenu;
    [SerializeField]
    TMP_Dropdown languageDropDown;
    [SerializeField]
    Toggle vibrationToggle;
    public bool vibrationCheck;
    Gamepad gamepad;
    #endregion
    #region ControlsSettings
    [SerializeField]
    GameObject controlsMenu;
    #endregion
    #region HelpSettings
    [SerializeField]
    GameObject helpMenu;
    #endregion

    private void Awake()
    {
        optionMenu = GameObject.FindGameObjectsWithTag("MenuOptions")[0];
        if (GameObject.FindGameObjectWithTag("MenuStart")) menuStartButtons = GameObject.FindGameObjectWithTag("MenuStart");
        else menuStartButtons = GameObject.FindGameObjectWithTag("Empty");
        specificOptionMenu = GameObject.FindGameObjectsWithTag("MenuOptions")[1];
        videoMenu = GameObject.FindGameObjectsWithTag("MenuOptions")[2];
        applyChangeVideo = GameObject.FindGameObjectsWithTag("MenuOptions")[3];
        audioMenu = GameObject.FindGameObjectsWithTag("MenuOptions")[4];
        gameMenu = GameObject.FindGameObjectsWithTag("MenuOptions")[5];
        controlsMenu = GameObject.FindGameObjectsWithTag("MenuOptions")[6];
        helpMenu = GameObject.FindGameObjectsWithTag("MenuOptions")[7];

        if (GameObject.FindGameObjectWithTag("MenuPausa")) pauseMenuPanel = GameObject.FindGameObjectsWithTag("MenuPausa")[0];
        else pauseMenuPanel = GameObject.FindGameObjectWithTag("Empty");
        if (GameObject.FindGameObjectWithTag("MenuPausa")) backMainMenuPanel = GameObject.FindGameObjectsWithTag("MenuPausa")[1];
        else backMainMenuPanel = GameObject.FindGameObjectWithTag("Empty");
        if (GameObject.FindGameObjectWithTag("MenuPausa")) exitPanel = GameObject.FindGameObjectsWithTag("MenuPausa")[2];
        else exitPanel = GameObject.FindGameObjectWithTag("Empty");

        masterValue = Slider.FindObjectsOfType<Slider>()[0];
        brightnessSlider = Slider.FindObjectsOfType<Slider>()[1];
        SFXValue = Slider.FindObjectsOfType<Slider>()[2];
        musicValue = Slider.FindObjectsOfType<Slider>()[3];

        fullscreenToggle = Toggle.FindObjectsOfType<Toggle>()[0];
        vSyncToggle = Toggle.FindObjectsOfType<Toggle>()[1];
        vibrationToggle = Toggle.FindObjectsOfType<Toggle>()[2];

        languageDropDown = TMP_Dropdown.FindObjectsOfType<TMP_Dropdown>()[0];
        FPSDropDown = TMP_Dropdown.FindObjectsOfType<TMP_Dropdown>()[1];
        resolutionDropDown = TMP_Dropdown.FindObjectsOfType<TMP_Dropdown>()[2];

        postProcess = GameObject.FindGameObjectWithTag("PostProcess").GetComponent<PostProcessVolume>();

        optionMenu.SetActive(false);
        specificOptionMenu.SetActive(false);
        videoMenu.SetActive(false);
        applyChangeVideo.SetActive(false);
        audioMenu.SetActive(false);
        gameMenu.SetActive(false);
        controlsMenu.SetActive(false);
        helpMenu.SetActive(false);
        pauseMenuPanel.SetActive(false);
        backMainMenuPanel.SetActive(false);
        exitPanel.SetActive(false);

        if(File.Exists(Application.persistentDataPath + "/OptionsData.json")) LoadOptions();
    }

    private void Start()
    {
        postProcess.profile.TryGetSettings(out autoExposure);
        postProcess.profile.TryGetSettings(out bloom);
    }

    void LateUpdate()
    {
        CheckOptionsMenu();
        AudioChanger();
        GameChanger();
    }

    private void OnApplicationQuit()
    {
        SaveOptions();
    }

    #region OptionMenu
    public void OptionMenuButton()
    {
        menuStartButtons.SetActive(false);
        optionMenu.SetActive(true);       
    }

    public void OpenVideoMenu()
    {
        optionMenu.SetActive(false);
        specificOptionMenu.SetActive(true);
        videoMenu.SetActive(true);
    }

    public void OpenAudioMenu()
    {
        optionMenu.SetActive(false);
        specificOptionMenu.SetActive(true);
        audioMenu.SetActive(true);
    }

    public void OpenGameMenu()
    {
        optionMenu.SetActive(false);
        specificOptionMenu.SetActive(true);
        gameMenu.SetActive(true);
    }

    public void OpenControlsMenu()
    {
        optionMenu.SetActive(false);
        specificOptionMenu.SetActive(true);
        controlsMenu.SetActive(true);
    }

    public void OpenHelpMenu()
    {
        optionMenu.SetActive(false);
        specificOptionMenu.SetActive(true);
        helpMenu.SetActive(true);
    }

    public void BackToStartMenu()
    {
        SaveOptions();
        optionMenu.SetActive(false);
        menuStartButtons.SetActive(true);
    }

    #region OptionVideo
    public void PostProcessing()
    {
        //Funzione per regolare la luminosita
        autoExposure.keyValue.value = brightnessSlider.value;
        autoExposure.keyValue.value = Mathf.Clamp(autoExposure.keyValue.value, 0.5f, 1.5f);
    }

    public void VideoSettings()
    {
        //Funzione per avere lo schermo intero
        Screen.fullScreen = fullscreenToggle.isOn;
        
        //Switch per regolare lo schermo
        switch (resolutionDropDown.value)
        {
            case 0:
                Screen.SetResolution(1280, 720, fullscreenToggle.isOn);
                break;
            case 1:
                Screen.SetResolution(1366, 768, fullscreenToggle.isOn);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, fullscreenToggle.isOn);
                break;
            case 3:
                Screen.SetResolution(2650, 1440, fullscreenToggle.isOn);
                break;
            case 4:
                Screen.SetResolution(3840, 2160, fullscreenToggle.isOn);
                break;
        }


        //vSync
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
            FPSDropDown.value = 2;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        //Imposta il framerate se si vuole bloccarlo
        switch(FPSDropDown.value)
        {
            case 0:
                Application.targetFrameRate = 30;
                break;
            case 1:
                Application.targetFrameRate = 60;
                break                    ; 
            case 2:
                Application.targetFrameRate = 999;
                break; 
        }
    }

    //Apre la conferma alle impostazioni video
    public void OpenApplyVideoSetting()
    {
        applyChangeVideo.SetActive(true);
    }

    //Chiude la conferma alle impostazioni video
    public void CloseApplyVideoSetting() 
    {  
        applyChangeVideo.SetActive(false);
    }

    //Conferma le modifiche apportate nel menu video
    public void ConfirmVideoSettings()
    {
        SaveOptions();
        VideoSettings();
        PostProcessing();
        applyChangeVideo.SetActive(false);
        videoMenu.SetActive(false);
        specificOptionMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    //Chiude il menu del video
    public void CloseVideoMenu()
    {
        if (File.Exists(Application.persistentDataPath + "/OptionsData.json")) LoadOptions();
        videoMenu.SetActive(false);
        specificOptionMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    #endregion
    #region OptionsAudio
    //Funzione per poter abbassare il volume
    public void AudioChanger()
    {
        audioMixer.SetFloat("MasterVolume", masterValue.value); //Volume generale
        audioMixer.SetFloat("MusicVolume", musicValue.value); //Volume per la musica
        audioMixer.SetFloat("SFXVolume", SFXValue.value); //Volume per i suoni
    }

    //Chiude il menu del volume
    public void CloseAudioMenu()
    {
        SaveOptions();
        audioMenu.SetActive(false);
        specificOptionMenu.SetActive(false);
        optionMenu.SetActive(true);
    }
    #endregion
    #region GameMenu
    //Funzione per modificare impostazioni game
    public void GameChanger()
    {
        switch(languageDropDown.value) //Funzione da sviluppare per il multilingua
        {
            case 0:
                break;
        }

        vibrationCheck = vibrationToggle.isOn;        
    }

    //Funzione per chiudere il menu game
    public void CloseGameMenu()
    {
        SaveOptions();
        gameMenu.SetActive(false);
        specificOptionMenu.SetActive(false);
        optionMenu.SetActive(true);
    }
    #endregion
    #region ControlsMenu

    public void ChangeControls()
    {
        
    }

    public void CloseControlsMenu()
    {        
        controlsMenu.SetActive(false);
        specificOptionMenu.SetActive(false);
        optionMenu.SetActive(true);        
    }
    #endregion
    #region HelpMenu



    public void CloseHelpMenu()
    {
        helpMenu.SetActive(false);
        specificOptionMenu.SetActive(false);
        optionMenu.SetActive(true);
    }
    #endregion
    #endregion

    void CheckOptionsMenu()
    {
        if (optionMenu.activeSelf) checkOptionMenu = true; else checkOptionMenu = false;
        if (specificOptionMenu.activeSelf) checkSpecificOptionMenu = true; else checkSpecificOptionMenu = false;
    }

    void SaveOptions()
    {
        Options options = new Options();
        options.brightness = brightnessSlider.value;
        options.vSync = vSyncToggle.isOn;
        options.fullScreen = fullscreenToggle.isOn;
        options.resolution = resolutionDropDown.value;
        options.FPS = FPSDropDown.value;
        options.audioMaster = masterValue.value;
        options.audioMusic = musicValue.value;
        options.audioSFX = SFXValue.value;
        options.vibration = vibrationToggle.isOn;
        options.language = languageDropDown.value;

        string json = JsonUtility.ToJson(options);

        File.WriteAllText(Application.persistentDataPath + "/OptionsData.json", json);
    }

    void LoadOptions()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/OptionsData.json");

        Options options = JsonUtility.FromJson<Options>(json);

        brightnessSlider.value = options.brightness;
        vSyncToggle.isOn = options.vSync;
        fullscreenToggle.isOn = options.fullScreen;
        resolutionDropDown.value = options.resolution;
        FPSDropDown.value = options.FPS;
        masterValue.value = options.audioMaster;
        musicValue.value = options.audioMusic;
        SFXValue.value = options.audioSFX;
        vibrationToggle.isOn = options.vibration;
        languageDropDown.value = options.language;
    }
}
