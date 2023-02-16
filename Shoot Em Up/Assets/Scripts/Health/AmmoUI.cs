using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    //Variables
    PlayerController player;
    [Header ("Ammo References")]
    [SerializeField] private Text ammoText;
    [SerializeField] private ScoreControl score;



    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla de tener siempre el jugador referenciado                                                                 */
    /*********************************************************************************************************************************/
    private void Update()
    {        
        if (player == null && !score.playerDeath)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        else
            UpdateAmmo();
    }


    /*********************************************************************************************************************************/
    /*Funcion: UpdateAmmo                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza el texto de municiones en base a las municiones que posea el jugador                                    */
    /*********************************************************************************************************************************/
    private void UpdateAmmo()
    {
        
        if (player.GetCurrentAmmo() <= 0 && Input.GetKey(KeyCode.Space))
            StartCoroutine(Hit());
        ammoText.text = player.GetCurrentAmmo() + "/" + player.GetTotalAmmo();
    }


    /*********************************************************************************************************************************/
    /*Funcion: hit                                                                                                                   */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Si el jugador no tiene balas cargadas, esta corrutina hara parpadear en rojo el texto de municiones               */
    /*********************************************************************************************************************************/
    IEnumerator Hit()
    {        
        ammoText.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(1f);
        ammoText.color = new Color(1, 1, 1);        
    }
}
