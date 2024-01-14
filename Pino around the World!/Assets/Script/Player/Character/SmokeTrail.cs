using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeTrail : MonoBehaviour
{
    PlayerMovement playerM;
    [SerializeField]
    ParticleSystem ps, ps1;
    private void Start()
    {
        playerM = FindObjectOfType<PlayerMovement>();
        ps.enableEmission = false;
        ps1.enableEmission = false;
    }

    private void Update()
    {
        if (playerM.barbarianRun)
        {
            ps.enableEmission = true;
            ps1.enableEmission = true;
        }
        else
        {
            Emission();
        }
    }

    void Emission()
    {
        ps.enableEmission = false;
        ps1.enableEmission = false;
    }
}
