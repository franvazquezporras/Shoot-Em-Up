using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxSpawner : MonoBehaviour
{
    [SerializeField] private float rate;
    [SerializeField] private GameObject[] ammoBoxs;    
    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
    }

    private void SpawnEnemy()
    {
        Instantiate(ammoBoxs[Random.Range(0, ammoBoxs.Length)], new Vector3(Random.Range(-7f, 7f), Random.Range(-4.5f, 4.5f), 0), Quaternion.Euler(0f, 0f, -90f));
    }
}
