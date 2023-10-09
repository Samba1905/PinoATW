using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerLoadScene : TriggerArea
{
    ScoreManager SM;
    private void Start()
    {
        SM = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SM.levelEnd = true;
        }
    }
}
