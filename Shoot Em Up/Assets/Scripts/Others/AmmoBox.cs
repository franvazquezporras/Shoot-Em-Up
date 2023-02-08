using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private int ammo;
    AudioSource audioSource;
    private void Awake()
    {
        Destroy(gameObject, 10);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.layer == Layers.PLAYER)
        {
            audioSource.Play();
            collision.gameObject.GetComponent<PlayerController>().PickAmmo(ammo);
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            Destroy(gameObject,0.5f);
        }
    }
    
}
