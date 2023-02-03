using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float rate;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] bosses;
    [SerializeField] private int waves;
    [SerializeField] private ScoreControl score;
    private GameObject bossAlive;

    private int levelUnloked = 1;
    
    private int levelGoal = 2000;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
    }

    private void SpawnEnemy()
    {
        if ((score.GetPlayerScore()>levelGoal || score.GetPlayerScore() % levelGoal ==0) && score.GetPlayerScore()>0 && bossAlive ==null)
        {
            bossAlive = Instantiate(bosses[Random.Range(0,bosses.Length)], new Vector3(11, Random.Range(-4.5f, 4.5f), 0), Quaternion.Euler(0f, 0f, -90f));    
            levelGoal += 2000;
            levelUnloked++;
        }                            
        if (bossAlive == null)
            for (int i = 0; i < waves; i++)
            {
                if(levelUnloked<enemies.Length)
                    Instantiate(enemies[(int)Random.Range(0, levelUnloked)], new Vector3(11, Random.Range(-4.5f, 4.5f), 0), Quaternion.Euler(0f, 0f, 90f));
                else
                    Instantiate(enemies[(int)Random.Range(0, enemies.Length)], new Vector3(11, Random.Range(-4.5f, 4.5f), 0), Quaternion.Euler(0f, 0f, 90f));
            }
                
    }


}
