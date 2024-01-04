using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float _healtsPoints;
    [SerializeField]
    float maxHP, minHP = 0;
    [SerializeField]
    float speed, timer, timerImmune, distDetect;
    [SerializeField]
    bool isDeath;
    public Animator anim;
    PlayerNew player;
    public AudioClip hitDmg;
    AudioSource SFX;
    Collider colliderB;

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
        SFX = GameObject.Find("SFX").GetComponent<AudioSource>();
        colliderB = GetComponent<Collider>();
    }

    private void Update()
    {
        AnimUpdate();
        PlayerDetect();
    }

    bool isDead()
    {
        if (CurrentHP == minHP)
        {
            DEP();
            colliderB.isTrigger = true;
            return isDeath = true;
        }          
        return isDeath = false;
    }

    void AnimUpdate()
    {
        if (isDeath)
        {
            anim.SetBool("isDeath", true);
            timer += Time.deltaTime;
            if(timer > 3.5f) gameObject.SetActive(false);
        }
    }

    void DEP() //DarkEnergyPlayer
    {
        player.UpdateDarkEnergy(10, false);
    }

    public bool PlayerDetect() //Detect Player
    {
        Ray ray = new Ray(transform.position + new Vector3 (0f,0.5f,0f), (player.transform.position - transform.position).normalized);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, distDetect))
        {
            if (hit.collider.gameObject.layer == 10)
            {
                return true;
            }
        }
        return false;
    }

    public void LoseHP(float dmg)
    {
        CurrentHP -= dmg;
        SFX.PlayOneShot(hitDmg);
        isDead();
        anim.SetTrigger("takeDmg");
        Debug.Log("Danno");
    }
}
