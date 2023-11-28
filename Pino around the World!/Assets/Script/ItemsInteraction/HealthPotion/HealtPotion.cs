using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealtPotion : MonoBehaviour
{
    PlayerNew player;

    private void Start()
    {
        player = FindObjectOfType<PlayerNew>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            player.UpdateHP(40f, false);
            gameObject.SetActive(false);
        }
    }
}
