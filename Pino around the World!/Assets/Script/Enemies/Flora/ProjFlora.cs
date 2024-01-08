using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjFlora : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    private void OnEnable()
    {
        Invoke("Hide", 1.15f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            other.gameObject.GetComponentInParent<PlayerNew>().UpdateHP(10f, true);
        }
    }

    private void LateUpdate()
    {
        rb.AddForce(transform.forward * 7.5f, ForceMode.Acceleration);
    }

    void Hide()
    {
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
        rb.velocity = Vector3.zero;
    }
}
