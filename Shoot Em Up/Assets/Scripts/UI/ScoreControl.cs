using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    private int playerScore = 0;
    [SerializeField] private Text scoreText;
    public bool playerDeath;
    
    private void Awake()
    {        
        UpdateText();        
    }

    private void Update()
    {
        if (playerDeath)
            SaveScore();
        else
            UpdateText();                
    }
    public void SetPlayerScore(int score){playerScore += score;
        UpdateText();
    }
    public int GetPlayerScore(){return playerScore;}


    public void SaveScore()
    {
        Debug.Log(playerScore);
        int aux;
        for (int i = 0; i <= 10; i++)
        {
            if (playerScore > PlayerPrefs.GetInt("Puntuacion" + i))
            {
                aux = PlayerPrefs.GetInt("Puntuacion" + i);
                PlayerPrefs.SetInt("Puntuacion" + i, playerScore);
                playerScore = aux;
            }
        }
    }
    private void UpdateText()
    {
        scoreText.text = playerScore.ToString();
    }

}
