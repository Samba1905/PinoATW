using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEnergyPotion : MonoBehaviour
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
        if (other.gameObject.layer == 10)
        {
            player.UpdateDarkEnergy(50f, false);
            clip.PlayOneShot(audioClip);
            gameObject.SetActive(false);
        }
    }
}
