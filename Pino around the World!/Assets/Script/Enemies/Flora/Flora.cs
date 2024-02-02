using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flora : MonoBehaviour
{
    Enemy enemy;
    bool firstDamage, firstRoar;
    public bool allFlora;
    float timerAttack;
    public AudioClip roarClip;
    AudioSource audioSourceSFX;
    PlayerNew player;
    [SerializeField]
    GameObject [] proj;
    [SerializeField]
    GameObject [] research; 

    void Start()
    {
        enemy = GetComponent<Enemy>();
        audioSourceSFX = GameObject.Find("SFX").GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerNew>();
        research = GameObject.FindGameObjectsWithTag("Flora");
    }

    void Update()
    {
        if (!firstDamage && enemy.CurrentHP != 4)
        {
            foreach(GameObject g in research)
            {
                g.GetComponentInParent<Flora>().allFlora = true;
                g.GetComponentInParent<Flora>().firstRoar = true;
            }
        }
        if (firstRoar)
        {
            Roar();
            firstRoar = false;
        }
        if (allFlora && enemy.PlayerDetect() && enemy.CurrentHP != 0) Attack();
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
