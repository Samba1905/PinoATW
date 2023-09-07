using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform character;
    [SerializeField]
    Transform orientation;
    [SerializeField]
    Rigidbody rb;
    Camera cam;

    float horizontalInput, verticalInput;
    bool isRunning;

    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float moveSpeed, walkSpeed, runSpeed;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        InputPlayer();
    }

    private void FixedUpdate()
    {
        MovementPlayer();
    }

    void InputPlayer()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButton("Run")) isRunning = true;
        else isRunning = false;

        AttackInput();
    }
    void MovementPlayer()
    {
        //Funzioni per dare direzione al player
        Vector3 viewDir = player.position - new Vector3(cam.transform.position.x, player.position.y, cam.transform.position.z);

        orientation.forward = viewDir.normalized; //
        
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Funzione per ruotare il player
        if (inputDir != Vector3.zero) 
        { 
            character.forward = Vector3.Slerp(character.forward, inputDir.normalized, rotationSpeed * Time.fixedDeltaTime);
        }

        //Velocita di movimento
        if (!isRunning) moveSpeed = walkSpeed;
        else if (isRunning) moveSpeed = runSpeed;

        //Movimento del player
        rb.velocity = (inputDir.normalized * moveSpeed * Time.fixedDeltaTime);
    }  

    void AttackInput()
    {

    }
}
