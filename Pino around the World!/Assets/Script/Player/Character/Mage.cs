using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    PoolManager poolManager;
    PlayerNew player;
    PlayerMovement playerM;
    Animator anim;
    public AudioClip clipAtt, explosion;
    public static Vector3 forwardPosition, position;

    void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();
        player = GetComponentInParent<PlayerNew>();
        playerM = GetComponentInParent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        forwardPosition = Vector3.forward;
        position = transform.position;
        RotationMage();
        SpellCast();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack1") && player.DarkEnergyPoints >= 3f)
        {           
            anim.SetTrigger("isAttacking");
        }
    }

    void SpellCast()
    {
        if (Input.GetButtonDown("Attack3"))
        {
            playerM.animM.SetBool("isCasting", true);
            if (player.DarkEnergyPoints >= 17f)
            {
                poolManager.CastExplosion();
                player.UpdateDarkEnergy(17f, true);
                playerM.audioSourceSFX.PlayOneShot(explosion);
                Debug.Log(player.DarkEnergyPoints);
            }
            else playerM.audioSourceSFX.PlayOneShot(playerM.failSpell);
        }
        else
        {
            playerM.animM.SetBool("isCasting", false);
        }
    }

    void RotationMage()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 playerPosScreen = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 direction = mousePos - playerPosScreen;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
    }

    void EvAttack()
    {
        poolManager.CastProjectile();
        playerM.audioSourceSFX.pitch = Random.Range(0.90f, 1.10f);
        playerM.audioSourceSFX.PlayOneShot(clipAtt);
        player.UpdateDarkEnergy(3f, true);
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
