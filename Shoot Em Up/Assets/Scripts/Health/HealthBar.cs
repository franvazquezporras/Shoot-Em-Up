using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{    
    //Variables
    PlayerController player;
    [Header ("Life References")]
    [SerializeField] private Text healthText;
    [SerializeField] private Slider lifeBar;
    [SerializeField] private ScoreControl score;

    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Guarda la referencia del player para controlar su vida y conecta la barra de vida al mismo                        */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        lifeBar.maxValue = player.GetMaxHealth();
        InitHealthBar();
    }

    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza la vida del player y cuando esta llegue a 0 levanta la bandera de jugador muerto para setear los        */
    /*              puntos desde otro script, si el player sube de nivel y pierde la referencia la vuelve a conectar                 */
    /*********************************************************************************************************************************/
    private void Update()
    {
        if (player.GetCurrentHealth() <= 0)
            score.playerDeath = true;        
        if(player== null && !score.playerDeath)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        else
            InitHealthBar();

        
    }


    /*********************************************************************************************************************************/
    /*Funcion: InitHealthBar                                                                                                         */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza la vida del player y el texto de la barra en base a la vida que le quede al player                      */
    /*********************************************************************************************************************************/
    private void InitHealthBar()
    {
        if (player.GetCurrentHealth() <= 0)
            healthText.text = 0 + "/" + player.GetMaxHealth();
        else
            healthText.text = player.GetCurrentHealth() + "/" + player.GetMaxHealth();
        lifeBar.value = player.GetCurrentHealth();
    }

}
