using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.canDamage = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && player.isAttacking && player.doDamage > 0 && player.canDamage)
        {
            other.gameObject.GetComponent<Enemies>().Healt(player.doDamage);
            player.canDamage = false;
            StartCoroutine("DPS");
        }
    }

    IEnumerator DPS()
    {
        yield return new WaitForSeconds(0.55f);
        player.canDamage = true;
    }
}
