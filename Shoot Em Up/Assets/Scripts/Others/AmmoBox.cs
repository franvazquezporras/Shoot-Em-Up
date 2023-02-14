using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    //Variables
    [Header ("Box Parameters")]
    [SerializeField] private int ammo;
    [SerializeField] private int health;
    [SerializeField] private bool healthbox;
    AudioSource audioSource;


    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: asigna referencias y destruye el item 10 segundos despues de su invocacion si no recibe interacciones             */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        Destroy(gameObject, 10);
        audioSource = GetComponent<AudioSource>();
    }



    /*********************************************************************************************************************************/
    /*Funcion: OnTriggerEnter2D                                                                                                      */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: collision (Collider del objeto con el que colisiona)                                                    */   
    /*Descripción: suma municiones o vida al jugador cuando este entra en la colision de la caja, luego destruye la caja             */
    /*********************************************************************************************************************************/
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.layer == Layers.PLAYER)
        {
            audioSource.Play();
            if (!healthbox)
                collision.gameObject.GetComponent<PlayerController>().PickAmmo(ammo);
            else
                collision.gameObject.GetComponent<PlayerController>().GetDamage(health);

            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            Destroy(gameObject,0.5f);
        }
    }
    
}
