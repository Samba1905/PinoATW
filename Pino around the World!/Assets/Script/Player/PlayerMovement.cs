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
    [SerializeField]
    float dashForce, dashUpwardForce;
    [SerializeField]
    float timerDash, timeDash;
    [SerializeField]
    bool isDashing;

    private void Awake()
    {
        if(GameManager.WarriorCheck)
        {
            GameObject.Find("Mage").SetActive(false);
            GameObject.Find("Barbarian").SetActive(false);

            Destroy(GameObject.Find("Mage"));
            Destroy(GameObject.Find("Barbarian"));

        }
        else if(GameManager.MageCheck)
        { 
            GameObject.Find("Warrior").SetActive(false);
            GameObject.Find("Barbarian").SetActive(false);

            Destroy(GameObject.Find("Warrior"));
            Destroy(GameObject.Find("Barbarian"));
        }
        else if(GameManager.BarbarianCheck)
        {
            GameObject.Find("Mage").SetActive(false);
            GameObject.Find("Warrior").SetActive(false);

            Destroy(GameObject.Find("Mage"));
            Destroy(GameObject.Find("Warrior"));
        }
    }

    private void Start()
    {
        cam = Camera.main;
        player = GetComponent<Transform>();
        character = GameObject.FindWithTag("Character").GetComponent<Transform>();
        orientation = GetComponentInChildren<Transform>().Find("Orientation");
        rb = GetComponent<Rigidbody>();
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
        //Input per movimento
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        //Input per corsa
        if (Input.GetButton("Run")) isRunning = true;
        else isRunning = false;

        //Input per dash
        if (Input.GetButtonDown("Dash")) isDashing = true;


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

        //Dash
        if(isDashing)
        {
            Dash();
            timerDash += Time.deltaTime;
            if (timerDash > timeDash)
            {
                isDashing = false;
                timerDash = 0f;
            }
        }
    }

    private void Dash()
    {
        Vector3 dashDirection = character.forward * dashForce + character.up * dashUpwardForce;
        rb.AddForce(dashDirection, ForceMode.Impulse);
    }
}
