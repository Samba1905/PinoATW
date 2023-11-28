using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEnergyPotion : MonoBehaviour
{
    PlayerNew player;

    private void Start()
    {
        player = FindObjectOfType<PlayerNew>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            player.UpdateDarkEnergy(50f, false);
            gameObject.SetActive(false);
        }
    }
}
