using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public bool levelEnd, playerDeath;
    int enemyKilled, enemyCount, coinsCount, coinsCollect;
    [SerializeField]
    TextMeshProUGUI timeText, coinsText;
    [SerializeField]
    float timeLevel;
    [SerializeField]
    GameObject levelEndPanel;
    [SerializeField]
    TextMeshProUGUI gameOver, scoreVal, coinsVal, enemiesVal;
    [SerializeField]
    Button continueButton;

    GameManager.SlotGame slotGameSM;
    TriggerArea triggerA;

    private void Awake()
    {
        if (enemyCount == 0) enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (coinsCount == 0) coinsCount = GameObject.FindGameObjectsWithTag("TreasureChest").Length;
        if (timeText != GameObject.Find("TimeLevel")) timeText = GameObject.Find("TimeLevel").GetComponent<TextMeshProUGUI>();
        if (coinsText != GameObject.Find("CoinCounter")) coinsText = GameObject.Find("CoinCounter").GetComponent<TextMeshProUGUI>();
        if (gameOver != GameObject.Find("GameOver")) gameOver = GameObject.Find("GameOver").GetComponent<TextMeshProUGUI>();
        if (scoreVal != GameObject.Find("ScoreVal")) scoreVal = GameObject.Find("ScoreVal").GetComponent<TextMeshProUGUI>();
        if (coinsVal != GameObject.Find("CoinsVal")) coinsVal = GameObject.Find("CoinsVal").GetComponent<TextMeshProUGUI>();
        if (enemiesVal != GameObject.Find("EnemyVal")) enemiesVal = GameObject.Find("EnemyVal").GetComponent<TextMeshProUGUI>();
        if (continueButton != GameObject.Find("ContinueButton")) continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        if (levelEndPanel != GameObject.Find("LevelEndPanel")) levelEndPanel = GameObject.Find("LevelEndPanel");

        triggerA = GameObject.FindWithTag("TriggerScene").GetComponent<TriggerArea>();
    }
    void Start()
    {
        levelEnd = false;
        levelEndPanel.SetActive(false);
    }

    void Update()
    {
        TimeLevel();
        CoinText();
    }

    void TimeLevel()
    {
        if (!levelEnd)
        {
            timeLevel = Time.timeSinceLevelLoad;
            timeText.text = timeLevel.ToString("0.0");
        }
        else if (levelEnd)
        {
            Time.timeScale = 0f;
            EnemiesCount();
            MoneyCount();
            levelEndPanel.SetActive(true);
            gameOver.text = "Victory!";
            scoreVal.text = $"{timeLevel.ToString("0.0")} Sec!";
            coinsVal.text = $"{coinsCollect} / {coinsCount} Coins!";
            enemiesVal.text = $"{enemyKilled} / {enemyCount} Enemies!";
            GameManager.CheckLevelStatus();
            switch(slotGameSM)
            {
                case (GameManager.SlotGame)0:
                    SaveRecord(triggerA.currentLevel, "/Slot1Data.json");
                    GameManager.SaveSlot1();
                    break;
                case (GameManager.SlotGame)1:
                    SaveRecord(triggerA.currentLevel, "/Slot2Data.json");
                    GameManager.SaveSlot2();
                    break;
                case (GameManager.SlotGame)2:
                    SaveRecord(triggerA.currentLevel, "/Slot3Data.json");
                    GameManager.SaveSlot3();
                    break;
            }
        }
        if(playerDeath) //Se muore il Player
        {
            Time.timeScale = 0f;
            EnemiesCount();
            MoneyCount();
            levelEndPanel.SetActive(true);
            continueButton.interactable = false;
            gameOver.text = "Game Over!";
            scoreVal.text = $"{timeLevel.ToString("0.0")} Sec!";
            coinsVal.text = $"{coinsCollect} / {coinsCount} Coins!";
            enemiesVal.text = $"{enemyKilled} / {enemyCount} Enemies!";
        }
    }

    void CoinText()
    {
        coinsText.text = coinsCollect.ToString("0");
        if (Input.GetButtonUp("Interact")) Invoke("MoneyCount", 5.5f);
    }

    int EnemiesCount()
    {
        enemyKilled = enemyCount - GameObject.FindGameObjectsWithTag("Enemy").Length;
        return enemyKilled;
    }

    int MoneyCount()
    {
        coinsCollect = coinsCount - GameObject.FindGameObjectsWithTag("Coins").Length;
        return coinsCollect;
    }

    void SaveRecord(TriggerArea.LevelComplete current, string jsonData)
    {
        string json = File.ReadAllText(Application.persistentDataPath + jsonData);

        Game game = JsonUtility.FromJson<Game>(json);

        switch (current)
        {
            case (TriggerArea.LevelComplete)0:
                Debug.Log("Null");
                break;
            case (TriggerArea.LevelComplete)1:
                Debug.Log("Tutorial");
                break;
            case (TriggerArea.LevelComplete)2:
                if (timeLevel > game.timer1) game.timer1 = timeLevel;
                if (coinsCollect > game.coins1) game.coins1 = coinsCollect;
                if (enemyKilled > game.enemies1) game.enemies1 = enemyKilled;
                Debug.Log("livello1");
                break;
            case (TriggerArea.LevelComplete)3:
                if (timeLevel > game.timer2) game.timer2 = timeLevel;
                if (coinsCollect > game.coins2) game.coins2 = coinsCollect;
                if (enemyKilled > game.enemies2) game.enemies2 = enemyKilled;
                Debug.Log("livello2");
                break;
            case (TriggerArea.LevelComplete)4:
                if (timeLevel > game.timer3) game.timer3 = timeLevel;
                if (coinsCollect > game.coins3) game.coins3 = coinsCollect;
                if (enemyKilled > game.enemies3) game.enemies3 = enemyKilled;
                Debug.Log("livello3");
                break;
            case (TriggerArea.LevelComplete)5:
                if (timeLevel > game.timer4) game.timer4 = timeLevel;
                if (coinsCollect > game.coins4) game.coins4 = coinsCollect;
                if (enemyKilled > game.enemies4) game.enemies4 = enemyKilled;
                Debug.Log("livello4");
                break;
            case (TriggerArea.LevelComplete)6:
                if (timeLevel > game.timer5) game.timer5 = timeLevel;
                if (coinsCollect > game.coins5) game.coins5 = coinsCollect;
                if (enemyKilled > game.enemies5) game.enemies5 = enemyKilled;
                Debug.Log("livello5");
                break;
        }

        json = JsonUtility.ToJson(game);

        File.WriteAllText(Application.persistentDataPath + jsonData, json);
    }
}
