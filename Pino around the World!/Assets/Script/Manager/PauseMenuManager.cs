using LayerLab.CasualGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuPanel, exitPanel, mainMenuPanel;
    OptionManager oM;
    bool checkPauseMenu;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 1.0f;
    }
    private void Start()
    {
        oM = FindObjectOfType<OptionManager>();
    }

    private void LateUpdate()
    {
        openPauseMenu();
    }

    void openPauseMenu()
    {
        if(!checkPauseMenu && Input.GetButtonDown("Menu") && !oM.checkOptionMenu && !oM.checkSpecificOptionMenu) //Apre il menu durante il gioco fermando il tempo
        {
            Time.timeScale = 0f;
            pauseMenuPanel.SetActive(true);
            checkPauseMenu = true;
        }
        else if(checkPauseMenu && Input.GetButtonDown("Menu") && !oM.checkOptionMenu && !oM.checkSpecificOptionMenu) //Chiude il menu durante il gioco riprendendo il tempo
        {
            Time.timeScale = 1f;
            pauseMenuPanel.SetActive(false);
            checkPauseMenu = false;
        }
    }

    #region LevelEndMenu
    public void RetryButton()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Ricarica il livello attuale
    }
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
    #region Option
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
}
