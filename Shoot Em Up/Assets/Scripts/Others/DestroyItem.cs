using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private AudioSource audioSource;    
    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        audioSource.Play();
        Destroy(gameObject, time);
    }
}
