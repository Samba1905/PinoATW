using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Warrior warrior;
    PlayerNew player;

    private void Start()
    {
        warrior = GetComponentInParent<Warrior>();
        player = GetComponentInParent<PlayerNew>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && warrior.isAttacking && warrior.doDamage)
        {
            other.gameObject.GetComponent<Enemy>().LoseHP(warrior.Damage);
            player.UpdateDarkEnergy(5f, false);
            warrior.doDamage = false;
        }
    }
}
