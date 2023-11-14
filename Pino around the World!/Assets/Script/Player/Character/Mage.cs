using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    PoolManager poolManager;
    public static Vector3 forwardPosition;
    // Start is called before the first frame update
    void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        forwardPosition = Vector3.forward;
        if(Input.GetButtonDown("Attack1")) poolManager.CastProjectile();
    }
}
