using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearyAttack : Beary
{
    void Update()
    {
        transform.position = transform.parent.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canDamage)
        {
            other.gameObject.GetComponent<Player>().HealtPoints(damage);
            canDamage = false;
            StartCoroutine("TimerDamage");
        }
    }

    IEnumerator TimerDamage()
    {
        yield return new WaitForSeconds(attackSpeed);
        canDamage = true;
    }
}
