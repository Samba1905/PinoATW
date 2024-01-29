using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : MonoBehaviour
{
    PlayerMovement playerM;
    PlayerNew player;
    PoolManager poolManager;
    Rigidbody rb;
    float timerSpellCD, timerSpell;
    bool restoreDE, stopMove;
    public static Vector3 position;
    public AudioClip runBarb, electricBoom;


    void Start()
    {
        playerM = GetComponentInParent<PlayerMovement>();
        player = GetComponentInParent<PlayerNew>();
        poolManager = FindObjectOfType<PoolManager>();
        rb = GetComponentInParent<Rigidbody>();
        stopMove = false;
    }

    
    void Update()
    {
        BarbSpell();
        position = transform.position;
    }

    void BarbSpell()
    {
        timerSpellCD += Time.deltaTime;
        timerSpell += Time.deltaTime;
        if(stopMove && timerSpellCD > 1.5f)
        {
            stopMove = false;
        }
        else if(!stopMove)
        {         
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
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

        if(Input.GetButtonDown("Attack3") && timerSpellCD > 1f)
        {
            playerM.animB.SetBool("isCasting", true);
            if(player.DarkEnergyPoints >= 40f)
            {
                player.UpdateDarkEnergy(40f, true);
                stopMove = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                poolManager.CastElectric();
                playerM.audioSourceSFX.PlayOneShot(electricBoom);
                timerSpellCD = 0f;
                timerSpell = 0f;
            }
            else
            {
                playerM.audioSourceSFX.PlayOneShot(playerM.failSpell);
                timerSpellCD = 0f;
            }
        }
        else
        {
            playerM.animB.SetBool("isCasting", false);
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
