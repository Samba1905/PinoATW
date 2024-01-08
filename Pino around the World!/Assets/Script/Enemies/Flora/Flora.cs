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
    [SerializeField]
    GameObject [] proj;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerNew>();
    }

    void Update()
    {
        if(!firstDamage && enemy.CurrentHP != 3) Roar();
        if(firstDamage && enemy.PlayerDetect() && enemy.CurrentHP != 0) Attack();
    }

    bool Roar()
    {
        audioSourceSFX.PlayOneShot(roarClip);
        timerAttack = 1.25f;
        return firstDamage = true;
    }

    void Attack()
    {
        timerAttack += Time.deltaTime;
        if (!proj[0].activeInHierarchy) transform.forward = (transform.position - player.transform.position) * -1;

        if (timerAttack > 1.55f)
        {            
            foreach (GameObject p in proj)
            {            
                p.SetActive(true);
            }
            timerAttack = 0f;
        }
    }
}
