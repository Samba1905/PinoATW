using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMage : MonoBehaviour
{
    float speed, lifetime;
    Vector3 direction;
    [SerializeField]
    Rigidbody rb;

    private void OnEnable()
    {
        transform.position = Mage.position + Vector3.up;
        direction = PlayerMovement.forwardNow;
        lifetime = 0.5f;
        speed = 1000f;
        Invoke("Hide", lifetime);
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * Time.fixedDeltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 0 || other.gameObject.layer == 6)
        {
            other.gameObject.GetComponentInParent<Enemy>().LoseHP(1);
            Debug.Log(other.gameObject.GetComponentInParent<Enemy>().CurrentHP);
        }
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
