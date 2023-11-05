using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public string enemyName;
    public int takeDamage;
    public bool isAttacking;
    [SerializeField]
    protected int HealtPoints, damage;
    protected float speed, rangeMovement, rangeVision, minRange, attackSpeed;
    [SerializeField]
    protected bool canDamage;
    GameObject target;
    protected Animator anim;
    protected PlayerNew player;
    protected ScoreManager SM;

    Color colorDmg;

    private void Awake()
    {
        player = FindObjectOfType<PlayerNew>();
        colorDmg = Color.red;
    }

    public int Healt(int dmg) //funzione per perdere vita
    {
        if(dmg > 0)
        {
            HealtPoints -= dmg;
            
            if(HealtPoints <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        return HealtPoints;
    }

    protected void WalkEnemy() //Funzione per far muovere verso il player
    {
        target = GameObject.FindGameObjectWithTag("Player");
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if(distance <= rangeMovement && distance >= minRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
        }
        if(distance <= rangeVision)
        {
            transform.LookAt(target.transform.position);
        }
    }

    protected void SetAttribute(string name, int HP, int dmg, float spd, float rangeMov, float rangeMin, float rangeVis, float attSpd) //funzione per impostare i valori al nemico
    {
        enemyName = name;
        HealtPoints = HP;
        damage = dmg;
        speed = spd * Time.deltaTime;
        rangeMovement = rangeMov;
        minRange = rangeMin;
        rangeVision = rangeVis;
        attackSpeed = attSpd;
    }

    private void OnDisable()
    {
        //player.DarkEnergyPlus(2);
    }
}

