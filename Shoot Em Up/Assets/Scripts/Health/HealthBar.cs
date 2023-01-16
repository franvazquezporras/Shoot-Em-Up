using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{    
    PlayerController player;
    [SerializeField] private Text healthText;
    [SerializeField] private Slider lifeBar;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        lifeBar.maxValue = player.GetMaxHealth();
        InitHealthBar();
    }


    private void Update()
    {
        InitHealthBar();
        //Codigo para testear
        if (Input.GetKeyDown(KeyCode.P) && player.GetCurrentHealth()>0)
            player.SetCurrentHealth(-1);
    }

    private void InitHealthBar()
    {
        healthText.text = player.GetCurrentHealth() + "/" + player.GetMaxHealth();
        lifeBar.value = player.GetCurrentHealth();

    }

}
