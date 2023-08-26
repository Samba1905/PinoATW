using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : ItemsInteractable
{
    public bool openChest;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        openChest = false;
        interactable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        { 
            interactable = true;
            player.isInteractable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactable = false;
            player.isInteractable = false;
        }
    }

    private void Update()
    {
        if(interactable && player.isInteractable && Input.GetButtonDown("Interact") && !openChest)
        { 
            openChest = true;
            player.nCoin += 3;
        }
    }

    private void LateUpdate()
    {
        if (openChest)
        {
            anim.SetBool("openChest", openChest);
        }
    }
}
