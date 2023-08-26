using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beary : Enemies
{
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        SetAttribute("Beary", 10, 10, 3.5f, 12f, 1.5f, 50f, 0.5f);  
    }

    void Update()
    {
        Healt(takeDamage);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GetComponentInChildren<BearyAttack>().canDamage = true;
            GetComponentInChildren<BearyAttack>().isAttacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInChildren<BearyAttack>().canDamage = false;
        GetComponentInChildren<BearyAttack>().isAttacking = false;
    }

    private void LateUpdate()
    {
        anim.SetBool("Attack 01", GetComponentInChildren<BearyAttack>().isAttacking);
    }

    void FixedUpdate()
    {
        WalkEnemy();
    }
}
