using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipUpgrading : MonoBehaviour
{
    //Variables
    [SerializeField] private GameObject[] playerLevel;
    private ScoreControl scoreLevel;
    private int airshipLevel = 1000;
    private GameObject player;
    private int shipActive;
    private int ammo, totalAmmo, currentHealth;
    [SerializeField] private GameObject levelUp;

    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Asigna la referencia y spawne el player inicial                                                                   */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        scoreLevel = GetComponent<ScoreControl>();
        player = Instantiate(playerLevel[0], transform.position, Quaternion.Euler(0,0,-90f));
    }


    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla cuando el jugador a superado un nivel                                                                    */
    /*********************************************************************************************************************************/
    void Update()
    {
        if (scoreLevel.GetPlayerScore() > airshipLevel)
        {
            ControlUpgrade();
            airshipLevel += 1000;
            levelUp.SetActive(true);
        }            
    }


    /*********************************************************************************************************************************/
    /*Funcion: GetPlayerParam                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Obtiene los ultimos parametros del player para la nueva nave                                                      */
    /*********************************************************************************************************************************/
    private void GetPlayerParam()
    {
        currentHealth = player.GetComponent<PlayerController>().GetCurrentHealth();
        ammo = player.GetComponent<PlayerController>().GetCurrentAmmo();
        totalAmmo = player.GetComponent<PlayerController>().GetTotalAmmo();
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetPlayerParam                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Guarda los ultimos parametros del player antes de modificar la nave                                               */
    /*********************************************************************************************************************************/
    private void SetPlayerParam()
    {
        player.GetComponent<PlayerController>().SetCurrentHealth(currentHealth);
        player.GetComponent<PlayerController>().SetCurrentAmmo(ammo);
        player.GetComponent<PlayerController>().SetTotalAmmo(totalAmmo);
    }


    /*********************************************************************************************************************************/
    /*Funcion: ControlUpgrade                                                                                                        */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla cuando el jugador a superado los puntos suficientes para upgradear la nave sustituyendo la nave anterior */
    /*********************************************************************************************************************************/
    private void ControlUpgrade()
    {
        GetPlayerParam();
        if (scoreLevel.GetPlayerScore() > 10000 && shipActive<2)
        {
            Destroy(player);
            player = Instantiate(playerLevel[2], player.transform.position, Quaternion.Euler(0, 0, -90f));
            shipActive++;
            SetPlayerParam();
        }            
        else if (scoreLevel.GetPlayerScore() > 5000 && shipActive<1)
        {
            Destroy(player);
            player = Instantiate(playerLevel[1], player.transform.position, Quaternion.Euler(0, 0, -90f));
            shipActive++;
            SetPlayerParam();
        }
        
    }

}
