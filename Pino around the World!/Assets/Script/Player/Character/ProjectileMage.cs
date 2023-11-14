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
        direction = PlayerMovement.forwardNow;
        lifetime = 2f;
        speed = 500f;
        Invoke("Hide", lifetime);
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * Time.fixedDeltaTime * speed;
    }

    void Hide()
    {
        transform.position = transform.parent.position + new Vector3 (0, 1, 0);
        direction = new Vector3 (0, 0, 0);
        gameObject.SetActive(false);
    }
}
