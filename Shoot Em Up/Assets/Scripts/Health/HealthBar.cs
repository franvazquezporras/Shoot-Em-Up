using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private Text healthText;
    [SerializeField] private Slider lifeBar;

    private void Awake()
    {
        currentHealth = maxHealth;
        InitHealthBar();
    }


    private void Update()
    {
        InitHealthBar();
        //Codigo para testear
        if (Input.GetKeyDown(KeyCode.P) && currentHealth>0)
            currentHealth--;
    }

    private void InitHealthBar()
    {
        healthText.text = currentHealth + "/" + maxHealth;
        lifeBar.value = currentHealth;

    }

}
