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
    bool isDeath;
    Animator anim;
    PlayerNew player;

    public float CurrentHP
    {
        get
        {
            return _healtsPoints;
        }
        private set
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
        player = GameObject.Find("Player").GetComponent<PlayerNew>();
    }

    private void Update()
    {
        isDead();
        AnimUpdate();
    }

    bool isDead()
    {
        if (CurrentHP == minHP) return isDeath = true;
        return isDeath = false;
    }

    void AnimUpdate()
    {
        if (isDeath)
        {
            anim.SetBool("isDeath", true);
            timer += Time.deltaTime;
            player.UpdateDarkEnergy(10f, false);
            if(timer > 5f) gameObject.SetActive(false);
        }
    }

    public void LoseHP(float dmg)
    {
        CurrentHP -= dmg;
        anim.SetTrigger("takeDmg");
        Debug.Log("Danno");
    }
}
