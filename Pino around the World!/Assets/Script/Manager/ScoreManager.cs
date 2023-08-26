using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public bool levelEnd;
    int enemyKilled, enemyCount, coinsCount;
    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    float timeLevel;
    [SerializeField]
    GameObject levelEndPanel;
    [SerializeField]
    TextMeshProUGUI gameOver, scoreVal, coinsVal, enemiesVal;
    Player player;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        levelEnd = false;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        coinsCount = GameObject.FindGameObjectsWithTag("TreasureChest").Length;
        levelEndPanel.SetActive(false);
        player = FindObjectOfType<Player>();
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
            levelEndPanel.SetActive(true);
            gameOver.text = "Victory!";
            scoreVal.text = $"{timeLevel.ToString("0.0")} Sec!";
            coinsVal.text = $"{player.nCoin} / {coinsCount * 3} Coins!";
            enemiesVal.text = $"{enemyKilled} / {enemyCount} Enemies!";
        }
        if(player.isDeathUI)
        {
            Time.timeScale = 0f;
            EnemiesCount();
            levelEndPanel.SetActive(true);
            gameOver.text = "Game Over!";
            scoreVal.text = $"{timeLevel.ToString("0.0")} Sec!";
            coinsVal.text = $"{player.nCoin} / {coinsCount * 3} Coins!";
            enemiesVal.text = $"{enemyKilled} / {enemyCount} Enemies!";
        }
    }

    public int EnemiesCount()
    {
        enemyKilled = enemyCount - GameObject.FindGameObjectsWithTag("Enemy").Length;
        return enemyKilled;
    }
}
