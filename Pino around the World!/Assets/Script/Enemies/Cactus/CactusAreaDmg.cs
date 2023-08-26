using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusAreaDmg : Cactus
{

    void Update()
    {
        transform.position = transform.parent.position;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canDamage)
        {
            other.gameObject.GetComponent<Player>().HealtPoints(damage);
            isAttacking= true;
            canDamage = false;          
            StartCoroutine("TimerDamage");
        }
    }

    IEnumerator TimerDamage()
    { 
        yield return new WaitForSeconds(attackSpeed);
        isAttacking= false;
        canDamage = true;
    }
}
