using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    bool isSaved = false;
    [SerializeField]
    Button loadGameButton;

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
