using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSpell : MonoBehaviour
{
    private void OnEnable()
    {
        transform.position = Barbarian.position + new Vector3 (0f, 1.5f, 0f);
    }
}
