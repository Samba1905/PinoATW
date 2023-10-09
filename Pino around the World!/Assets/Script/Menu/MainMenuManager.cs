using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject menuStartButtons;   
    [SerializeField]
    Button loadGameButton;
    public GameManager.SlotGame currentSlotMM;

    [Header("LevelsComponets")]
    [SerializeField]
    GameObject levelSelMenu;
    [SerializeField]
    Button level1, level2, level3, level4, level5;

    [Header("SlotsFile")]
    [SerializeField]
    TMP_InputField IF;
    [SerializeField]
    GameObject gameSelMenu;
    [SerializeField]
    GameObject inputFieldPanel;
    [SerializeField]
    TextMeshProUGUI tSlot1, tSlot2, tSlot3;
    [SerializeField]
    Button bSlot1, bSlot2, bSlot3;

    [Header("DeleteComponets")]
    [SerializeField]
    GameObject deletePanel;
    [SerializeField]
    Button dbSlot1, dbSlot2, dbSlot3;
    [SerializeField]
    TextMeshProUGUI dtSlot1, dtSlot2, dtSlot3;

    bool gameSel;

    string slot1, slot2, slot3;

    private void Awake()
    {
        menuStartButtons.gameObject.SetActive(true);
        Camera.main.transform.position = new Vector3(7f, 7f, 5f);
        Camera.main.transform.localRotation = Quaternion.Euler(40f, 144f, 0f);
        gameSel = false;
    }

    private void Start()
    {
        LoadSlotDataText();
    }

    private void LateUpdate()
    {
        LoadInteraction();        
        CamInteraction();
        DeleteButtonsInteraction();
        SlotGameTextInteraction();
    }

    bool LoadInteraction()//Rendere interagibile LoadButton
    {
        if (GameManager.SlotSave1 || GameManager.SlotSave2 || GameManager.SlotSave3)
        {
            return loadGameButton.interactable = true;
        }
        
        return loadGameButton.interactable = false;
    }

    void CamInteraction()//Movimento camera
    {
        if (gameSel) CamGameSel();
        else if (!gameSel) CamStandardPos();
    }

    void DeleteButtonsInteraction()//Funzione per far coincidere nomi
    {
        dtSlot1.text = tSlot1.text;
        dtSlot2.text = tSlot2.text;
        dtSlot3.text = tSlot3.text;
    }

    void SlotGameTextInteraction() //Funzione per aggiornare i nomi
    {
        tSlot1.text = slot1;
        tSlot2.text = slot2;
        tSlot3.text = slot3;
    }

    void LoadSlotDataText()//Funzione per caricare i nomi correttamente
    {
        if (File.Exists(Application.persistentDataPath + "/Slot1Data.json"))
        {
            slot1 = File.ReadAllText(Application.persistentDataPath + "/Slot1Data.json");
            Game game = JsonUtility.FromJson<Game>(slot1);
            slot1 = game.slotName1;
        }
        if (File.Exists(Application.persistentDataPath + "/Slot2Data.json"))
        {
            slot2 = File.ReadAllText(Application.persistentDataPath + "/Slot2Data.json");
            Game game = JsonUtility.FromJson<Game>(slot2);
            slot2 = game.slotName2;
        }
        if (File.Exists(Application.persistentDataPath + "/Slot3Data.json"))
        {
            slot3 = File.ReadAllText(Application.persistentDataPath + "/Slot3Data.json");
            Game game = JsonUtility.FromJson<Game>(slot3);
            slot3 = game.slotName3;
        }
    }

    public void NewGameButton() //Funzione per andare al multisalvataggio
    {
        gameSel = true;
        menuStartButtons.gameObject.SetActive(false);
        gameSelMenu.SetActive(true);
    }

    public void BackFromGameSel() //Funzione per tornare al mainmenu dal multisalvataggio
    {
        gameSel = false;
        menuStartButtons.gameObject.SetActive(true);
        gameSelMenu.SetActive(false);
    }
   
    public void LoadGameButton() //Funzione per far interagire gli slot con salvataggio
    {
        levelSelMenu.SetActive(true);
        if(!GameManager.SlotSave1) bSlot1.interactable = false;
        else bSlot1.interactable = true;
        if(!GameManager.SlotSave2) bSlot2.interactable = false;
        else bSlot2.interactable = true;
        if(!GameManager.SlotSave3) bSlot3.interactable = false;
        else bSlot3.interactable = true;
    }

    public void QuitGame()//Per uscire dal gioco
    {
        Application.Quit(); 
    }
    void CamStandardPos() //Posizione telecamera normale
    {
        Vector3 camPos = new Vector3(7f, 7f, 5f);
        Vector3 camRot = new Vector3(40f, 144f, 0f);
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, camPos, Time.deltaTime);
        Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(camRot), Time.deltaTime);
    }

    void CamGameSel() //Posizione telecamera ravvicinata
    {
        Vector3 camPos = new Vector3(9.5f, 2.5f, 1.8f);
        Vector3 camRot = new Vector3(30f, 144f, 0f);
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, camPos, Time.deltaTime);
        Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(camRot), Time.deltaTime);
    }

    public void ConfirmText() //Funzione per creare il salvataggio
    {
        Game game = new Game();

        switch (currentSlotMM)
        {
            case (GameManager.SlotGame)0:
                tSlot1.text = IF.text;
                slot1 = tSlot1.text;
                inputFieldPanel.gameObject.SetActive(false);
                GameManager.SlotSave1 = true;

                game.slotName1 = slot1;
                slot1 = JsonUtility.ToJson(game);
                File.WriteAllText(Application.persistentDataPath + "/Slot1Data.json", slot1);

                SceneManager.LoadScene(1);
                break;
            case (GameManager.SlotGame)1:
                tSlot2.text = IF.text;
                inputFieldPanel.gameObject.SetActive(false);
                GameManager.SlotSave2 = true;
                slot2 = IF.text;
                game.slotName2 = slot2;
                slot2 = JsonUtility.ToJson(game);
                File.WriteAllText(Application.persistentDataPath + "/Slot2Data.json", slot2);

                SceneManager.LoadScene(1);
                break;
            case (GameManager.SlotGame)2:
                tSlot3.text = IF.text;
                inputFieldPanel.gameObject.SetActive(false);
                GameManager .SlotSave3 = true;
                slot3 = IF.text;
                game .slotName3 = slot3;
                slot3 = JsonUtility.ToJson(game);
                File.WriteAllText(Application.persistentDataPath + "/Slot3Data.json", slot3);

                SceneManager.LoadScene(1);
                break;
        }
    }

    public void BackFromConfirmName()
    {
        inputFieldPanel.gameObject.SetActive(false);
    }

    public void OpenDeleteFile()
    {
        deletePanel.gameObject.SetActive(true);
    }

    public void BackFromDeleteFile()
    {
        deletePanel.gameObject.SetActive(false);
    }

    public void DeleteFile1()
    {
        GameManager.SlotSave1 = false;
        tSlot1.text = "Empty";
        slot1 = null;
        File.Delete(Application.persistentDataPath + "/SlotData1.json");
        BackFromDeleteFile();
        BackFromGameSel();
    }

    public void DeleteFile2()
    {
        GameManager.SlotSave2 = false;
        tSlot2.text = "Empty";
        slot2 = null;
        File.Delete(Application.persistentDataPath + "/SlotData2.json");
        BackFromDeleteFile();
        BackFromGameSel();
    }

    public void DeleteFile3()
    {
        GameManager.SlotSave3 = false;
        tSlot3.text = "Empty";
        slot3 = null;
        File.Delete(Application.persistentDataPath + "/SlotData3.json");
        BackFromDeleteFile();
        BackFromGameSel();
    }

    void GameSelection()
    {

    }

    public void BackFromGameSelection()
    {
        levelSelMenu.SetActive(false);
        gameSelMenu.SetActive(true);
    }

    void LevelProgress()
    {
        if (GameManager.Level1) level2.interactable = true;
        else level2.interactable = false;
        if (GameManager.Level2) level3.interactable = true;
        else level3.interactable = false;
        if (GameManager.Level3) level4.interactable = true;
        else level4.interactable = false;
        if (GameManager.Level4) level5.interactable = true;
        //if (GameManager.Level5) ;   qualcosa
    }

    public void Slot1()
    {
        if(!GameManager.SlotSave1)
        {
            inputFieldPanel.gameObject.SetActive(true);
            currentSlotMM = (GameManager.SlotGame)0;
        }
        else if(GameManager.SlotSave1)
        { 
            LevelProgress();
            gameSelMenu.SetActive(false);
            levelSelMenu.gameObject.SetActive(true);
        }
    }

    public void Slot2()
    {
        if (!GameManager.SlotSave2)
        {
            inputFieldPanel.gameObject.SetActive(true);
            currentSlotMM = (GameManager.SlotGame)1;
        }
        else if (GameManager.SlotSave2)
        {
            LevelProgress();
            gameSelMenu.SetActive(false);
            levelSelMenu.gameObject.SetActive(true);
        }
    }

    public void Slot3()
    {
        if (!GameManager.SlotSave3)
        {
            inputFieldPanel.gameObject.SetActive(true);
            currentSlotMM = (GameManager.SlotGame)2;
        }
        else if (GameManager.SlotSave3)
        {
            LevelProgress();
            gameSelMenu.SetActive(false);
            levelSelMenu.gameObject.SetActive(true);
        }
    }
}
