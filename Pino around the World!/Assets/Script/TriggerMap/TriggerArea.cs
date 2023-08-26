using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerArea : MonoBehaviour
{
    public void LoadNextScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
