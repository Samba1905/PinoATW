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
    #region Singleton
    [HideInInspector] public bool destroyOnLoad;
    private static OptionManager _main;
    public static OptionManager Main { get { return _main; } }
    #endregion

    [SerializeField]
    GameObject optionMenu, menuStartButtons, specificOptionMenu;
    public bool checkOptionMenu, checkSpecificOptionMenu;
    private bool SaveCheck = false;

    #region VideoSettings
    [SerializeField]
    GameObject videoMenu, closeVideoMenu, applyChangeVideo;
    [SerializeField]
    Slider brightnessSlider;
    [SerializeField]
    Toggle vSyncToggle, fullscreenToggle;
    [SerializeField]
    TMP_Dropdown resolutionDropDown, FPSDropDown;
    [SerializeField]
    PostProcessVolume postProcess;
    private AutoExposure autoExposure = null;
    [SerializeField]
    bool confirmedVideoChanges;
    #endregion
    #region AudioSettings
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    Slider masterValue, musicValue, SFXValue;
    [SerializeField]
    GameObject audioMenu, closeAudioMenu;
    #endregion
    #region GameSettings
    [SerializeField]
    GameObject gameMenu, closeGameMenu;
    [SerializeField]
    TMP_Dropdown languageDropDown;
    [SerializeField]
    Toggle vibrationToggle;
    public bool vibrationCheck;
    Gamepad gamepad;
    #endregion
    #region ControlsSettings
    [SerializeField]
    GameObject controlsMenu, closeControlsMenu;
    #endregion
    #region HelpSettings
    [SerializeField]
    GameObject helpMenu, closeHelpMenu;
    #endregion

    private void Awake()
    {
        if (_main != null && _main != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _main = this;
        }

        if (!destroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        if(SaveCheck)
        {
            SaveOptions();
            SaveCheck = false;
        }
        LoadOptions();
    }

    private void Start()
    {
        postProcess.profile.TryGetSettings(out autoExposure);
    }

    void LateUpdate()
    {
        CheckOptionsMenu();
        PostProcessing();
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
        optionMenu.SetActive(false);
        menuStartButtons.SetActive(true);
    }

    #region OptionVideo
    public void PostProcessing()
    {
        //Funzione per regolare la luminosita
        autoExposure.keyValue.value = brightnessSlider.value;
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
        applyChangeVideo.SetActive(false);
        videoMenu.SetActive(false);
        specificOptionMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    //Chiude il menu del video
    public void CloseVideoMenu()
    {
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
    }
}
