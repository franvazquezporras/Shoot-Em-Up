using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipUpgrading : MonoBehaviour
{
    [SerializeField] private GameObject[] playerLevel;
    private ScoreControl scoreLevel;
    private int airshipLevel;
    private GameObject player;
    private int shipActive;


    private void Awake()
    {
        scoreLevel = GetComponent<ScoreControl>();
        player = Instantiate(playerLevel[0], transform.position, Quaternion.Euler(0,0,-90f));
    }
    void Update()
    {
        if (scoreLevel.GetPlayerScore() > airshipLevel)
        {
            ControlUpgrade();
            airshipLevel += airshipLevel;
        }
            
    }

    private void ControlUpgrade()
    {
        if (scoreLevel.GetPlayerScore() > 10000 && shipActive<2)
        {
            Destroy(player);
            player = Instantiate(playerLevel[2], player.transform.position, Quaternion.Euler(0, 0, -90f));
            shipActive++;
        }            
        else if (scoreLevel.GetPlayerScore() > 5000 && shipActive<1)
        {
            Destroy(player);
            player = Instantiate(playerLevel[1], player.transform.position, Quaternion.Euler(0, 0, -90f));
            shipActive++;
        }
        
    }

}
