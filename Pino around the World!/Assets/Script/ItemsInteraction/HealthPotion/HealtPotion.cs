using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealtPotion : MonoBehaviour
{
    PlayerNew player;
    public AudioClip audioClip;
    AudioSource clip;

    private void Start()
    {
        player = FindObjectOfType<PlayerNew>();
        clip = GameObject.Find("SFX").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            player.UpdateHP(40f, false);
            clip.PlayOneShot(audioClip);
            gameObject.SetActive(false);
        }
    }
}
