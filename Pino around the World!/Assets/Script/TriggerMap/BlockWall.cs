using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWall : MonoBehaviour
{
    [SerializeField] GameObject flora;
    [SerializeField] GameObject [] smoke;
    [SerializeField] Collider collisore;

    private void LateUpdate()
    {
        if(!flora.activeSelf)
        {
            collisore.isTrigger = true;
            foreach (GameObject s in smoke) s.SetActive(false);
        }
    }
}
