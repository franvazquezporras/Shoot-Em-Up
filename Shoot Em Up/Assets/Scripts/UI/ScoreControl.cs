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
        StartCoroutine(sumar());
    }
    
    public void SetPlayerScore(int score){playerScore += score;
        UpdateText();
    }
    public int GetPlayerScore(){return playerScore;}

    private void UpdateText()
    {
        scoreText.text = playerScore.ToString();
    }

    IEnumerator sumar()
    {
        for(int i = 0; i < 3000; i++)
        {
            SetPlayerScore(100);
            yield return new WaitForSeconds(1);
        }
        
        yield return new WaitForEndOfFrame();
    }
}
