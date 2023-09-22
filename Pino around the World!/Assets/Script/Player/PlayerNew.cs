using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerNew : MonoBehaviour
{
    float _healtsPoints, _staminaPoints, _darkEnergyPoints;
    float maxHP = 100f, minHP = 0f;
    float maxSTA = 100f, minSTA = 0f, overSTA = -25;
    float maxDE = 100f, minDE = 0f;
    [SerializeField] bool recoverySTA, exhaustState;
    public bool DANNO;

    PlayerMovement playerM;

    float HealtsPoints
    {
        get
        {
            return _healtsPoints;
        }
        set
        {
            if (value > maxHP)
            {
                _healtsPoints = maxHP;
            }
            else if (value < minHP)
            {
                _healtsPoints = minHP;
            }
            else
            {
                _healtsPoints = value;
            }
        }
    }

    float StaminaPoints
    {
        get
        {
            return _staminaPoints;
        }
        set
        {
            if(value > maxSTA)
            { 
                _staminaPoints = maxSTA;
                exhaustState = false;
            }
            else if(value < minSTA)
            {
                _staminaPoints = value;
                exhaustState = true;
                if(value < overSTA)
                {
                    _staminaPoints = overSTA;
                }
            }
            else
            {
                _staminaPoints = value;
            }
        }
    }

    float DarkEnergyPoints
    {
        get 
        { 
            return _darkEnergyPoints;
        }
        set
        { 
            if(value < maxDE)
            {
                _darkEnergyPoints = maxDE;
            }
            else if(value < minDE)
            {
                _darkEnergyPoints = minDE;
            }
            else
            {
                _darkEnergyPoints = value;
            }
        }
    }

    private void Awake()
    {
        if (GameManager.WarriorCheck)
        {
            GameObject.Find("Mage").SetActive(false);
            GameObject.Find("Barbarian").SetActive(false);

            Destroy(GameObject.Find("Mage"));
            Destroy(GameObject.Find("Barbarian"));

        }
        else if (GameManager.MageCheck)
        {
            GameObject.Find("Warrior").SetActive(false);
            GameObject.Find("Barbarian").SetActive(false);

            Destroy(GameObject.Find("Warrior"));
            Destroy(GameObject.Find("Barbarian"));
        }
        else if (GameManager.BarbarianCheck)
        {
            GameObject.Find("Mage").SetActive(false);
            GameObject.Find("Warrior").SetActive(false);

            Destroy(GameObject.Find("Mage"));
            Destroy(GameObject.Find("Warrior"));
        }
    }

    private void Start()
    {
        recoverySTA = true;
        HealtsPoints = maxHP;
        StaminaPoints = maxSTA;
        playerM = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        UpdateSTA();
        if (DANNO)
        {
            UpdateHP(5, true);
            DANNO = false;
        }
        //Debug.Log(StaminaPoints);
    }

    void UpdateSTA()
    {
        if (recoverySTA && !exhaustState) StaminaPoints += Time.deltaTime * 12.5f;
        else if (recoverySTA && exhaustState) StaminaPoints += Time.deltaTime * 7.5f;

        if (playerM.consumeRun)
        {
            recoverySTA = false;
            StaminaPoints -= Time.deltaTime * 15f;
        }
        else recoverySTA = true;

        if(playerM.consumeDash)
        {
            playerM.consumeDash = false;
            StaminaPoints -= 25f;
        }
    }

    public float UpdateHP(float value, bool damage)
    {
        if (damage)
        {
            playerM.anim.SetTrigger("TakeDamage");
            return HealtsPoints -= value;
        }
        return HealtsPoints += value;
    }
}
