using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float rate;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int waves;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
    }

    private void SpawnEnemy()
    {
        for(int i = 0; i<waves;i++)
        Instantiate(enemies[(int)Random.Range(0,enemies.Length)],new Vector3(11,Random.Range(-4.5f,4.5f),0),Quaternion.Euler(0f, 0f, 90f));
    }
}
