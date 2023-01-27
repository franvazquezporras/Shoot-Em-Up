using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float rate;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int waves;
    [SerializeField] private ScoreControl score;
    private bool bossAlive;
    private int levelGoal = 1000;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
    }

    private void SpawnEnemy()
    {
        Debug.Log(score.GetPlayerScore() % levelGoal);
        if (score.GetPlayerScore() % levelGoal != 0 && bossAlive)
            bossAlive = false;
        
        if ((score.GetPlayerScore()>levelGoal || score.GetPlayerScore() % levelGoal ==0) && score.GetPlayerScore()>0 && !bossAlive)
        {
            Instantiate(enemies[9], new Vector3(11, Random.Range(-4.5f, 4.5f), 0), Quaternion.Euler(0f, 0f, -90f));
            bossAlive = true;
            levelGoal += 1000;
        }                    
        
        if (score.GetPlayerScore() % levelGoal != 0 || score.GetPlayerScore() == 0 && !bossAlive)
            for (int i = 0; i < waves; i++)
                Instantiate(enemies[(int)Random.Range(0, enemies.Length - 1)], new Vector3(11, Random.Range(-4.5f, 4.5f), 0), Quaternion.Euler(0f, 0f, 90f));

    }


}
