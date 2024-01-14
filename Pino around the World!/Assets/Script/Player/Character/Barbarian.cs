using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : MonoBehaviour
{
    PlayerMovement playerM;
    PlayerNew player;
    PoolManager poolManager;
    float timerSpellCD, timerSpell;
    bool restoreDE;
    public AudioClip runBarb;


    void Start()
    {
        playerM = GetComponentInParent<PlayerMovement>();
        player = GetComponentInParent<PlayerNew>();
        poolManager = FindObjectOfType<PoolManager>();
    }

    
    void Update()
    {
        BarbRun();
    }

    void BarbRun()
    {
        timerSpellCD += Time.deltaTime;
        timerSpell += Time.deltaTime;
        if(Input.GetButtonDown("Attack1"))
        {

        }

        if (Input.GetButtonDown("Attack2") && timerSpellCD > 0.33f && !player.ExhaustState)
        {
            if (player.DarkEnergyPoints >= 25f)
            {
                player.UpdateDarkEnergy(25f, true);
                playerM.barbarianRun = true;
                playerM.audioSourceSFX.PlayOneShot(runBarb);
                timerSpellCD = 0f;
                timerSpell = 0f;
                restoreDE = true;
            }
            else
            {
                playerM.audioSourceSFX.PlayOneShot(playerM.failSpell);
                timerSpellCD = 0f;
            }
        }

        if(Input.GetButtonDown("Attack3"))
        {

        }

        if(timerSpell > 2f)
        {
            playerM.barbarianRun = false;
            if(restoreDE)
            {
                player.UpdateDarkEnergy(10f, false);
                restoreDE = false;
            }
        }
    }

    void EvFootStep()
    {
        playerM.audioSourceSFX.pitch = Random.Range(0.95f, 1.05f);
        playerM.audioSourceSFX.PlayOneShot(playerM.footStep);
    }
    void EvFootStep2()
    {
        playerM.audioSourceSFX.pitch = Random.Range(0.85f, 1.15f);
        playerM.audioSourceSFX.PlayOneShot(playerM.footStep2);
    }

    void EvIdle()
    {
        playerM.audioSourceSFX.pitch = 1f;
    }
}
