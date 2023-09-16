using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject menuStartButtons, chSelMenu;
    [SerializeField]
    bool isSaved = false;
    [SerializeField]
    Button loadGameButton;
    bool chSel;

    private void Awake()
    {
        menuStartButtons.gameObject.SetActive(true);
        Camera.main.transform.position = new Vector3(7f, 7f, 5f);
        Camera.main.transform.localRotation = Quaternion.Euler(40f, 144f, 0f);
        chSel = false;
    }
    private void LateUpdate()
    {
        LoadGameButton();
        //Movimento camera
        if (chSel)
        {
            CamChSel();
        }
        else if(!chSel)
        {
            CamStandardPos();
        }
    }
    public void NewGameButton()
    {
        chSel = true;
        menuStartButtons.gameObject.SetActive(false);
        chSelMenu.SetActive(true);
    }

    public void BackFromChSel()
    {
        chSel = false;
        menuStartButtons.gameObject.SetActive(true);
        chSelMenu.SetActive(false);
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
    void CamStandardPos()
    {
        Vector3 camPos = new Vector3(7f, 7f, 5f);
        Vector3 camRot = new Vector3(40f, 144f, 0f);
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, camPos, Time.deltaTime);
        Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(camRot), Time.deltaTime);
    }

    void CamChSel()
    {
        Vector3 camPos = new Vector3(9.5f, 2.5f, 1.8f);
        Vector3 camRot = new Vector3(30f, 144f, 0f);
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, camPos, Time.deltaTime);
        Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(camRot), Time.deltaTime);
    }
    public void WarriorSel()
    {
        GameManager.WarriorCheck = true;
        GameManager.MageCheck = false;
        GameManager.BarbarianCheck = false;
        SceneManager.LoadScene(2);
    }
    public void MageSel()
    {
        GameManager.WarriorCheck = false;
        GameManager.MageCheck = true;
        GameManager.BarbarianCheck = false;
        SceneManager.LoadScene(2);
    }
    public void BarbarianSel()
    {
        GameManager.WarriorCheck = false;
        GameManager.MageCheck = false;
        GameManager.BarbarianCheck = true;
        SceneManager.LoadScene(2);
    }
}
