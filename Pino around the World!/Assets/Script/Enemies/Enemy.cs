using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float _healtsPoints;
    [SerializeField]
    float maxHP, minHP = 0;
    [SerializeField]
    float speed, timer, timerImmune;
    [SerializeField]
    bool isDeath, immune;
    Animator anim;

    float currentHP
    {
        get
        {
            return _healtsPoints;
        }
        set
        {
            if (value > maxHP)
            {
                _healtsPoints = maxHP;
            }
            else if (value < minHP)
            {
                _healtsPoints = minHP;
            }
            else
            {
                _healtsPoints = value;
            }
        }
    }

    void Start () 
    {
        _healtsPoints = maxHP;
        isDeath = false;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        isDead();
        AnimUpdate();
        loseImmune();
    }

    bool isDead()
    {
        if (currentHP == minHP) return isDeath = true;
        return isDeath = false;
    }

    bool loseImmune()
    {
        if (immune)
        {
            timerImmune += Time.deltaTime;
            if (timerImmune > 0.5f) return immune = false;
            return immune = true;
        }
        return immune = false;
    }

    void AnimUpdate()
    {
        if (isDeath)
        {
            anim.SetBool("isDeath", true);
            timer += Time.deltaTime;
            if(timer > 5f) gameObject.SetActive(false);
        }
    }

    public void LoseHP(float dmg)
    {
        if(!immune)
        {
            currentHP -= dmg;
            anim.SetTrigger("takeDmg");
            immune = true;
            Debug.Log("Danno");
        }            
    }
}
