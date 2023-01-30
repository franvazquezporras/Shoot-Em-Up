using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    private int playerScore = 0;
    [SerializeField] private Text scoreText;
    
    
    private void Awake()
    {        
        UpdateText();        
    }

    private void Update()
    {
        UpdateText();                
    }
    public void SetPlayerScore(int score){playerScore += score;
        UpdateText();
    }
    public int GetPlayerScore(){return playerScore;}

    private void UpdateText()
    {
        scoreText.text = playerScore.ToString();
    }

}
