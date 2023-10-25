using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public bool levelEnd, playerDeath;
    int enemyKilled, enemyCount, coinsCount, coinsCollect;
    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    float timeLevel;
    [SerializeField]
    GameObject levelEndPanel;
    [SerializeField]
    TextMeshProUGUI gameOver, scoreVal, coinsVal, enemiesVal;

    GameManager gameManager;
    public GameManager.SlotGame slotGameSM;

    private void Awake()
    {
        if (enemyCount == 0) enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (coinsCount == 0) coinsCount = GameObject.FindGameObjectsWithTag("TreasureChest").Length;
        if (timeText != GameObject.Find("TimeLevel")) timeText = GameObject.Find("TimeLevel").GetComponent<TextMeshProUGUI>();
        if (gameOver != GameObject.Find("GameOver")) gameOver = GameObject.Find("GameOver").GetComponent<TextMeshProUGUI>();
        if (scoreVal != GameObject.Find("ScoreVal")) scoreVal = GameObject.Find("ScoreVal").GetComponent<TextMeshProUGUI>();
        if (coinsVal != GameObject.Find("CoinsVal")) coinsVal = GameObject.Find("CoinsVal").GetComponent<TextMeshProUGUI>();
        if (enemiesVal != GameObject.Find("EnemyVal")) enemiesVal = GameObject.Find("EnemyVal").GetComponent<TextMeshProUGUI>();
        if (levelEnd != GameObject.Find("LevelEndPanel")) levelEnd = GameObject.Find("LevelEndPanel");
    }
    void Start()
    {
        levelEnd = false;
        levelEndPanel.SetActive(false);
    }

    void Update()
    {
        TimeLevel();
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
            coinsVal.text = $"{coinsCount} / {coinsCollect} Coins!";
            enemiesVal.text = $"{enemyKilled} / {enemyCount} Enemies!";
            gameManager.CheckLevelStatus();
            switch(slotGameSM)
            {
                case (GameManager.SlotGame)0:
                    gameManager.SaveSlot1();
                    break;
                case (GameManager.SlotGame)1:
                    gameManager.SaveSlot2();
                    break;
                case (GameManager.SlotGame)2:
                    gameManager.SaveSlot3();
                    break;
            }
        }
        if(playerDeath)//Funzione da riprogrammare
        {
            Time.timeScale = 0f;
            EnemiesCount();
            MoneyCount();
            levelEndPanel.SetActive(true);
            gameOver.text = "Game Over!";
            scoreVal.text = $"{timeLevel.ToString("0.0")} Sec!";
            coinsVal.text = $"{coinsCollect} / {coinsCount} Coins!";
            enemiesVal.text = $"{enemyKilled} / {enemyCount} Enemies!";
        }
    }

    int EnemiesCount()
    {
        enemyKilled = enemyCount - GameObject.FindGameObjectsWithTag("Enemy").Length;
        return enemyKilled;
    }

    int MoneyCount()
    {
        coinsCollect = coinsCount - GameObject.FindGameObjectsWithTag("TreasureChest").Length;
        return coinsCollect;
    }
}
