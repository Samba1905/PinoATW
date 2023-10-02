using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameManager NULL");
            }

            return _instance;
        }
    }
    #endregion
    #region MenuPausa
    public static OptionManager oM;
    public GameObject options;
    bool checkPauseMenu;
    #endregion

    private static bool _level1;
    private static bool _level2;
    private static bool _level3;
    private static bool _level4;
    private static bool _level5;

    public static bool Level1 { get { return _level1; } }
    public static bool Level2 { get { return _level2; } }
    public static bool Level3 { get { return _level3; } }
    public static bool Level4 { get { return _level4; } }
    public static bool Level5 { get { return _level5; } }


    private static bool _save1;
    private static bool _save2;
    private static bool _save3;

    public static bool SlotSave1 
    { 
        get
        { 
            return _save1;
        }
        set
        {
            _save1 = value;
        }
    }
    public static bool SlotSave2 
    { 
        get 
        { 
            return _save2;
        }
        set
        {
            _save2 = value;
        }
    }
    public static bool SlotSave3 
    {
        get
        { 
            return _save3;
        }
        set
        {
            _save3 = value;
        }
    }

    public static bool PlayerStatus
    { 
        get
        {
            return true;
        }
    }

    private void Awake()
    {
        #region Singleton
        if (_instance)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        else _instance = this;

        DontDestroyOnLoad(this);
        #endregion

        if (GameObject.Find("OptionMenu")) options = GameObject.Find("OptionMenu");
        else options = GameObject.FindGameObjectWithTag("Empty");
        Time.timeScale = 1.0f;
    }

    private void Start()
    {        
        oM = FindObjectOfType<OptionManager>();
    }

    private void LateUpdate()
    {
        PauseMenuGame();
    }

    #region Sezione salvataggi

    public void CheckSlotsave1()
    {
        
    }








    #endregion
    #region Gestione Livelli










    #endregion
    #region MenuPausa
    void PauseMenuGame()
    {
        if (!checkPauseMenu && Input.GetButtonDown("Menu") && !oM.checkOptionMenu && !oM.checkSpecificOptionMenu) //Apre il menu durante il gioco fermando il tempo
        {
            Time.timeScale = 0f;
            oM.pauseMenuPanel.SetActive(true);
            checkPauseMenu = true;
        }
        else if (checkPauseMenu && Input.GetButtonDown("Menu") && !oM.checkOptionMenu && !oM.checkSpecificOptionMenu) //Chiude il menu durante il gioco riprendendo il tempo
        {
            Time.timeScale = 1f;
            oM.pauseMenuPanel.SetActive(false);
            checkPauseMenu = false;
        }
    }

    #region Options
    public void OpenOptions()
    {
        options.SetActive(true);
        oM.pauseMenuPanel.SetActive(false);
    }

    public void CloseOptions()
    {
        options.SetActive(false);
        oM.pauseMenuPanel.SetActive(true);
    }
    #endregion
    #region Retry
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Ricarica il livello attuale
    }
    #endregion
    #region CaricamentoNuovaScena
    public void ContinueButton()
    {
        SceneManager.LoadScene(0); //Momentaneo
    }
    #endregion
    #region Resume
    public void ResumeButton() //Fa riprendere la partita e il tempo
    {
        Time.timeScale = 1f;
        oM.pauseMenuPanel.SetActive(false);
        checkPauseMenu = false;
    }
    #endregion
    #region MainMenu
    public void OpenMainMenuPanel() //Apre il pannello di conferma per tornare al MainMenu
    {
        oM.backMainMenuPanel.SetActive(true);
    }
    public void CloseMainMenuPanel() //Chiude il pannello di conferma per tornare al MainMenu
    {
        oM.backMainMenuPanel.SetActive(false);
    }
    public void MainMenu() //Carica il MainMenu
    {
        SceneManager.LoadScene(0);
    }
    #endregion
    #region Exit
    public void OpenExitPanel() //Apre il pannello di conferma per uscire dal gioco
    {
        oM.exitPanel.SetActive(true);
    }

    public void CloseExitPanel() //Chiude il pannello di conferma per uscire dal gioco
    {
        oM.exitPanel.SetActive(false);
    }

    public void ExitButton() //Chiude l'applicazione
    {
        Application.Quit();
    }
    #endregion
    #endregion

}
