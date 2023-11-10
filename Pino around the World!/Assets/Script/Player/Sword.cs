using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Warrior warrior;

    private void Start()
    {
        warrior = GetComponentInParent<Warrior>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && warrior.isAttacking && warrior.doDamage)
        {
            other.gameObject.GetComponent<Enemy>().LoseHP(warrior.Damage);
            warrior.doDamage = false;
        }
    }
}
