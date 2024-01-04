using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    PlayerNew player;
    float dist;
    bool interactable, isOpen;
    [SerializeField]
    GameObject coins;
    Animator anim;
    AudioSource clip;
    public AudioClip audioClip;

    private void Start()
    {
        anim = GetComponent<Animator>();
        interactable = false;
        dist = 3.5f;
        player = FindObjectOfType<PlayerNew>();
        clip = GameObject.Find("SFX").GetComponent<AudioSource>();
    }
    private void LateUpdate()
    {
        Ray ray = new Ray(transform.position, (player.transform.position - transform.position).normalized);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist))
        {
            if(hit.collider.gameObject.layer == 10) interactable = true;           
        }
        else interactable = false;

        Interaction();
    }

    void Interaction()
    {
        if (interactable && Input.GetButton("Interact") && !isOpen)
        {
            anim.SetBool("openChest", true);
            Invoke("Hide", 5f);
        }
    }

    void Hide()
    {
        coins.SetActive(false);
        isOpen = true;
    }

    void EvCoins()
    {
        clip.PlayOneShot(audioClip);
    }
}
