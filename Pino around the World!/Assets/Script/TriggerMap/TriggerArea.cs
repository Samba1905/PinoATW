using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerArea : MonoBehaviour
{
    ScoreManager SM;
    public enum LevelComplete
    {
        _null,
        _tutorial,
        _level1,
        _level2, 
        _level3, 
        _level4, 
        _level5
    }

    public LevelComplete currentLevel;

    private void Start()
    {
        SM = FindObjectOfType<ScoreManager>();
        CurrentLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 10)
        {
            SM.levelEnd = true;
        }
    }

    public void CurrentLevel()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "FirstMap":
                currentLevel = LevelComplete._level1;
                break;
            case "SecondMap":
                currentLevel = LevelComplete._level2;
                break;
            case "ThirdMap":
                currentLevel = LevelComplete._level3;
                break;
            case "FourhMap":
                currentLevel = LevelComplete._level4;
                break;
            case "FiveMap":
                currentLevel = LevelComplete._level5;
                break;
            default:
                currentLevel = LevelComplete._null;
                break;
        }
    }
}
