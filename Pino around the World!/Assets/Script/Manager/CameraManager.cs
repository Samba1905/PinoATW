using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    PlayerNew player;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        player = FindObjectOfType<PlayerNew>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition();
    }

    void CameraPosition()
    {
        mainCamera.transform.position = player.transform.position + new Vector3(7f, 7f, 0f);
        mainCamera.transform.localEulerAngles = new Vector3(40f, -90, 0f);
    }
}
