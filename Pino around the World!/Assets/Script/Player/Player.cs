using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int takeDamage, nCoin, doDamage;
    public bool isAttacking, canDamage, isInteractable, isDeathUI;
    [SerializeField]
    float verticalSpeed, horizontalSpeed, movementSpeed, rotationspeed, timer, timerDmg;
    bool isMoving, isRunning, isShielding, isCasting, isDead;
    int attack, HP, DE, HPMax;

    Vector3 directionPlayer;
    [SerializeField]
    public Transform provaMove;
    Rigidbody rb;
    Animator anim;
    [SerializeField]
    GameObject weapon;

    [SerializeField]
    TextMeshProUGUI coin;
    [SerializeField]
    Slider healtPointsbar, darkEnergyBar;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb= GetComponent<Rigidbody>();
        movementSpeed = 250f;
        attack = 0;
        DE = 0;
        doDamage = 3;
        HPMax = 100;
        HP = HPMax;
        rotationspeed = 720;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();     
        Combat();
        Animation();
        Dead();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //Input movimento verso la direzione
        horizontalSpeed = Input.GetAxisRaw("Horizontal");
        verticalSpeed = Input.GetAxisRaw("Vertical");

        directionPlayer = new Vector3(verticalSpeed * -1f, 0f ,horizontalSpeed).normalized;
        
        if(Input.GetButton("Run"))
        {
            isRunning = true;
            movementSpeed = 400f;
        }
        else
        {
            isRunning = false;
            movementSpeed = 250f;
        }
        
        //Condizione per ruotare il player nella direzione di movimento
        if (directionPlayer != Vector3.zero)
        {     
            isMoving = true;
            Quaternion toRotate = Quaternion.LookRotation(directionPlayer, Vector3.up);
            transform.rotation = toRotate;
            rb.velocity = transform.TransformDirection(Vector3.forward * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            isMoving = false;
            movementSpeed = 0f;
        }
    }

    void Combat()
    {
        #region Attacchi 
        if (timerDmg < Time.time)
        {
            if (Input.GetButtonDown("Attack1") && timer == 0)   //Codice da miglirare permette di fare attacchi consecutivi diversi
            {
                isAttacking = true;
                timer = Time.time + 0.5f;
                attack = 1;
            }
            else if (Input.GetButtonDown("Attack1") && timer > Time.time)
            {
                isAttacking = true;
                timer = Time.time + 0.5f;
                attack++;
            }
            else if (timer < Time.time)
            {
                isAttacking = false;
                timer = 0f;
                attack = 0;
            }
        }
        #endregion
        #region Scudo
        if (Input.GetButton("Attack2")) //attiva solo l'animazione dello scudo ancora
        {
            StartCoroutine("Shield");
        }
        else
        {
            isShielding = false;
        }
        #endregion
        #region Abilità
        if(Input.GetButtonDown("Attack3")) // attiva solo l'animazione della spell ancora
        {
            isCasting = true;
        }
        else
        {
            isCasting = false;
        }
        #endregion
    }

    void Dead()
    {
        if (HP <= 0) //funzione in caso di morte
        {
            isDead = true;
            isDeathUI = true;
            movementSpeed = 0;
            HP = 0;
            rotationspeed= 0;
        }
    }

    #region Damage, HealthPlus & DarkEnergyPlus

    public int HealtPoints(int dmg) //funzione per perdere vita
    {
        if (!isShielding)
        {
            if (dmg > 0)
            {
                if (!isDead)
                {
                    anim.Play("Take Damage");
                    timerDmg = Time.time + 0.5f;
                }

                HP -= dmg;
            }
        }
        return HP;
    }

    public int DarkEnergyPlus(int darkEnergy) //funzione per guadagnare dark energy
    {
        DE += darkEnergy;

        if (DE >= 100)
        {
            return DE = 100;
        }

        return DE;
    }

    public int HealthPointsPlus(int health) //funzione per guadagnare vita
    {
        HP += health;

        if (HP >= 100)
        {
            return HP = 100;
        }

        return HP;
    }

    #endregion

    void UpdateUI() //funzione per dare dei feedback di vita, monete e darkenergy al player
    {
        healtPointsbar.value = HP;
        darkEnergyBar.value = DE;
        coin.text = nCoin.ToString();
    }
   

    void Animation() //animazioni
    {
        anim.SetBool("isWalking", isMoving);
        anim.SetBool("isRunning", isRunning);
        anim.SetInteger("isAttacking", attack);
        anim.SetBool("isCasting", isCasting);
        anim.SetBool("isShielding", isShielding);
        anim.SetBool("isDead", isDead);
    }

    IEnumerator Shield() //Per dare ritardo allo shield
    {
        yield return new WaitForSeconds(0.3f);
        isShielding = true;
    }
}
