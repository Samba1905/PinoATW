using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj : MonoBehaviour
{
    void Update()
    {
        GetComponent<Rigidbody>().AddForce (-transform.forward, ForceMode.Impulse);
        Destroy (gameObject, 1.25f);
    }
}
