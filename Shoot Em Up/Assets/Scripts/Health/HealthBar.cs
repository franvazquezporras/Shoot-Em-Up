using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{    
    PlayerController player;
    [SerializeField] private Text healthText;
    [SerializeField] private Slider lifeBar;
    [SerializeField] private ScoreControl score;    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        lifeBar.maxValue = player.GetMaxHealth();
        InitHealthBar();
    }


    private void Update()
    {
        if (player.GetCurrentHealth() <= 0)
            score.playerDeath = true;        
        if(player== null && !score.playerDeath)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        else
            InitHealthBar();

        
    }

    private void InitHealthBar()
    {
        healthText.text = player.GetCurrentHealth() + "/" + player.GetMaxHealth();
        lifeBar.value = player.GetCurrentHealth();

    }

}
