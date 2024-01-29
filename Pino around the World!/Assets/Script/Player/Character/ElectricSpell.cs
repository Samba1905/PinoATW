using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSpell : MonoBehaviour
{
    [SerializeField]
    SphereCollider sphereCollider;

    private void OnEnable()
    {
        transform.position = Barbarian.position + new Vector3 (0f, 1.5f, 0f);
        sphereCollider.radius = 1f;
    }

    private void Update()
    {
        sphereCollider.radius = Mathf.Clamp (sphereCollider.radius, 1f, 5f);
        sphereCollider.radius += Time.deltaTime * 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            other.gameObject.GetComponentInParent<Enemy>().LoseHP(12);
        }
    }

    private void OnDisable()
    {
        sphereCollider.radius = 1f;
    }
}
