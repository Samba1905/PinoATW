using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    [HideInInspector] public bool destroyOnLoad;
    private static ScoreManager _main;
    public static ScoreManager Main { get { return _main; } }
    #endregion

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

    private void Awake()
    {
        if (_main != null && _main != this)
        {
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            _main = this;
        }

        if (!destroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        levelEnd = false;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        coinsCount = GameObject.FindGameObjectsWithTag("TreasureChest").Length;
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
            switch((int)gameManager.currentSlot)
            {
                case 0:
                    gameManager.SaveSlot1();
                    break;
                case 1:
                    break;
                case 2:
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
            coinsVal.text = $"{coinsCount} / {coinsCollect} Coins!";
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
