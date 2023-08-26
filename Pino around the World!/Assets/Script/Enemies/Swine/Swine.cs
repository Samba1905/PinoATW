using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swine : Enemies
{
    void Start()
    {
        anim = GetComponent<Animator>();
        SetAttribute("Swine", 4, 20, 4.5f, 15f, 1.2f, 70f, 2f);
        canDamage = true;
    }

    void Update()
    {
        Healt(takeDamage);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canDamage)
        {
            other.gameObject.GetComponent<Player>().HealtPoints(damage);
            canDamage = false;
            isAttacking= false;
            StartCoroutine("TimerDamage");
        }
    }

    IEnumerator TimerDamage()
    {
        
        yield return new WaitForSeconds(attackSpeed); 
        canDamage = true;
        isAttacking = true;
    }

    private void LateUpdate()
    {
        anim.SetBool("Attack 02", isAttacking);
    }

    void FixedUpdate()
    {
        WalkEnemy();
    }
}
