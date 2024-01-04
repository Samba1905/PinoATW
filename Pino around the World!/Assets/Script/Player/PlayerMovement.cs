using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, HideInInspector]
    Transform player;
    [SerializeField]
    Transform warrior, mage, barbarian;
    [SerializeField, HideInInspector]
    Transform orientation;
    [SerializeField, HideInInspector]
    Rigidbody rb;
    [SerializeField, HideInInspector]
    PlayerNew playerN;
    Camera cam;
    public AudioSource audioSourceSFX;
    public Animator animW, animM, animB;

    public AudioClip footStep, footStep2, dashSFX, failSpell;

    int actuallyCh;
    public PlayerNew.Character presentCh;

    #region Movement
    float horizontalInput, verticalInput;
    bool isRunning, isWalking;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float moveSpeed, walkSpeed, runSpeed, exhSpeed;
    Vector3 move;
    public static Vector3 forwardNow;
    #endregion

    #region Dash
    [SerializeField]
    float dashForce, dashUpwardForce;
    [SerializeField]
    float timeDash;
    [SerializeField, HideInInspector]
    float timerDash;
    [SerializeField]
    bool isDashing;
    #endregion

    public bool consumeRun, consumeDash;

    private void Start()
    {
        cam = Camera.main;
        player = GetComponent<Transform>();
        animW = GameObject.Find("Warrior").GetComponent<Animator>();
        animM = GameObject.Find("Mage").GetComponent<Animator>();
        animB = GameObject.Find("Barbarian").GetComponent<Animator>();
        warrior = GameObject.Find("Warrior").GetComponent<Transform>();
        mage = GameObject.Find("Mage").GetComponent<Transform>();
        barbarian = GameObject.Find("Barbarian").GetComponent<Transform>();
        orientation = GetComponentInChildren<Transform>().Find("Orientation");
        audioSourceSFX = GameObject.Find("SFX").GetComponentInChildren<AudioSource>();
        playerN = GetComponent<PlayerNew>();
        rb = GetComponent<Rigidbody>();
        actuallyCh = 0;
    }

    private void Update()
    {
        InputPlayer();
        Animation();
        forwardNow = mage.forward;
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
        if(!playerN.ExhaustState)
        {
            if (Input.GetButton("Run")) isRunning = true;
            else isRunning = false;
        }
        else
        {
            consumeRun = false;
        }

        //Input per dash
        if (!playerN.ExhaustState && Time.timeScale > 0.2f)
        {
            if (Input.GetButtonDown("Dash"))
            {
                consumeDash = true;
                isDashing = true;
                audioSourceSFX.PlayOneShot(dashSFX);
            }
        }

        //Prova
        move = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        if (move != Vector3.zero) isWalking = true;
        else isWalking = false;
    }
    void MovementPlayer()
    {
        //Funzioni per dare direzione al player
        Vector3 viewDir = player.position - new Vector3(cam.transform.position.x, player.position.y, cam.transform.position.z);

        orientation.forward = viewDir.normalized;
        
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Funzione per ruotare il player
        if (inputDir != Vector3.zero) 
        { 
            warrior.forward = Vector3.Slerp(warrior.forward, inputDir.normalized, rotationSpeed * Time.fixedDeltaTime);
            //mage.forward = warrior.forward; TopDownShooter ora
            barbarian.forward = warrior.forward;
            if (isRunning) consumeRun = true;
            else consumeRun = false;
        }

        //Velocita di movimento
        if (playerN.ExhaustState) moveSpeed = exhSpeed;
        else if (!isRunning) moveSpeed = walkSpeed;
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
                actuallyCh++;
                ChangeCh();
                isDashing = false;
                timerDash = 0f;
            }
        }
    }

    private void Dash() //Funzione per fare il Dash
    {
        Vector3 dashDirection = warrior.forward * dashForce + warrior.up * dashUpwardForce;
        rb.AddForce(dashDirection, ForceMode.Impulse);
    }

    private void ChangeCh()
    {
        switch(actuallyCh)
        {
            case 0:
                presentCh = PlayerNew.Character.Warrior;
                break;
            case 1:
                presentCh = PlayerNew.Character.Mage;
                break;
            case 2:
                presentCh = PlayerNew.Character.Barbarian;
                break;
            case 3:
                actuallyCh = 0;
                goto case 0;
        }
    }

    void Animation() //Aggiornamento animazioni
    {
        animW.SetBool("isWalking", isWalking);
        animW.SetBool("isRunning", isRunning);
        animW.SetBool("isDashing", isDashing);
        animW.SetBool("isDashing", isDashing);
        animM.SetBool("isWalking", isWalking);
        animM.SetBool("isRunning", isRunning);
        animM.SetBool("isDashing", isDashing);
        animM.SetBool("isDashing", isDashing);
        animB.SetBool("isWalking", isWalking);
        animB.SetBool("isRunning", isRunning);
        animB.SetBool("isDashing", isDashing);
        animB.SetBool("isDashing", isDashing);
    }
}
