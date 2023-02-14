using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{

    //Variables
    private int playerScore = 0;
    [SerializeField] private Text scoreText;
    public bool playerDeath;
    private bool save;
    [SerializeField] private GameObject losepanel;



    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Llama por primera vez la actualizacion del texto de puntos                                                        */
    /*********************************************************************************************************************************/
    private void Awake()
    {        
        UpdateText();        
    }



    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Mientras el jugador no muera actualizara el texto de puntos, en caso contrario guardara los puntos y mostrara     */
    /*              el panel de game over                                                                                            */
    /*********************************************************************************************************************************/
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



    /*********************************************************************************************************************************/
    /*Funcion: SetPlayerScore                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: score (puntos que recibe el jugador)                                                                    */
    /*Descripción: Setea los puntos y vuelve a modificarlos en el texto de puntos                                                    */
    /*********************************************************************************************************************************/
    public void SetPlayerScore(int score){
        playerScore += score;
        UpdateText();
    }

    /*********************************************************************************************************************************/
    /*Funcion: GetPlayerScore                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Recupera los puntos del player                                                                                    */
    /*********************************************************************************************************************************/
    public int GetPlayerScore(){return playerScore;}



    /*********************************************************************************************************************************/
    /*Funcion: SaveScore                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Comprueba que los puntos obtenidos al finalizar partida esten entre los 10 primero y lo guarda en su posicion     */
    /*********************************************************************************************************************************/
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

    /*********************************************************************************************************************************/
    /*Funcion: UpdateText                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza los puntos del jugador en el texto de la UI y los guarda en la variable del playerpreb                  */
    /*********************************************************************************************************************************/
    private void UpdateText()
    {
        scoreText.text = playerScore.ToString();
        PlayerPrefs.SetInt("Puntuacion", playerScore);
    }

}
