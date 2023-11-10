using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    int rotationCount;
    float timer, dmgPlayer, damageCactus;
    Enemy enemy;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        enemy = GetComponent<Enemy>();
        damageCactus = 7.5f;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            anim.SetBool("Attack", true);
            dmgPlayer += Time.deltaTime;
            if (dmgPlayer > 1.75f && timer == 0f && enemy.CurrentHP != 0)
            {
                other.gameObject.GetComponentInParent<PlayerNew>().UpdateHP(damageCactus, true);
                dmgPlayer = 0f;
            }
        }
    }

    private void Update()
    {
        if (rotationCount >= 10)
        {
            anim.SetBool("Attack", false);
            timer += Time.deltaTime;
            if (timer > 5f)
            {
                rotationCount = 0;
                dmgPlayer = 0f;
                timer = 0f;
            }
        }
    }

    void EvCounterAttack()
    {
        rotationCount++;
    }
}
