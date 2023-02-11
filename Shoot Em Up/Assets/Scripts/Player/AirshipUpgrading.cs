using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipUpgrading : MonoBehaviour
{
    [SerializeField] private GameObject[] playerLevel;
    private ScoreControl scoreLevel;
    private int airshipLevel = 1000;
    private GameObject player;
    private int shipActive;
    private int ammo, totalAmmo, currentHealth;
    [SerializeField] private GameObject levelUp;

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
            airshipLevel += 1000;
            levelUp.SetActive(true);
        }            
    }

    private void GetPlayerParam()
    {
        currentHealth = player.GetComponent<PlayerController>().GetCurrentHealth();
        ammo = player.GetComponent<PlayerController>().GetCurrentAmmo();
        totalAmmo = player.GetComponent<PlayerController>().GetTotalAmmo();
    }

    private void SetPlayerParam()
    {
        player.GetComponent<PlayerController>().SetCurrentHealth(currentHealth);
        player.GetComponent<PlayerController>().SetCurrentAmmo(ammo);
        player.GetComponent<PlayerController>().SetTotalAmmo(totalAmmo);
    }
    private void ControlUpgrade()
    {
        GetPlayerParam();
        if (scoreLevel.GetPlayerScore() > 10000 && shipActive<2)
        {
            Destroy(player);
            player = Instantiate(playerLevel[2], player.transform.position, Quaternion.Euler(0, 0, -90f));
            shipActive++;
            SetPlayerParam();
        }            
        else if (scoreLevel.GetPlayerScore() > 5000 && shipActive<1)
        {
            Destroy(player);
            player = Instantiate(playerLevel[1], player.transform.position, Quaternion.Euler(0, 0, -90f));
            shipActive++;
            SetPlayerParam();
        }
        
    }

}
