using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject menuStartButtons;
    [SerializeField]
    bool isSaved = false;
    [SerializeField]
    Button loadGameButton;

    private void Awake()
    {
        menuStartButtons.gameObject.SetActive(true);
    }
    private void LateUpdate()
    {
        LoadGameButton();
    }
    public void NewGameButton()
    {
        SceneManager.LoadScene(1); //momentaneo
    }

    public void LoadGameButton()
    {
        if (isSaved) //momentaneo
        {
            loadGameButton.interactable = true;
        }
        else
        {
            loadGameButton.interactable = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit(); //Per uscire dal gioco
    }
}
