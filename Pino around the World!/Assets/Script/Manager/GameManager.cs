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
    bool checkPauseMenu;
    #endregion

    public static bool WarriorCheck { get; set; }
    public static bool MageCheck { get; set; }
    public static bool BarbarianCheck { get; set; }

    private void Awake()
    {
        #region Singleton
        if (_instance) Destroy(gameObject);
        else _instance = this;

        DontDestroyOnLoad(this);
        #endregion

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
