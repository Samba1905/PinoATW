using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    PoolManager poolManager;
    public static Vector3 forwardPosition, position;
    // Start is called before the first frame update
    void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        forwardPosition = Vector3.forward;
        position = transform.position;
        RotationMage();
        if(Input.GetButtonDown("Attack1")) poolManager.CastProjectile();
    }

    void RotationMage()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 playerPosScreen = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 direction = mousePos - playerPosScreen;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
    }
}
