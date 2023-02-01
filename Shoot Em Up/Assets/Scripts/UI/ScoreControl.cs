using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    private int playerScore = 0;
    [SerializeField] private Text scoreText;
    public bool playerDeath;
    private bool save;
    [SerializeField] private GameObject losepanel;
    
    private void Awake()
    {        
        UpdateText();        
    }

    private void Update()
    {
        if (playerDeath && !save)
        {
            SaveScore();
            save = true;
            losepanel.SetActive(true);
            
        }            
        else
            UpdateText();                
    }
    public void SetPlayerScore(int score){playerScore += score;
        UpdateText();
    }
    public int GetPlayerScore(){return playerScore;}


    public void SaveScore()
    {
        int puntuacion = PlayerPrefs.GetInt("Puntuacion");
        int aux;
        for (int i = 0; i <= 10; i++)
        {
            if (puntuacion > PlayerPrefs.GetInt("Puntuacion" + i))
            {
                aux = PlayerPrefs.GetInt("Puntuacion" + i);
                PlayerPrefs.SetInt("Puntuacion" + i, puntuacion);
                puntuacion = aux;
            }
        }

        
    }
    private void UpdateText()
    {
        scoreText.text = playerScore.ToString();
        PlayerPrefs.SetInt("Puntuacion", playerScore);
    }

}
