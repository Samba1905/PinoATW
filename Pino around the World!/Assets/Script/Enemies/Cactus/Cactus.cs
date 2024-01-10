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
        damageCactus = 8f;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            anim.SetBool("Attack", true);
            dmgPlayer += Time.deltaTime;
            if (dmgPlayer > 0.55f && timer == 0f && enemy.CurrentHP != 0)
            {
                other.gameObject.GetComponentInParent<PlayerNew>().UpdateHP(damageCactus, true);
                dmgPlayer = 0f;
            }
        }
    }

    private void Update()
    {
        if (rotationCount >= 12)
        {
            anim.SetBool("Attack", false);
            timer += Time.deltaTime;
            if (timer > 0.9f)
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
