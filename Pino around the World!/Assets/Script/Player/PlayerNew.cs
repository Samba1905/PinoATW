using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerNew : MonoBehaviour
{
    Rigidbody rb;
    public float force;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        if (GameManager.WarriorCheck)
        {
            GameObject.Find("Mage").SetActive(false);
            GameObject.Find("Barbarian").SetActive(false);

            Destroy(GameObject.Find("Mage"));
            Destroy(GameObject.Find("Barbarian"));

        }
        else if (GameManager.MageCheck)
        {
            GameObject.Find("Warrior").SetActive(false);
            GameObject.Find("Barbarian").SetActive(false);

            Destroy(GameObject.Find("Warrior"));
            Destroy(GameObject.Find("Barbarian"));
        }
        else if (GameManager.BarbarianCheck)
        {
            GameObject.Find("Mage").SetActive(false);
            GameObject.Find("Warrior").SetActive(false);

            Destroy(GameObject.Find("Mage"));
            Destroy(GameObject.Find("Warrior"));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && Warrior.vulnerable) Debug.Log("Colpito");
        else if (other.gameObject.layer == 3 && !Warrior.vulnerable)
        {
            rb.AddForce(transform.forward * -force, ForceMode.Impulse);
            Debug.Log("Parato");
        }
    }
}
