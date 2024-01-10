using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) other.gameObject.GetComponentInParent<Enemy>().LoseHP(8f);
    }
}
