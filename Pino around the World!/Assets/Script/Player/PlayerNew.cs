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
    float timerInvulnerable;
    [SerializeField] bool recoverySTA, exhaustState, deadState, vulnerable;
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
        
    }

    private void Start()
    {
        recoverySTA = true;
        HealtsPoints = maxHP;
        StaminaPoints = maxSTA;
        vulnerable = true;
        playerM = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        UpdateSTA(0f);
        isDead();
        Timer();

        if (DANNO) //per fare test
        {
            UpdateHP(5, true); 
            DANNO = false;
        }       
    }

    public void UpdateSTA(float riduzioneStamina) //Funzione per creare una meccanica di stamina
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

        StaminaPoints -= riduzioneStamina;
    }

    public float UpdateHP(float value, bool damage) //Funzione per gestire la vita del player
    {
        if (vulnerable)
        {
            if (damage)
            {
                playerM.anim.SetTrigger("TakeDamage");
                timerInvulnerable = 1.5f;
                InvulnerableStatus(true);
                return HealtsPoints -= value;
            }
        }
        return HealtsPoints += value;
    }

    bool isDead() //Funzione per gestire lo stato morto del player
    {
        playerM.anim.SetBool("isDead", deadState);
        if (HealtsPoints <= 0)
        {
            return deadState = true;
        }           
        return deadState = false;
    }

    bool InvulnerableStatus(bool invulnerable) //Funzione per invulnerabilità
    {
        if (invulnerable) return vulnerable = false;
        return vulnerable = true;
    }

    void Timer() //Timer per invulenrabilità
    {
        timerInvulnerable -= Time.deltaTime;
        if (timerInvulnerable < 0) InvulnerableStatus(false);
    }
}
