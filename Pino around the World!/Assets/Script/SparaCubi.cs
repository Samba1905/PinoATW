using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SparaCubi : MonoBehaviour
{
    float timer;
    public GameObject proj;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2.5f)
        {           
            GameObject bullet = Instantiate(proj, transform.position, Quaternion.identity);
            timer = 0f;
        }

    }
}
