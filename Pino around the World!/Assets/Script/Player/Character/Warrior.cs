using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Warrior : MonoBehaviour
{
    float timerAttack, timerPush;
    float pushForce = 3f, pushUpwardForce;
    public bool firstAttack, secondAttack, thirdAttack, isAttacking;
    PlayerMovement playerM;
    PlayerNew player;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        playerM = GetComponentInParent<PlayerMovement>();
        player = GetComponentInParent<PlayerNew>();
    }

    private void Update()
    {
        AttackMode();        
    }

    void AttackMode()
    {
        Vector3 warriorDirection = transform.forward * pushForce + transform.up * pushUpwardForce;
        timerAttack += Time.deltaTime;
        timerPush += Time.deltaTime;
        playerM.anim.SetBool("isAttacking", isAttacking);

        if(timerPush < 0.33f) rb.AddForce(warriorDirection, ForceMode.Impulse);

        if(firstAttack && secondAttack && thirdAttack)
        {
            firstAttack = false;
            secondAttack = false;
            thirdAttack = false;
            isAttacking = false;
        }
        else if (timerAttack > 0.75f)
        {
            firstAttack = false;
            secondAttack = false;
            thirdAttack = false;
            isAttacking = false;
        }

        if(Input.GetButtonDown("Attack1") && firstAttack && secondAttack && timerAttack < 0.75f)
        {
            Debug.Log("Attacco2");
            thirdAttack = true;
            playerM.anim.SetTrigger("thirdAttack");
            player.UpdateSTA(20f);
            timerAttack = 0f;
            timerPush = 0f;
        }
        else if (Input.GetButtonDown("Attack1") && firstAttack && timerAttack < 0.75f)
        {
            Debug.Log("Attacco1");
            rb.AddForce(warriorDirection, ForceMode.Impulse);
            timerAttack = 0f;
            secondAttack = true;
            playerM.anim.SetTrigger("secondAttack");
            player.UpdateSTA(5f);
        }
        else if (Input.GetButtonDown("Attack1"))
        {
            Debug.Log("Attacco");
            rb.AddForce(warriorDirection * 20f, ForceMode.Impulse);
            timerAttack = 0f;
            isAttacking = true;
            firstAttack = true;
            playerM.anim.SetTrigger("firstAttack");
            player.UpdateSTA(5f);
        }
    }
}
