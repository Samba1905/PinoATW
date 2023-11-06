using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    int rotationCount;
    float timer, dmgPlayer, damageCactus;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        damageCactus = 7.5f;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            anim.SetBool("Attack", true);
            dmgPlayer += Time.deltaTime;
            if (dmgPlayer > 2.5f && timer == 0f)
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
