using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Warrior : MonoBehaviour
{
    float timerAttack, timerPush;
    float pushForce = 3f, pushUpwardForce;
    [SerializeField]
    float powerPush, powerPushTh;
    public bool firstAttack, secondAttack, thirdAttack, isAttacking, isShielding, doDamage;
    float _damage = 1;
    public float Damage { get { return _damage; } }
    PlayerMovement playerM;
    PlayerNew player;
    Rigidbody rb;
    PoolManager poolManager;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        playerM = GetComponentInParent<PlayerMovement>();
        player = GetComponentInParent<PlayerNew>();
        poolManager = FindObjectOfType<PoolManager>();
    }

    private void Update()
    {
        AttackMode();
        ShieldMode();
        SpellCast();
    }

    void AttackMode()
    {
        Vector3 warriorDirection = transform.forward * pushForce + transform.up * pushUpwardForce;
        timerAttack += Time.deltaTime;
        timerPush += Time.deltaTime;
        playerM.anim.SetBool("isAttacking", isAttacking);

        if (timerAttack > 1f)
        {
            firstAttack = false;
            secondAttack = false;
            thirdAttack = false;
            isAttacking = false;
            playerM.anim.SetBool("firstAttack", false);
            playerM.anim.SetBool("secondAttack", false);
            playerM.anim.SetBool("thirdAttack", false);

        }
        if (!player.ExhaustState) //Condizione che impedisce di attaccare in caso di stato esausto
        {
            if (Input.GetButtonDown("Attack1") && firstAttack && secondAttack && timerAttack < 1f)
            {
                thirdAttack = true;
                doDamage = true;
                playerM.anim.SetBool("thirdAttack", true);
                timerAttack = 0f;
                timerPush = 0f;
            }
            else if (Input.GetButtonDown("Attack1") && firstAttack && timerAttack < 1f)
            {
                secondAttack = true;
                doDamage = true;
                timerAttack = 0f;
                playerM.anim.SetBool("secondAttack", true);
            }
            else if (Input.GetButtonDown("Attack1"))
            {
                firstAttack = true;
                doDamage = true;
                timerAttack = 0f;
                isAttacking = true;
                playerM.anim.SetBool("firstAttack", true);
            }
        }
    }

    void ShieldMode()
    {
        if (!player.ExhaustState)
        {
            if (Input.GetButton("Attack2"))
            {
                isShielding = true;
                playerM.anim.SetBool("isShielding", true);
            }
            else
            {
                isShielding = false;
                playerM.anim.SetBool("isShielding", false);
            }
        }
    }

    void SpellCast()
    {
        if(Input.GetButtonDown("Attack3"))
        {
            playerM.anim.SetBool("isCasting", true);
            if (player.DarkEnergyPoints >= 25f)
            {
                poolManager.LightningCall();
                player.UpdateDarkEnergy(25f, true);
                Debug.Log(player.DarkEnergyPoints);
            }
            else Debug.Log("Fallimento");
        }
        else
        {
            playerM.anim.SetBool("isCasting", false);
        }
    }


    void EvNormalAttack() //Funzione per attacco più immersivo richiamato da evento
    {
        Vector3 warriorDirection = transform.forward * pushForce + transform.up * pushUpwardForce;
        rb.AddForce(warriorDirection * powerPush, ForceMode.Impulse);
        player.UpdateSTA(7.5f);
    }

    void EvThirdAttack() //Funzione per terzo attacco più immersivo richiamato da evento
    {
        Vector3 warriorDirection = transform.forward * pushForce + transform.up * pushUpwardForce;
        rb.AddForce(warriorDirection * powerPushTh, ForceMode.Impulse);
        player.UpdateSTA(15f);
    }

    void EvResetAnim() //Funzione per reset attacchi richiamato da evento
    {
        firstAttack = false;
        secondAttack = false;
        thirdAttack = false;
        playerM.anim.SetBool("firstAttack", false);
        playerM.anim.SetBool("secondAttack", false);
        playerM.anim.SetBool("thirdAttack", false);
    }
}
