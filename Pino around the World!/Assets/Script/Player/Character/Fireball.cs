using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    float speed, lifetime;
    Vector3 direction;
    [SerializeField]
    Rigidbody rb;

    private void OnEnable()
    {
        transform.position = Mage.position + new Vector3(0f, 0.5f, 0f);
        direction = PlayerMovement.forwardNow;
        lifetime = 1.2f;
        speed = 750f;
        Invoke("Hide", lifetime);
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * Time.fixedDeltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.gameObject.GetComponentInParent<Enemy>().LoseHP(3);

            Debug.Log(other.gameObject.GetComponentInParent<Enemy>().CurrentHP);
            gameObject.SetActive(false);
        }
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
