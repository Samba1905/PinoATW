using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Enemies
{
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        SetAttribute("Cactus", 7, 12, 0.5f, 4f, 1.25f, 30f, 0.75f);
        canDamage = true;
        isAttacking = false;
    }
    void Update()
    {
        Healt(takeDamage);
    }
    private void LateUpdate()
    {
        anim.SetBool("Spin Attack", GetComponentInChildren<CactusAreaDmg>().isAttacking);
    }

    private void FixedUpdate()
    {
        WalkEnemy();
    }
}
