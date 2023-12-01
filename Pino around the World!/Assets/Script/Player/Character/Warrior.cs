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
    float _damage = 2;
    public float Damage { get { return _damage; } }
    PlayerMovement playerM;
    PlayerNew player;
    Rigidbody rb;
    PoolManager poolManager;
    ParticleSystem ps;
    public AudioClip clipAtt, thunderSpell;

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
        playerM.animW.SetBool("isAttacking", isAttacking);

        if (timerAttack > 1f)
        {
            firstAttack = false;
            secondAttack = false;
            thirdAttack = false;
            isAttacking = false;
            playerM.animW.SetBool("firstAttack", false);
            playerM.animW.SetBool("secondAttack", false);
            playerM.animW.SetBool("thirdAttack", false);

        }
        if (!player.ExhaustState) //Condizione che impedisce di attaccare in caso di stato esausto
        {
            if (Input.GetButtonDown("Attack1") && firstAttack && secondAttack && timerAttack < 1f)
            {
                thirdAttack = true;
                doDamage = true;
                playerM.animW.SetBool("thirdAttack", true);
                timerAttack = 0f;
                timerPush = 0f;
            }
            else if (Input.GetButtonDown("Attack1") && firstAttack && timerAttack < 1f)
            {
                secondAttack = true;
                doDamage = true;
                timerAttack = 0f;
                playerM.animW.SetBool("secondAttack", true);
            }
            else if (Input.GetButtonDown("Attack1"))
            {
                firstAttack = true;
                doDamage = true;
                timerAttack = 0f;
                isAttacking = true;
                playerM.animW.SetBool("firstAttack", true);
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
                playerM.animW.SetBool("isShielding", true);
            }
            else
            {
                isShielding = false;
                playerM.animW.SetBool("isShielding", false);
            }
        }
    }

    void SpellCast()
    {
        if(Input.GetButtonDown("Attack3"))
        {
            playerM.animW.SetBool("isCasting", true);
            if (player.DarkEnergyPoints >= 25f)
            {
                poolManager.LightningCall();
                playerM.audioSourceSFX.PlayOneShot(thunderSpell);
                player.UpdateDarkEnergy(25f, true);
                Debug.Log(player.DarkEnergyPoints);
            }
            else playerM.audioSourceSFX.PlayOneShot(playerM.failSpell);
        }
        else
        {
            playerM.animW.SetBool("isCasting", false);
        }
    }


    void EvNormalAttack() //Funzione per attacco più immersivo richiamato da evento
    {
        Vector3 warriorDirection = transform.forward * pushForce + transform.up * pushUpwardForce;
        playerM.audioSourceSFX.pitch = Random.Range(0.90f, 1.10f);
        playerM.audioSourceSFX.PlayOneShot(clipAtt);
        rb.AddForce(warriorDirection * powerPush, ForceMode.Impulse);
        player.UpdateSTA(7.5f);
    }

    void EvThirdAttack() //Funzione per terzo attacco più immersivo richiamato da evento
    {
        Vector3 warriorDirection = transform.forward * pushForce + transform.up * pushUpwardForce;
        playerM.audioSourceSFX.pitch = Random.Range(0.90f, 1.10f);
        playerM.audioSourceSFX.PlayOneShot(clipAtt);
        rb.AddForce(warriorDirection * powerPushTh, ForceMode.Impulse);
        player.UpdateSTA(15f);
    }

    void EvResetAnim() //Funzione per reset attacchi richiamato da evento
    {
        firstAttack = false;
        secondAttack = false;
        thirdAttack = false;
        playerM.animW.SetBool("firstAttack", false);
        playerM.animW.SetBool("secondAttack", false);
        playerM.animW.SetBool("thirdAttack", false);
    }
    void EvFootStep()
    {
        playerM.audioSourceSFX.pitch = Random.Range(0.95f, 1.05f);
        playerM.audioSourceSFX.PlayOneShot(playerM.footStep);
    }
    void EvFootStep2()
    {
        playerM.audioSourceSFX.pitch = Random.Range(0.85f, 1.15f);
        playerM.audioSourceSFX.PlayOneShot(playerM.footStep2);
    }
    void EvIdle()
    {
        playerM.audioSourceSFX.pitch = 1f;
    }
}
