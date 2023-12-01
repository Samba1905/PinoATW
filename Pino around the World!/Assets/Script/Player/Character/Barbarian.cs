using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : MonoBehaviour
{
    PlayerMovement playerM;



    void Start()
    {
        playerM = GetComponentInParent<PlayerMovement>();
    }

    
    void Update()
    {
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
