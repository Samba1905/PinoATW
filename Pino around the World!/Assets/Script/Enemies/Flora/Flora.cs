using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flora : MonoBehaviour
{
    Enemy enemy;
    bool firstDamage;
    float timerAttack;
    public AudioClip roarClip;
    AudioSource audioSourceSFX;
    PlayerNew player;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerNew>();
    }

    void Update()
    {
        if(!firstDamage && enemy.CurrentHP != 3) Roar();
        if(firstDamage && enemy.PlayerDetect()) Attack();
    }

    bool Roar()
    {
        audioSourceSFX.PlayOneShot(roarClip);
        return firstDamage = true;
    }

    void Attack()
    {
        transform.forward = player.transform.position;
        timerAttack += Time.deltaTime;
        if(timerAttack > 1.5f)
        {
            Debug.Log("SFERA");
            timerAttack = 0f;
        }
    }
}
