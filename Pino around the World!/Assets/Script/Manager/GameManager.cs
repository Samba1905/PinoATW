using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    OptionManager oM;
    PlayerNew player;
    bool checkPauseMenu;
    #endregion
    public SlotGame currentSlot;

    public enum SlotGame
    {
        Slot1,
        Slot2,
        Slot3
    }

    private static bool _level1;
    private static bool _level2;
    private static bool _level3;
    private static bool _level4;
    private static bool _level5;

    public static bool Level1
    {
        get
        {
            return _level1;
        }
    }
    public static bool Level2
    { 
        get
        {
            return _level2;
        }
    }
    public static bool Level3
    {
        get
        {
            return _level3;
        }
    }
    public static bool Level4
    {
        get
        {
            return _level4;
        }
    }
    public static bool Level5
    {
        get
        {
            return _level5;
        }
    }

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
        get ;
    }

    private void Awake()
    {
        #region Singleton
        if (_instance)
        {
            Destroy(gameObject);
        }
        else _instance = this;

        DontDestroyOnLoad(this);
        #endregion
        Time.timeScale = 1.0f;
    }

    private void LateUpdate()
    {
        PauseMenuGame();
        if (oM != FindObjectOfType<OptionManager>()) oM = FindObjectOfType<OptionManager>();
        if (player != FindObjectOfType<PlayerMovement>()) player = FindObjectOfType<PlayerNew>();
        if(checkPauseMenu) if (!oM.pauseMenuPanel.activeSelf) checkPauseMenu = false;
    }

    #region Sezione salvataggi
    public void SaveSlot1()
    {
        Game game = new Game();

        game.level1 = Level1;
        game.level2 = Level2;
        game.level3 = Level3;
        game.level4 = Level4;
        game.level5 = Level5;

        string json = JsonUtility.ToJson(game);

        File.WriteAllText(Application.persistentDataPath + "/Slot1Data.json", json);
    }
    public void SaveSlot2()
    {
        Game game = new Game();

        game.level1 = Level1;
        game.level2 = Level2;
        game.level3 = Level3;
        game.level4 = Level4;
        game.level5 = Level5;

        string json = JsonUtility.ToJson(game);

        File.WriteAllText(Application.persistentDataPath + "/Slot2Data.json", json);
    }
    public void SaveSlot3()
    {
        Game game = new Game();

        game.level1 = Level1;
        game.level2 = Level2;
        game.level3 = Level3;
        game.level4 = Level4;
        game.level5 = Level5;

        string json = JsonUtility.ToJson(game);

        File.WriteAllText(Application.persistentDataPath + "/Slot3Data.json", json);
    }

    public void LoadSlot1()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/Slot1Data.json");

        Game game = JsonUtility.FromJson<Game>(json);

        _level1 = game.level1;
        _level2 = game.level2;
        _level3 = game.level3;
        _level4 = game.level4;
        _level5 = game.level5;
    }
    public void LoadSlot2()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/Slot2Data.json");

        Game game = JsonUtility.FromJson<Game>(json);

        _level1 = game.level1;
        _level2 = game.level2;
        _level3 = game.level3;
        _level4 = game.level4;
        _level5 = game.level5;
    }
    public void LoadSlot3()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/Slot3Data.json");

        Game game = JsonUtility.FromJson<Game>(json);

        _level1 = game.level1;
        _level2 = game.level2;
        _level3 = game.level3;
        _level4 = game.level4;
        _level5 = game.level5;
    }
    #endregion
    #region Gestione Livelli
    public void CheckLevelStatus()
    {
        _level1 = player.Lvl1;
        _level2 = player.Lvl2;
        _level3 = player.Lvl3;
        _level4 = player.Lvl4;
        _level5 = player.Lvl5;
    }
    #endregion
    #region MenuPausa
    void PauseMenuGame()
    {
        if (SceneManager.GetActiveScene().name != "MenuScene")
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
    }
    #endregion

}
