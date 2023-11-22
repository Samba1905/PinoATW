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

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
