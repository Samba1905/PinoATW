using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    [HideInInspector] public bool destroyOnLoad;
    private static GameManager _main;
    public static GameManager Main { get { return _main; } }
    #endregion
    #region MenuPausa
    [SerializeField] GameObject pauseMenuPanel, exitPanel, mainMenuPanel;
    OptionManager oM;
    bool checkPauseMenu;
    #endregion

    private void Awake()
    {
        #region Singleton
        if (_main != null && _main != this)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            _main = this;
        }

        if (!destroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
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
            pauseMenuPanel.SetActive(true);
            checkPauseMenu = true;
        }
        else if (checkPauseMenu && Input.GetButtonDown("Menu") && !oM.checkOptionMenu && !oM.checkSpecificOptionMenu) //Chiude il menu durante il gioco riprendendo il tempo
        {
            Time.timeScale = 1f;
            pauseMenuPanel.SetActive(false);
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
        pauseMenuPanel.SetActive(false);
        checkPauseMenu = false;
    }
    #endregion
    #region MainMenu
    public void OpenMainMenuPanel() //Apre il pannello di conferma per tornare al MainMenu
    {
        mainMenuPanel.SetActive(true);
    }
    public void CloseMainMenuPanel() //Chiude il pannello di conferma per tornare al MainMenu
    {
        mainMenuPanel.SetActive(false);
    }
    public void MainMenu() //Carica il MainMenu
    {
        SceneManager.LoadScene(0);
    }
    #endregion
    #region Exit
    public void OpenExitPanel() //Apre il pannello di conferma per uscire dal gioco
    {
        exitPanel.SetActive(true);
    }

    public void CloseExitPanel() //Chiude il pannello di conferma per uscire dal gioco
    {
        exitPanel.SetActive(false);
    }

    public void ExitButton() //Chiude l'applicazione
    {
        Application.Quit();
    }
    #endregion
    #endregion
}
