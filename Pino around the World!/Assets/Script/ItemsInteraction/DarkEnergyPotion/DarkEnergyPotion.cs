using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DarkEnergyPotion : ItemsGather
{
    // Start is called before the first frame update
    void Start()
    {
        isGather = false;
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !isGather)
        {
            player.DarkEnergyPlus(25);
            isGather = true;
            Destroy(gameObject);
        }
    }
}
